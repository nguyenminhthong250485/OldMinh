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
            cboSheet.DataSource = db.getPartnerName();
            cboSheet.Name = "name";
            cboSheet.ValueMember = "name";
        }

        private void btnAddNewSheet_Click(object sender, EventArgs e)
        {
            if (txtPartnerName.Text != "")
            {
                if (db.doInsertPartnerName(txtPartnerName.Text) > 0)
                    loadDataToComboBox();
            }
        }

        private void loadDataOnGridView()
        {
            if (cboSheet.Text != "")
            {
                dt = db.getAccBet(cboSheet.Text).Tables["AccBet"];
                bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;

                dataGridView1.Columns["id"].Width = 30;
                dataGridView1.Columns["type"].Width = 50;
                dataGridView1.Columns["group"].Width = 50;
                dataGridView1.Columns["username"].Width = 100;
                dataGridView1.Columns["usd"].Width = 50;
                dataGridView1.Columns["ip"].Width = 100;
                dataGridView1.Columns["money"].Width = 600;
                dataGridView1.Columns["mode"].Width = 50;
                dataGridView1.Columns["profit"].Width = 50;
                dataGridView1.Columns["league"].Width = 150;
                dataGridView1.Columns["status"].Width = 50;
                dataGridView1.Columns["PartnerName"].Width = 100;
                dataGridView1.Columns["MaxBet"].Width = 100;

                dataGridView1.Columns["id"].ReadOnly = true;
                dataGridView1.Columns["PartnerName"].ReadOnly = true;
                

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);      
                    col.DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleCenter; 
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you want to save it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.UpdateAccBet();
                MessageBox.Show("Successfully");
                //loadDataOnGridView();
            }
            else
            {
                //loadDataOnGridView();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadDataOnGridView();
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            int row = dataGridView1.Rows.Count;
            e.Row.Cells["PartnerName"].Value = cboSheet.Text;            
        }

        private void frmSheet_Load(object sender, EventArgs e)
        {
            loadDataToComboBox();
        }
    }
}
