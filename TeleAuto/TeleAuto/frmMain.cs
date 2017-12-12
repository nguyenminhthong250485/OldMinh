using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeleAuto
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TelegramHelper tele = new TelegramHelper("472134659:AAGBdeNQki_tz5LxL-KW9904ckM78kQkNTM");
            getUpdates lst = tele.getMessages();
            foreach (result rs in lst.result)
                MessageBox.Show(rs.message.from.id.ToString());
        }
    }
}
