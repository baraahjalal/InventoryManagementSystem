using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem.Classes
{
    /// <summary>
    /// Central UI tokens and styling helpers. Invoke from *.Designer.cs only.
    /// </summary>
    public static class AppTheme
    {
        public static readonly Color Primary       = Color.FromArgb(15, 23, 42);
        public static readonly Color PrimaryHover  = Color.FromArgb(30, 41, 59);
        public static readonly Color SidebarHover  = Color.FromArgb(51, 65, 85);
        public static readonly Color Surface       = Color.White;
        public static readonly Color SurfaceMuted  = Color.FromArgb(249, 250, 251);
        public static readonly Color Border        = Color.FromArgb(229, 231, 235);
        public static readonly Color TextPrimary   = Color.FromArgb(17, 24, 39);
        public static readonly Color TextSecondary = Color.FromArgb(107, 114, 128);
        public static readonly Color TextMuted       = Color.FromArgb(156, 163, 175);
        public static readonly Color GridHeaderBg    = Color.FromArgb(15, 23, 42);
        public static readonly Color GridHeaderFg    = Color.White;
        public static readonly Color GridAltRow      = Color.FromArgb(249, 250, 251);
        public static readonly Color GridSelection   = Color.FromArgb(226, 232, 240);

        public static readonly Font FontNav       = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        public static readonly Font FontTitle     = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        public static readonly Font FontBody      = new Font("Segoe UI", 10F);
        public static readonly Font FontGrid      = new Font("Segoe UI", 10F);
        public static readonly Font FontGridHeader = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static readonly Font FontGridCompact = new Font("Segoe UI", 9.5F);

        public static void ApplyMainShell(
            Form form,
            Panel sidebar,
            Panel mainContent,
            Button[] navButtons,
            Label systemIdLabel,
            Label companyNameLabel,
            Label companyDetailsLabel)
        {
            form.BackColor = Surface;

            sidebar.BackColor = Primary;

            mainContent.BackColor = Surface;

            foreach (var btn in navButtons)
                ApplyNavButton(btn);

            if (systemIdLabel != null)
            {
                systemIdLabel.Font      = new Font("Segoe UI", 9F);
                systemIdLabel.ForeColor = TextMuted;
            }

            if (companyNameLabel != null)
            {
                companyNameLabel.Font      = FontTitle;
                companyNameLabel.ForeColor = Primary;
            }

            if (companyDetailsLabel != null)
            {
                companyDetailsLabel.Font      = new Font("Segoe UI", 12F);
                companyDetailsLabel.ForeColor = TextSecondary;
            }
        }

        public static void ApplyNavButton(Button button)
        {
            button.BackColor               = Color.Transparent;
            button.FlatStyle               = FlatStyle.Flat;
            button.FlatAppearance.BorderSize         = 0;
            button.FlatAppearance.MouseOverBackColor = SidebarHover;
            button.Font                    = FontNav;
            button.ForeColor               = Color.White;
            button.Cursor                  = Cursors.Hand;
            button.TextAlign               = ContentAlignment.MiddleLeft;
            button.UseVisualStyleBackColor = false;
        }

        /// <summary>Primary list grids: dark header, alternating rows.</summary>
        public static void ApplyStandardGrid(DataGridView grid, int headerHeight = 40, int rowHeight = 36)
        {
            if (grid == null) return;

            grid.BackgroundColor       = Surface;
            grid.BorderStyle           = BorderStyle.None;
            grid.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor             = Border;
            grid.RowHeadersVisible     = false;
            grid.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight   = headerHeight;
            grid.RowTemplate.Height    = rowHeight;

            var alt = new DataGridViewCellStyle
            {
                BackColor = GridAltRow
            };
            grid.AlternatingRowsDefaultCellStyle = alt;

            var header = new DataGridViewCellStyle
            {
                Alignment         = DataGridViewContentAlignment.MiddleLeft,
                BackColor         = GridHeaderBg,
                ForeColor         = GridHeaderFg,
                Font              = FontGridHeader,
                SelectionBackColor = GridHeaderBg,
                SelectionForeColor = GridHeaderFg,
                Padding           = new Padding(12, 0, 8, 0),
                WrapMode          = DataGridViewTriState.True
            };
            grid.ColumnHeadersDefaultCellStyle = header;

            var cell = new DataGridViewCellStyle
            {
                Alignment          = DataGridViewContentAlignment.MiddleLeft,
                BackColor          = Surface,
                ForeColor          = TextPrimary,
                Font               = FontGrid,
                SelectionBackColor = GridSelection,
                SelectionForeColor = TextPrimary,
                Padding            = new Padding(12, 0, 8, 0),
                WrapMode           = DataGridViewTriState.False
            };
            grid.DefaultCellStyle = cell;
        }

        /// <summary>Detail / spec sub-grids inside forms.</summary>
        public static void ApplyCompactGrid(DataGridView grid)
        {
            ApplyStandardGrid(grid, headerHeight: 28, rowHeight: 28);

            var header = grid.ColumnHeadersDefaultCellStyle;
            header.Font              = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            header.Padding           = new Padding(8, 0, 4, 0);
            grid.ColumnHeadersDefaultCellStyle = header;

            var cell = grid.DefaultCellStyle;
            cell.Font    = FontGridCompact;
            cell.Padding = new Padding(8, 0, 4, 0);
            grid.DefaultCellStyle = cell;
        }
    }
}
