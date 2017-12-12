using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGENT
{
    public partial class MainF : Form
    {
        public MainF()
        {
            InitializeComponent();
        }

        private void bt_NewAcc_Click(object sender, EventArgs e)
        {
            FNewMember formNew = new FNewMember();
            formNew.ShowDialog();
        }
    }
}
