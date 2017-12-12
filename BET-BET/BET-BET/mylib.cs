using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BET_BET
{
    public static class mylib
    {
        public static void AppendText(this RichTextBox box, string text, Color color, bool AddNewLine = false)
        {
            string now = DateTime.Now.ToLongTimeString();
            text += "---{" + now + "}";
            if (AddNewLine)
            {
                text += Environment.NewLine;
            }

            box.SelectionColor = color;
            box.AppendText(text);
            box.ScrollToCaret();
        }
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
        public static string sha1(string str)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            SHA1 sha1;

            //Instantiate MD5CryptoServiceProvider, get bytes for original string and compute hash
            sha1 = new SHA1CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(str);
            encodedBytes = sha1.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            string hashed = BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
            return hashed;
        }
        public static string sha256(string str)
        {
            byte[] originalBytes;
            byte[] encodedBytes;
            SHA256 sha256;

            //Instantiate MD5CryptoServiceProvider, get bytes for original string and compute hash
            sha256 = new SHA256CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(str);
            encodedBytes = sha256.ComputeHash(originalBytes);

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

        public static string generateID(string prefix)
        {            
            string d = DateTime.Now.ToString("yyMMddHHmmssfff");            
            return prefix + d;
        }

        public static string updateKeo(string type, string keo)
        {
            if (type == "1" && keo == "h")
                return "[FT-Home]";
            else if (type == "1" && keo == "a")
                return "[FT-Away]";
            else if (type == "3" && keo == "h")
                return "[FT-Over]";
            else if (type == "3" && keo == "a")
                return "[FT-Under]";
            else if (type == "7" && keo == "h")
                return "[H1-Home]";
            else if (type == "7" && keo == "a")
                return "[H1-Away]";
            else if (type == "9" && keo == "h")
                return "[H1-Over]";
            else if (type == "9" && keo == "a")
                return "[H1-Under]";
            return "";
        }
        private static string ec(string input, string key)
        {
            string pt = input;
            int[] s = new int[256];
            for (int i = 0; i < 256; i++)
            {
                s[i] = i;
            }

            int j = 0;
            int x;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + (int)key[i % key.Length]) % 256;
                x = s[i];
                s[i] = s[j];
                s[j] = x;
            }
            int k = 0;
            j = 0;
            string ct = "";
            for (var y = 0; y < pt.Length; y++)
            {
                k = (k + 1) % 256;
                j = (j + s[k]) % 256;
                x = s[k];
                s[k] = s[j];
                s[j] = x;
                ct += Convert.ToChar((int)pt[y] ^ s[(s[k] + s[j]) % 256]);
            }
            return ct;
        }

        public static string hc(string input, string key)
        {
            string ct = ec(input, key);
            string b16digits = "0123456789abcdef";
            string[] b16map = new string[256];
            for (var i = 0; i < 256; i++)
            {
                b16map[i] = b16digits[i >> 4].ToString() + b16digits[i & 15].ToString();
            }
            List<string> result = new List<string>();
            for (int j = 0; j < ct.Length; j++)
            {
                result.Add(b16map[(int)ct[j]].ToString());
            }
            return string.Join("", result.ToArray());
        }
        public static string hashSecurityCode(string security_salt,string securitycode_token,string code)
        {
            string result = sha1(code + security_salt);
            result = sha256(result + securitycode_token);
            return result;
        }
    }
}
