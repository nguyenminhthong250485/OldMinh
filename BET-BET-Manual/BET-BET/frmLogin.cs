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
    public partial class frmLogin : Form
    {
        Database db;
        public frmLogin()
        {
            InitializeComponent();
            db = new Database();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(db.Login(txtUsername.Text, txtPassword.Text) != "")
            {
                frmMain frm = new frmMain();
                this.Hide();
                frm.ShowDialog();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
    }
}
