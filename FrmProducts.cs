using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class FrmProducts : Form
    {
        public FrmProducts()
        {
            InitializeComponent();
            this.Load += FrmProducts_Load;
            EnableDoubleBuffered(dgvProducts);
        }

        private void EnableDoubleBuffered(DataGridView dgv)
        {
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgv,
                new object[] { true }
            );
        }
        
        private void FrmProducts_Load(object sender, EventArgs e)
        {
            LoadStaticData();
            LoadGridData();
        }

        private void LoadStaticData()
        {
            lblDetailsTitle.Text = "Quick View";
            txtProdName.Text = "MacBook Pro 14\" M3";
            txtProdPrice.Text = "$ 1,999.00";
            txtProdSpec.Text = "Chip: Apple M3\r\nRAM: 16GB\r\nStorage: 512GB SSD\r\nColor: Space Gray";
            btnEdit.Text = "Update Inventory";
        }

        private void LoadGridData()
        {
            // إيقاف التوليد التلقائي للأعمدة لأننا صممناها مسبقاً في الـ Designer
            dgvProducts.AutoGenerateColumns = false;

            // إضافة البيانات مباشرة إلى الـ DataGridView بدون بناء أعمده من الصفر بالكود
            dgvProducts.Rows.Add("101", "MacBook Pro 14\" M3", "Laptops", "24", "$1,999", "In Stock");
            dgvProducts.Rows.Add("102", "iPhone 15 Pro Max", "Phones", "8", "$1,199", "Low Stock");
            dgvProducts.Rows.Add("103", "Samsung S24 Ultra", "Phones", "15", "$1,299", "In Stock");
            dgvProducts.Rows.Add("104", "HP LaserJet Pro", "Printers", "0", "$450", "Out of Stock");
            dgvProducts.Rows.Add("105", "iPad Air 5th Gen", "Laptops", "30", "$599", "In Stock");
            dgvProducts.Rows.Add("106", "Sony WH-1000XM5", "Accessories", "12", "$399", "In Stock");
            dgvProducts.Rows.Add("107", "Dell XPS 15", "Laptops", "5", "$1,799", "Low Stock");

            // تلوين عمود الـ Status
            dgvProducts.CellFormatting += DgvProducts_CellFormatting;
        }

        private void DgvProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null && dgvProducts.Columns[e.ColumnIndex].Name == "colStatus")
            {
                string status = e.Value.ToString();

                if (status == "Low Stock")
                    e.CellStyle.ForeColor = Color.DarkOrange;
                else if (status == "Out of Stock")
                    e.CellStyle.ForeColor = Color.Red;
                else
                    e.CellStyle.ForeColor = Color.ForestGreen;

                e.CellStyle.Font = new Font(dgvProducts.Font, FontStyle.Bold);
            }
        }

        private void pnlGridContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}