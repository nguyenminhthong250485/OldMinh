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
    public partial class frmMember : Form
    {
        Database db;
        BindingSource bs = new BindingSource();
        private DataTable table;

        int match_no = -1;
        int match_id = -1;
    
        public frmMember()
        {
            InitializeComponent();
            db = new Database();
        }      

        private void frmMain_Load(object sender, EventArgs e)
        {
            btn1.Click += Button_Click;
            btn2.Click += Button_Click;
            btn3.Click += Button_Click;
            btn4.Click += Button_Click;
            btn5.Click += Button_Click;
            btn6.Click += Button_Click;
            btn7.Click += Button_Click;
        }

        void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (radPlus.Checked)
                lblInput.Text = (double.Parse(lblInput.Text) + double.Parse(button.Text)).ToString();
            else
            {
                lblInput.Text = (double.Parse(lblInput.Text) - double.Parse(button.Text)).ToString();             
            }         
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            if (lblInput.Text != "0" && cboPerson.Text != "")
            {
                ticket o = new ticket();
                o.memberid = frmLogin.m.id;
                o.name = cboPerson.Text;
                o.matchid = match_id;
                o.companyid = txtCompanyID.Text.ToUpper();
                o.game = 1;
                o.money = double.Parse(lblInput.Text);

                int code = db.doInsertTicket(o);
                if (code > 0)
                {
                    lblRed.Text = (double.Parse(lblRed.Text) + double.Parse(lblInput.Text)).ToString();
                    lblInput.Text = "0";
                    cboPerson.Text = "";
                }
                else if (code == -99)
                {
                    MessageBox.Show("Hết giờ cược");
                }
            }
        }

        private void btnXanh_Click(object sender, EventArgs e)
        {
            if (lblInput.Text != "0" && cboPerson.Text != "")
            {
                ticket o = new ticket();
                o.memberid = frmLogin.m.id;
                o.name = cboPerson.Text;
                o.matchid = match_id;
                o.companyid = txtCompanyID.Text.ToUpper();
                o.game = 2;
                o.money = double.Parse(lblInput.Text);

                int code = db.doInsertTicket(o);
                if (code > 0)
                {                    
                    lblBlue.Text = (double.Parse(lblBlue.Text) + double.Parse(lblInput.Text)).ToString();
                    lblInput.Text = "0";
                    cboPerson.Text = "";
                }
                else if (code == -99)
                {
                    MessageBox.Show("Hết giờ cược");
                }
            }
        }

        private void btnHoa_Click(object sender, EventArgs e)
        {
            if (lblInput.Text != "0" && cboPerson.Text != "")
            {
                ticket o = new ticket();
                o.memberid = frmLogin.m.id;
                o.name = cboPerson.Text;
                o.matchid = match_id;
                o.companyid = txtCompanyID.Text.ToUpper();
                o.game = 3;
                o.money = double.Parse(lblInput.Text);

                int code = db.doInsertTicket(o);
                if (code > 0)
                {
                    lblHoa.Text = (double.Parse(lblHoa.Text) + double.Parse(lblInput.Text)).ToString();
                    lblInput.Text = "0";
                    cboPerson.Text = "";
                }
                else if (code == -99)
                {
                    MessageBox.Show("Hết giờ cược");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                match o = db.getCurrentMatch(txtCompanyID.Text, frmLogin.m.agentid);
                if (o.no != match_no)
                {
                    match_no = o.no;                    
                    match_id = o.id;
                    lblMatchNo.Text = match_no.ToString();
                    lblInput.Text = "0";
                    lblRed.Text = "0";
                    lblBlue.Text = "0";
                    lblHoa.Text = "0";
                }
            }
            catch (Exception)
            {                
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(btnStart.Text == "Start")
            {
                timer1.Enabled = true;
                btnStart.Text = "Stop";
                groupBox1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                btnStart.Text = "Start";
                groupBox1.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(btnEdit.Text == "Sửa danh sách")
            {
                richPerson.Enabled = true;
                btnEdit.Text = "Lưu danh sách";
            }
            else
            {
                btnEdit.Text = "Sửa danh sách";
                richPerson.Enabled = false;
                cboPerson.Items.Clear();
                foreach (String str in richPerson.Text.Split('\n'))
                {
                    cboPerson.Items.Add(str);
                }
            }
        }
    }
}
