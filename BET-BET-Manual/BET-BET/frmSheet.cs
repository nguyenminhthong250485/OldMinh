using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BET_BET
{
    public partial class frmSheet : Form
    {
        Database db;
        DataTable dt;
        BindingSource bs;
        public frmSheet()
        {
            InitializeComponent();
            db = new Database();
        }
        private void loadDataToComboBox()
        {
            cboSheet.DataSource = db.getSheetName();
            cboSheet.Name = "name";
            cboSheet.ValueMember = "name";
        }

        private void btnAddNewSheet_Click(object sender, EventArgs e)
        {
            if (txtSheetName.Text != "")
            {
                if (db.doInsertSheetName(txtSheetName.Text) > 0)
                    loadDataToComboBox();
            }
        }

        private void loadDataOnGridView()
        {
            if (cboSheet.Text != "")
            {
                dt = db.getSheetBet(cboSheet.Text).Tables["SheetBet"];
                bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
                dataGridView1.Columns["sheetname"].ReadOnly = true;
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["username"].Width = 150;
                dataGridView1.Columns["ip"].Width = 150;
                dataGridView1.Columns["money"].Width = 300;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);                    
                }

                dataGridView1.Columns["type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["betgroup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["usd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["sheetname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you want to save it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.UpdateSheetBet();
                MessageBox.Show("Successfully");
                loadDataOnGridView();
            }
            else
            {
                loadDataOnGridView();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadDataOnGridView();
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            int row = dataGridView1.Rows.Count;
            e.Row.Cells["sheetname"].Value = cboSheet.Text;            
        }

        private void frmSheet_Load(object sender, EventArgs e)
        {
            loadDataToComboBox();
        }

        private void btnCheckIP_Click(object sender, EventArgs e)
        {
            frmIP frm = new frmIP();
            frm.ShowDialog();
        }
    }
}
