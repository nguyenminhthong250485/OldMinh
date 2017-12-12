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
    public partial class frmIP : Form
    {
        Database db;
        public frmIP()
        {
            InitializeComponent();
            db = new Database();
        }

        private void frmIP_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.checkIP();
        }
    }
}
