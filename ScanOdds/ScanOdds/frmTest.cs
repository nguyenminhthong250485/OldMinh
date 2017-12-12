using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanOdds
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime d1 = DateTime.Parse("2017-06-07 13:00");
            DateTime d2 = DateTime.Parse("2017-06-07 15:00");
            TimeSpan s = d2.Subtract(d1);
            MessageBox.Show(s.TotalSeconds.ToString());
        }
    }
}
