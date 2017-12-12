using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BET_BET
{
    public class Util
    {
        public static string GetSubstringByString(string text, string a, string b)
        {
            try
            {
                if (text != "")
                {
                    int begin_a = text.IndexOf(a);
                    return text.Substring((begin_a + a.Length), (text.IndexOf(b, begin_a + a.Length) - begin_a - a.Length)).Trim();
                }
            }
            catch (Exception)
            {
                return text;
            }
            return text;
        }
        public static string GetSubstringByStringLast(string text, string a, string b)
        {
            int lasta = text.Substring(0, text.IndexOf(b)).LastIndexOf(a);
            return text.Substring(lasta + a.Length, (text.IndexOf(b) - lasta - a.Length)).Trim();
        }
        public static string GetSubstringByStringLastLast(string text, string a, string b)
        {
            int lasta = text.Substring(0, text.LastIndexOf(b)).LastIndexOf(a);
            return text.Substring(lasta + a.Length, (text.LastIndexOf(b) - lasta - a.Length)).Trim();
        }

        public static string GetSubstringByStringOptimize(string text, string a, string b)
        {
            return text.Substring((text.IndexOf(a) + a.Length), (text.IndexOf(b, text.IndexOf(a)) - text.IndexOf(a) - a.Length)).Trim();
        }

        public static string EscapeJson(string input)
        {
            return input.Replace("&#39;", "%27").Replace(":", "%3A").Replace("[", "%5B").Replace("{", "%7B").Replace("]", "%5D").Replace("}", "%7D").Replace(",", "%2C").Replace("&amp;", "&").Replace(" ", "%20");
        }

        public static string EscapeDataString(string input)
        {
            return Uri.EscapeDataString(input);
        }

        public static string UnescapeDataString(string input)
        {
            return Uri.UnescapeDataString(input);
        }

        public static string HtmlGetAttributeValue(string content, string attributeName, string xpath)
        {
            string ret = "";
            try
            {
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.OptionFixNestedTags = true;
                html.LoadHtml(content);
                HtmlNode node = html.DocumentNode.SelectSingleNode(xpath);
                ret = node.Attributes[attributeName].Value;
            }
            catch (Exception)
            {
                ret = "";
            }
            return ret;
        }

        public static string HtmlGetInnerText(string content, string xpath)
        {
            string ret = "";
            try
            {
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.OptionFixNestedTags = true;
                html.LoadHtml(content);
                HtmlNode node = html.DocumentNode.SelectSingleNode(xpath);
                ret = node.InnerText;
            }
            catch (Exception)
            {
                ret = "";
            }
            return ret;
        }

        public static string HtmlGetInnerText(string content, string xpath, string split)
        {
            string ret = "";
            try
            {
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.OptionFixNestedTags = true;
                html.LoadHtml(content);
                HtmlNodeCollection nodes = html.DocumentNode.SelectNodes(xpath);

                foreach (HtmlNode node in nodes)
                {
                    ret += node.InnerText + split;
                }
            }
            catch (Exception)
            {
                ret = "";
            }
            return ret;
        }

        public static HtmlNodeCollection HtmlGetNodeCollection(string content, string xpath)
        {
            HtmlNodeCollection ret = null;
            try
            {
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.OptionFixNestedTags = true;
                html.LoadHtml(content);
                ret = html.DocumentNode.SelectNodes(xpath);
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }

        public static void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            int len = end - start;
            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, len);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }
    }
}
