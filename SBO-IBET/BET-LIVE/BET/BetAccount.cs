using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BET
{
    public partial class BetAccount : Form
    {
        Database db;
        DataTable dt;
        BindingSource bs;
        public BetAccount()
        {
            InitializeComponent();
            db = new Database();
        }

        private void BetAccount_Load(object sender, EventArgs e)
        {
            loadDataToComboBox();
        }

        private void loadDataToComboBox()
        {
            cboSheet.DataSource = db.getSheet();
            cboSheet.Name = "sheetname";
            cboSheet.ValueMember = "sheetname";
        }

        private void loadDataOnGridView()
        {
            if (cboSheet.Text != "")
            {
                dt = db.getBetAccount(cboSheet.Text).Tables["BetAccount"];
                bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
                dataGridView1.Columns["sheetname"].ReadOnly = true;
                dataGridView1.Columns["id"].Visible = false;                
                dataGridView1.Columns["sboname"].Width = 250;
                dataGridView1.Columns["sboip"].Width = 250;
                dataGridView1.Columns["ibetname"].Width = 250;
                dataGridView1.Columns["ibetip"].Width = 250;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            DialogResult dlg = MessageBox.Show("Do you want to save it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.UpdateBetAccount();
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
            e.Row.Cells["control"].Value = "betcontrol" + row;
        }

        private void btnAddNewSheet_Click(object sender, EventArgs e)
        {
            if (txtSheetName.Text != "")
            {
                if(db.doInsertSheetName(txtSheetName.Text)>0)
                    loadDataToComboBox();
            }
        }
    }
}
