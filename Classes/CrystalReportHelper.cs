using System;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using InventoryManagementSystem.DAL;

namespace InventoryManagementSystem.Classes
{
    /// <summary>
    /// Applies the same SQL Server connection as DatabaseHelper to Crystal pull-model reports.
    /// </summary>
    public static class CrystalReportHelper
    {
        /// <summary>
        /// Sets report parameters from the logged-in session (e.g. {?CurrentUser} on Stock Inventory Status).
        /// Call before <see cref="ReportDocument.Refresh"/>.
        /// </summary>
        public static void ApplySessionParameters(ReportDocument report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var printedBy = DatabaseHelper.CurrentUser?.Username ?? "Unknown";

            if (HasParameter(report, "CurrentUser"))
                report.SetParameterValue("CurrentUser", printedBy);
        }

        // Returns ParameterFields to set on the viewer so it doesn't prompt the user.
        public static ParameterFields BuildViewerParameterFields()
        {
            var fields = new ParameterFields();

            var field = new ParameterField { ParameterFieldName = "CurrentUser" };
            field.CurrentValues.Add(new ParameterDiscreteValue
            {
                Value = DatabaseHelper.CurrentUser?.Username ?? "Unknown"
            });
            fields.Add(field);

            return fields;
        }

        private static bool HasParameter(ReportDocument report, string name)
        {
            foreach (ParameterFieldDefinition param in report.DataDefinition.ParameterFields)
            {
                if (string.Equals(param.Name, name, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(param.ParameterFieldName, name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static void ApplyDatabaseLogon(ReportDocument report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var server   = DatabaseHelper.DataSource;
            var database = DatabaseHelper.InitialCatalog;

            report.SetDatabaseLogon(string.Empty, string.Empty, server, database);

            foreach (Table table in report.Database.Tables)
            {
                var logon = table.LogOnInfo.ConnectionInfo;
                logon.ServerName         = server;
                logon.DatabaseName       = database;
                logon.IntegratedSecurity = true;
                table.ApplyLogOnInfo(table.LogOnInfo);
            }
        }
    }
}
