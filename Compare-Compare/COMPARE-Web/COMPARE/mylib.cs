using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace COMPARE
{
    public class mylib
    {
        public static string MD5(string str)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original string and compute hash
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(str);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            string hashed = BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
            return hashed;
        }

        public static string CFS(string str)
        {
            int codelen = 30;
            long codespace;
            double newcode = 1;
            string cfs = "";
            codespace = codelen - str.Length;
            if (!(codespace < 1))
                for (int i = 0; i < codespace; i++)
                    str += (char)21;
            double been;
            for (int j = 0; j < codelen; j++)
            {
                been = codelen + ((int)str[j]) * (j + 1);
                newcode = newcode * been;
            }
            string codestr = newcode.ToString();
            string newcodestr = "";
            for (int k = 0; k < codestr.Length; k++)
            {
                if (k < codestr.Length - 3)
                    newcodestr += CFSCode(codestr.Substring(k, 3));
                else
                    newcodestr += CFSCode(codestr.Substring(k));
            }
            for (int i = 19; i <= newcodestr.Length - 18; i = i + 2)
                cfs += newcodestr.Substring(i, 1);
            return cfs;
        }

        private static string CFSCode(string word)
        {
            string res = "";
            for (int i = 0; i < word.Length; i++)
            {
                res += Convert.ToInt16(word[i]).ToString();
            }
            decimal d = decimal.Parse(res);
            return DecimalToHex(d);
        }

        private static string DecimalToHex(decimal d)
        {
            int[] bits = decimal.GetBits(d);
            if (bits[3] != 0) throw new InvalidOperationException("Only +ve integers supported!");
            string s = Convert.ToString(bits[2], 16).PadLeft(8, '0') // high
                    + Convert.ToString(bits[1], 16).PadLeft(8, '0') // middle
                    + Convert.ToString(bits[0], 16).PadLeft(8, '0'); // low
            //Console.WriteLine(s);

            /* or Jon's much tidier: string.Format("{0:x8}{1:x8}{2:x8}",
                    (uint)bits[2], (uint)bits[1], (uint)bits[0]);  */

            const decimal chunk = (decimal)(1 << 16);
            StringBuilder sb = new StringBuilder();
            while (d > 0)
            {
                int fragment = (int)(d % chunk);
                sb.Insert(0, Convert.ToString(fragment, 16).PadLeft(4, '0'));
                d -= fragment;
                d /= chunk;
            }
            string res = sb.ToString();
            while (res.StartsWith("0"))
                res = res.Substring(1);
            return res.ToUpper();
        }
    }

}
