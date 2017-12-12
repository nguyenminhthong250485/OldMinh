using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace BET
{
    class LoadDataByExcel
    {
        public class Control
        {
            public string UserNameSbo = "";
            public string IpSbo = "";
            public string GiaDoSbo = "";
            public string UserNameIbet = "";
            public string IpIbet = "";
            public string GiaDoIbet = "";
            public string str_UserNameSbo = "";
            public string str_IpSbo = "";
            public string str_GiaDoSbo = "";
            public string str_UserNameIbet = "";
            public string str_IpIbet = "";
            public string str_GiaDoIbet = "";
            public string str_Money = "";
            public string Style = "";
            public string str_Style = "";
            public string Profit = "";
            public string str_Profit = "";
            public string str_Group = "";
        }
        public static Excel._Worksheet LoadFile(string PathFile, string name)
        {
            int index = 1;
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();

            oWB = oXL.Workbooks.Open(PathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
            for (int i = 1; i <= oWB.Sheets.Count; i++)
            {
                oSheet = (Excel._Worksheet)oWB.Sheets[i];
                if (oSheet.Name == name)
                {
                    index = i;
                    break;
                }
            }
            oSheet = (Excel._Worksheet)oWB.Sheets[index];
            return oSheet;
        }
        public static string getSheetNames(string PathFile)
        {
            string sheetName="";
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();

            oWB = oXL.Workbooks.Open(PathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
             
            for(int i = 1; i <= oWB.Sheets.Count; i++)
            {
                oSheet = (Excel._Worksheet)oWB.Sheets[i];
                sheetName += oSheet.Name + ",";
            }
            return sheetName.Substring(0,sheetName.Length-1);
        }


        static string GetValue(Excel.Range oRange)
        {
            string result = "";
            try
            {
                result=oRange.Value2.ToString();
            }
            catch
            {

            }
            return result;
        }
        public static List<Control> LoadData(string PathFile, string  name)
        {
            Excel._Worksheet oSheet;
            oSheet = LoadFile(PathFile, name);
            int n_Row = oSheet.UsedRange.Rows.Count;

            List<Control> ControlAcc = new List<Control>();
            for (int j = 2; j <= n_Row; j++)
            {
                Control Tam = new Control();
                Tam.str_UserNameSbo = GetValue(oSheet.Cells[j, 2]);
                Tam.str_IpSbo = GetValue(oSheet.Cells[j, 3]);
                Tam.str_GiaDoSbo = GetValue(oSheet.Cells[j, 4]);
                Tam.str_UserNameIbet = GetValue(oSheet.Cells[j, 5]);
                Tam.str_IpIbet = GetValue(oSheet.Cells[j, 6]);
                Tam.str_GiaDoIbet = GetValue(oSheet.Cells[j, 7]);
                Tam.str_Money = GetValue(oSheet.Cells[j, 8]);
                Tam.str_Style = GetValue(oSheet.Cells[j, 9]);
                Tam.str_Group = GetValue(oSheet.Cells[j, 10]);
                ControlAcc.Add(Tam);
            }
            return ControlAcc;
        } 
    }
}
