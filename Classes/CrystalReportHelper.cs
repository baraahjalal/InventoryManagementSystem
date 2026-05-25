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
