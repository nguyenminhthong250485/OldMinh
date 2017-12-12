using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace HTTP_AGENT
{
    public class mylib
    {
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

        private static string e(string q)
        {
            string r = "";
            for (int o = 1; o < q.Length; o++)
            {
                r += ((int)(q[o - 1].ToString())[0]).ToString();
            }
            return r;
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

        private static DateTime GetDummyDate()
        {
            return new DateTime(1000, 1, 1); //to check if we have an online date or not.
        }

        public static DateTime GetNistTime()
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            DateTime date = GetDummyDate();
            string serverResponse = string.Empty;

            // Represents the list of NIST servers
            string[] servers = new string[] {
                                                "nist1-ny.ustiming.org",
                                                "time-a.nist.gov",
                                                "nist1-chi.ustiming.org",
                                                "time.nist.gov",
                                                "ntp-nist.ldsbc.edu",
                                                "nist1-la.ustiming.org"
                                            };

            // Try each server in random order to avoid blocked requests due to too frequent request
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    // Open a StreamReader to a random time server
                    StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[ran.Next(0, servers.Length)], 13).GetStream());
                    serverResponse = reader.ReadToEnd();
                    reader.Close();

                    // Check to see that the signature is there
                    if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
                    {
                        // Parse the date
                        int jd = int.Parse(serverResponse.Substring(1, 5));
                        int yr = int.Parse(serverResponse.Substring(7, 2));
                        int mo = int.Parse(serverResponse.Substring(10, 2));
                        int dy = int.Parse(serverResponse.Substring(13, 2));
                        int hr = int.Parse(serverResponse.Substring(16, 2));
                        int mm = int.Parse(serverResponse.Substring(19, 2));
                        int sc = int.Parse(serverResponse.Substring(22, 2));

                        if (jd > 51544)
                            yr += 2000;
                        else
                            yr += 1999;

                        date = new DateTime(yr, mo, dy, hr, mm, sc);

                        // Exit the loop
                        break;
                    }
                }
                catch
                {
                    /* Do Nothing...try the next server */
                }
            }
            return date;
        }
    }
}
