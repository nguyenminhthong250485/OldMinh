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
    public partial class Login : Form
    {
        Database db;
        public Login()
        {
            InitializeComponent();
            db = new Database();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                string username = db.Login(txtUsername.Text, txtPassword.Text);
                if (username != "")
                {
                    Main frm = new Main();
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Login successfully");
                    this.Close();
                }
            }
        }
    }
}
