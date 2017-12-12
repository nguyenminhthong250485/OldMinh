using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomLoto
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            foreach(Control C in gb_Style.Controls)
            {
                try
                {
                    ((Label)C).Text = "";
                }
                catch { }
            }
            Mb = "hn";
            DayOfWeek Thu = DateTime.Now.DayOfWeek;
            switch(Thu)
            {
                case DayOfWeek.Monday:
                    Mn1 = "tp";
                    Mn2 = "dt";
                    break;
                case DayOfWeek.Tuesday:
                    Mn1 = "bt";
                    Mn2 = "vt";
                    break;
                case DayOfWeek.Wednesday:
                    Mn1 = "dn";
                    Mn2 = "ct";
                    break;
                case DayOfWeek.Thursday:
                    Mn1 = "tn";
                    Mn2 = "ag";
                    break;
                case DayOfWeek.Friday:
                    Mn1 = "vl";
                    Mn2 = "bd";
                    break;
                case DayOfWeek.Saturday:
                    Mn1 = "tp";
                    Mn2 = "la";
                    break;
                case DayOfWeek.Sunday:
                    Mn1 = "tg";
                    Mn2 = "kg";
                    break;
            }
        }
        Random ran = new Random();
        string Mn1, Mn2, Mb;
        int CaseDau = 0, CaseDuoi = 0, CaseDauDuoi = 0, CaseBao = 0, CaseMn1 = 0, CaseMn2 = 0, CaseMb = 0;
        private string Random2D()
        {
            string Output = "";
            int Number = ran.Next(0, 99);
            if(Number<10)
            {
                Output = "0" + Number.ToString();
            }
            else
            {
                Output = Number.ToString();
            }
            return Output;
        }
        private string Random3D()
        {
            string Output = "";
            int Number = ran.Next(0, 999);
            if (Number < 10)
            {
                Output = "00" + Number.ToString();
            }
            else if (Number < 100)
            {
                Output = "0" + Number.ToString();
            }
            else
            {
                Output = Number.ToString();
            }
            return Output;
        }

        private string RandomMoney(int TotalMoney,int RoundMoney)
        {
            string Output = "";
            int BaseMoney = ran.Next(0, TotalMoney/RoundMoney);
            int Money = BaseMoney * RoundMoney;
            if (RoundMoney > TotalMoney) return "Error Input";
            if(Money>TotalMoney)
            {
                Output = TotalMoney.ToString();
            }
            else
            {
                Output = Money.ToString();
            }
            return Output;
        }

        private string RandomDai(string TenDai,int randomcase)
        {
            string Output = "";
            //int randomcase = ran.Next(0, 10);
            switch(TenDai)
            {
                case "tp":
                    switch(randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Tp";
                            break;
                        case 3:
                            Output = "TP";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "dt":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Dt";
                            break;
                        case 3:
                            Output = "DT";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "bt":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Bt";
                            break;
                        case 3:
                            Output = "BT";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "vt":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Vt";
                            break;
                        case 3:
                            Output = "VT";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "dn":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Dn";
                            break;
                        case 3:
                            Output = "DN";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "ct":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Ct";
                            break;
                        case 3:
                            Output = "CT";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "tn":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Tn";
                            break;
                        case 3:
                            Output = "TN";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "ag":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Ag";
                            break;
                        case 3:
                            Output = "AG";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "vl":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Vl";
                            break;
                        case 3:
                            Output = "VL";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "bd":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Bd";
                            break;
                        case 3:
                            Output = "BD";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "la":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "La";
                            break;
                        case 3:
                            Output = "LA";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "tg":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Tg";
                            break;
                        case 3:
                            Output = "TG";
                            break;
                        case 4:
                            Output = "dc";
                            break;
                        case 5:
                            Output = "dch";
                            break;
                        default:
                            break;
                    }
                    break;
                case "kg":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Kg";
                            break;
                        case 3:
                            Output = "KG";
                            break;
                        case 4:
                            Output = "dp";
                            break;
                        case 5:
                            Output = "dph";
                            break;
                        default:
                            break;
                    }
                    break;
                case "hn":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "Hn";
                            break;
                        case 3:
                            Output = "HN";
                            break;
                        case 4:
                            Output = "Mb";
                            break;
                        case 5:
                            Output = "MB";
                            break;
                        case 6:
                            Output = "mb";
                            break;
                        default:
                            Output = "";
                            break;
                    }
                    break;
                case "2d":
                    switch (randomcase)
                    {
                        case 1:
                            Output = TenDai;
                            break;
                        case 2:
                            Output = "2dai";
                            break;
                        case 3:
                            Output = Mn1 + Mn2;
                            break;
                        case 4:
                            Output = Mn2 + Mn1;
                            break;
                        case 5:
                            Output = "2Dai";
                            break;
                        default:
                            Output = "dcdp";
                            break;
                    }
                    break;

            }
            return Output;
        }

        private void bt_StyleDa_Click(object sender, EventArgs e)
        {
            CaseDau += 1;
            switch(CaseDau)
            {
                case 1:
                    lb_StyleDa.Text = "da";
                    break;
                case 2:
                    lb_StyleDa.Text = "dau";
                    break;
                case 3:
                    lb_StyleDa.Text = "ddx";
                    break;
                default:
                    lb_StyleDa.Text = "";
                    break;
            }
        }

        

        private void bt_StyleDu_Click(object sender, EventArgs e)
        {
            CaseDuoi += 1;
            switch (CaseDuoi)
            {
                case 1:
                    lb_StyleDu.Text = "du";
                    break;
                case 2:
                    lb_StyleDu.Text = "duoi";
                    break;
                case 3:
                    lb_StyleDu.Text = "dxd";
                    break;
                default:
                    lb_StyleDu.Text = "";
                    break;
            }
        }
        private void bt_Dd_Click(object sender, EventArgs e)
        {
            CaseDauDuoi += 1;
            switch (CaseDauDuoi)
            {
                case 1:
                    lb_StyleDd.Text = "dd";
                    break;
                case 2:
                    lb_StyleDd.Text = "dadu";
                    break;
                default:
                    lb_StyleDd.Text = "";
                    break;
            }
        }
        private void bt_Bao_Click(object sender, EventArgs e)
        {
            CaseBao += 1;
            switch (CaseBao)
            {
                case 1:
                    lb_StyleBao.Text = "b";
                    break;
                case 2:
                    lb_StyleBao.Text = "bl";
                    break;
                case 3:
                    lb_StyleBao.Text = "bao";
                    break;
                default:
                    lb_StyleBao.Text = "";
                    break;
            }
        }
        private void bt_Mn1_Click(object sender, EventArgs e)
        {
            CaseMn1 += 1;
            lb_Mn1.Text = RandomDai(Mn1, CaseMn1);
        }
        private void bt_Mn2_Click(object sender, EventArgs e)
        {
            CaseMn2 += 1;
            lb_Mn2.Text = RandomDai(Mn2, CaseMn2);
        }
        private void bt_Mb_Click(object sender, EventArgs e)
        {
            CaseMb += 1;
            lb_Mb.Text = RandomDai(Mb, CaseMb);
        }
    }
}
