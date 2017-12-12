using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XanhDo
{
    public partial class frmAgent : Form
    {
        Database db;
        DataTable dt;
        BindingSource bs = new BindingSource();

        int no;
        public frmAgent()
        {
            InitializeComponent();
            db = new Database();
        }
        private void frmAgent_Load(object sender, EventArgs e)
        {
                        
        }

        private void btnCreateMatch_Click(object sender, EventArgs e)
        {
            no = Int32.Parse(txtMatch.Text);
            if (db.doInsertMatch(txtCompany.Text, no, frmLogin.a.id) > 0)
            {
                groupOdds.Enabled = true;
                groupResult.Enabled = true;
                groupTicket.Enabled = true;
            }            
        }

        private void btnUpdateOdds_Click(object sender, EventArgs e)
        {
            db.doUpdateOdd(txtCompany.Text, Int32.Parse(txtMatch.Text), double.Parse(txtRedOdds.Text), double.Parse(txtBlueOdds.Text), double.Parse(txtGreenOdds.Text.Split(':')[1]));
        }

        private void btnUpdateResult_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (radRedWin.Checked)
                result = 1;
            else if (radBlueWin.Checked)
                result = 2;
            else if (radHoaWin.Checked)
                result = 3;
            else if (radCancel.Checked)
                result = 0;

            if (db.doUpdateResult(txtCompany.Text, Int32.Parse(txtMatch.Text), result)>0)
            {
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtCompany.Text != "" && Int32.Parse(txtMatch.Text) > 0)
            {
                dt = db.getCurrentWinLoss(txtCompany.Text, Int32.Parse(txtMatch.Text), frmLogin.a.id, dtpDate.Text);
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            txtMatch.Text = (Int32.Parse(txtMatch.Text) + 1).ToString();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            txtMatch.Text = (Int32.Parse(txtMatch.Text) - 1).ToString();
        }
    }
}
