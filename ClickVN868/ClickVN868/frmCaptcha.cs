using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickVN868
{
    public partial class frmCaptcha : Form
    {
        Bitmap img;
        public string captcha;
        public frmCaptcha()
        {
            InitializeComponent();
        }

        public frmCaptcha(Bitmap img)
        {
            InitializeComponent();
            this.img = img;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            captcha = txtCaptcha.Text;
            this.Close();
        }

        private void frmCaptcha_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = img;
            txtCaptcha.Focus();
        }       
    }
}
