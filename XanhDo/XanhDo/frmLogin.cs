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
    public partial class frmLogin : Form
    {
        public static member m = null;        
        public static agent a = null; 
        Database db;
        public frmLogin()
        {
            InitializeComponent();
            db = new Database();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (radMember.Checked)
            {
                m = db.loginMember(txtUsername.Text, txtPassword.Text);
                if (m != null)
                {
                    frmMember frm = new frmMember();
                    frm.Show();
                    this.Hide();
                }
            }
            else if(radAgent.Checked)
            {
                a = db.loginAgent(txtUsername.Text, txtPassword.Text);
                if (a != null)
                {
                    this.Hide();
                    frmAgent frm = new frmAgent();                    
                    frm.ShowDialog();                    
                    this.Close();
                }
            }
            

        }
    }
}
