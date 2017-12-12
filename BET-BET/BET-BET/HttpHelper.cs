using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BET_BET
{
    public class HttpHelper
    {
        public string error = "";
        public enum HttpMethod
        {
            Post,
            Get
        }

        /// <summary>
        /// HTTP helper
        /// </summary>
        private CookieCollection cookies;
        public CookieContainer cookieContainer { get; set; }
        //mimic the Firefox agent
        //Mozilla/5.0 (Windows NT 6.1; WOW64; rv:16.0) Gecko/20100101 Firefox/16.0
        //
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";
        private const string DF_ACCEPT = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        private const string DF_CONTENT_TYPE = "application/x-www-form-urlencoded";
        private string accept;
        private bool keepAlive;
        private string contentType;

        private Encoding responseEncoding;
        private Encoding postDataEncoding;
        private readonly object _cookielocker = new Object();
        private bool setcookie = false;

        /// <summary>
        /// Http accepted content
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        /// <summary>
        /// Keep Alive
        /// </summary>
        public bool KeepAlive
        {
            get { return keepAlive; }
            set { keepAlive = value; }
        }

        /// <summary>
        /// Content Type of HTTP request
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        public Encoding ResponseEncoding
        {
            get
            {
                return this.responseEncoding;
            }
            set
            {
                responseEncoding = value;
            }
        }

        public Encoding PostDataEncoding
        {
            get
            {
                return this.postDataEncoding;
            }
            set
            {
                postDataEncoding = value;
            }
        }

        public HttpHelper()
        {
            ServicePointManager.DefaultConnectionLimit = 24;
            ServicePointManager.UseNagleAlgorithm = false;
            System.Net.ServicePointManager.Expect100Continue = false;
            lock (_cookielocker)
            {
                cookies = new CookieCollection();
                cookieContainer = new CookieContainer();
                //RNRVersion-d634b34b62a4586058088ba69be89dff=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-d634b34b62a4586058088ba69be89dff=yes; KeepOdds-d634b34b62a4586058088ba69be89dff=no; OddsType_SDV3234001=4; RNRVersion-832bd0328111916e12925c379db89cea=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-832bd0328111916e12925c379db89cea=yes; KeepOdds-832bd0328111916e12925c379db89cea=no; RNRVersion-81ada4a7e2e083f9c0d775cb999956ae=4bb05b5df2463b26cff0f519407df099; OddsType_ECWB17010=4; AcceptBetterOdds-81ada4a7e2e083f9c0d775cb999956ae=yes; KeepOdds-81ada4a7e2e083f9c0d775cb999956ae=no; RNRVersion-67ceed5cfd86ed29ca24421361073151=4bb05b5df2463b26cff0f519407df099; OddsType_SHDAAC1003=4; AcceptBetterOdds-67ceed5cfd86ed29ca24421361073151=yes; KeepOdds-67ceed5cfd86ed29ca24421361073151=no; RNRVersion-abb9b10cc26aeb40aaade869e938de4d=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-abb9b10cc26aeb40aaade869e938de4d=yes; KeepOdds-abb9b10cc26aeb40aaade869e938de4d=no; OddsType_SDV3208JS7=4; RNRVersion-9c2043b6a12d8dfb60efc5a7d89d2b1e=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-9c2043b6a12d8dfb60efc5a7d89d2b1e=yes; KeepOdds-9c2043b6a12d8dfb60efc5a7d89d2b1e=no; OddsType_SDV3234002=4; RNRVersion-6223c4c89aef96e6fed0fd9757dc8a59=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-6223c4c89aef96e6fed0fd9757dc8a59=yes; KeepOdds-6223c4c89aef96e6fed0fd9757dc8a59=no; OddsType_SDV3234009=4; RNRVersion-0f5d8062815f1a4d7df041a4653e1524=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-0f5d8062815f1a4d7df041a4653e1524=yes; KeepOdds-0f5d8062815f1a4d7df041a4653e1524=no; OddsType_SDV3234012=4; RNRVersion-a5c699c1c71f5ae6749afe007dbf006e=4bb05b5df2463b26cff0f519407df099; AcceptBetterOdds-a5c699c1c71f5ae6749afe007dbf006e=yes; KeepOdds-a5c699c1c71f5ae6749afe007dbf006e=no; OddsType_SDV3234014=4; DispVer=3; MiniKey=max; ASP.NET_SessionId=pqosxq55xfai1l2r33klf155
            }
            this.accept = DF_ACCEPT;
            this.contentType = DF_CONTENT_TYPE;
            this.keepAlive = true;
            this.responseEncoding = Encoding.UTF8;
            this.postDataEncoding = Encoding.GetEncoding(1252);
        }

        //public void SetCookie(ICookieJar jar)
        //{
        //    for (int i = 0; i < jar.AllCookies.Count; i++)
        //    {
        //        System.Net.Cookie c = new System.Net.Cookie(jar.AllCookies[i].Name, jar.AllCookies[i].Value);
        //        cookies.Add(c);
        //    }
        //    setcookie = true;
        //}

        public string Fetch(string url, HttpMethod method, string referer, string postData, string ip = "", string Adds = "")
        {
            string r = string.Empty;
            try
            {
                using (HttpWebResponse res = FetchResponse(url, method, referer, postData, ip, Adds))
                {
                    if (res != null)
                    {
                        using (Stream ressr = res.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(ressr, responseEncoding, true))
                            {
                                r = sr.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        r = error;
                    }
                }
            }
            catch (Exception)
            {
                //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
            }
            return r;
        }

        private void CloseResponse(HttpWebResponse res)
        {
            if (res != null)
                res.Close();
        }

        private HttpWebResponse FetchResponse(string url, HttpMethod method, string referer, string postData, string ip = "", string Adds = "")
        {
            string Host = Util.GetSubstringByString(url, "http://", ".com").Replace("http://", "") + ".com";
            HttpWebResponse res = null;
            WebRequest.DefaultWebProxy = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.ContentType = "application/x-www-form-urlencoded";

            req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            if (ip != "")
                req.Headers.Add("X-Forwarded-For", ip);
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us,en;q=0.5");
            req.Headers.Add(HttpRequestHeader.KeepAlive, "true");
            req.Headers.Add("Upgrade-Insecure-Requests", "1");

            //req.Host = Host;
            if (Adds != "")
            {
                string[] arr_Add = Adds.Split('&');
                foreach (string Add in arr_Add)
                {
                    if (Add.Contains("ContentLength"))
                    {
                        req.ContentLength = int.Parse(Add.Split('=')[1]);
                        continue;
                    }
                    if (Add.Contains("Host"))
                    {
                        req.Host = Add.Split('=')[1];
                        continue;
                    }
                    req.Headers.Add(Add.Split('=')[0], Add.Split('=')[1]);
                }
            }

            if (!string.IsNullOrEmpty(referer))
                req.Referer = referer;
            lock (_cookielocker)
            {
                if (setcookie)
                {
                    req.CookieContainer = new CookieContainer();
                    req.CookieContainer.Add(new Uri(url), cookies);
                }
                else
                {
                    req.CookieContainer = cookieContainer;
                }
            }
            req.Method = method.ToString().ToUpper();
            if (method == HttpMethod.Post)
            {
                //set up the post data to be sent
                if (!string.IsNullOrEmpty(postData))
                {
                    byte[] buf = postDataEncoding.GetBytes(postData);
                    req.ContentLength = buf.Length;
                    Stream poststr = req.GetRequestStream();
                    poststr.Write(buf, 0, buf.Length);
                    poststr.Close();
                }
            }

            //get response
            res = (HttpWebResponse)req.GetResponse();
            //get response


            res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
            // Add the cookie to this instance of object, such that the cookies can be reused
            // for next request
            lock (_cookielocker)
            {
                if (setcookie)
                {
                    cookies = res.Cookies;
                    cookieContainer = req.CookieContainer;
                }
                else
                {
                    cookies.Add(res.Cookies);
                    cookieContainer.Add(cookies);
                }
            }
            return res;
        }

        public Image FetchImage(string url, HttpMethod method, string referer, string postData)
        {
            Image img = null;
            try
            {
                //get the response stream
                using (HttpWebResponse res = FetchResponse(url, method, referer, postData))
                {
                    if (res != null)
                    {
                        using (Stream sr = res.GetResponseStream())
                        {
                            using (StreamReader resStream = new StreamReader(sr, responseEncoding, true))
                            {
                                img = Image.FromStream(resStream.BaseStream);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
                string haha = Fetch(url, method, referer, postData);
                //EngineLogger.Log("FK==>>>", haha, LogMode.Normal, StaticLog.Main);
            }
            return img;
        }

        public string FetchResponseUri(string url, HttpMethod method, string referer, string postData, string ip = "")
        {
            string uri = string.Empty;
            try
            {
                using (HttpWebResponse res = FetchResponse(url, method, referer, postData, ip))
                {
                    if (res != null)
                    {
                        uri = res.ResponseUri.AbsoluteUri;
                    }
                }
            }
            catch (Exception)
            {
                //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
            }
            return uri;
        }

        public void ClearCookies()
        {
            lock (_cookielocker)
            {
                cookies = new CookieCollection();
                cookieContainer = new CookieContainer();
            }
        }

        //public string FetchWithProxy(string url, HttpMethod method, string referer, string postData)
        //{
        //    string r = string.Empty;
        //    try
        //    {
        //        using (HttpWebResponse res = FetchResponseWithProxy(url, method, referer, postData))
        //        {
        //            if (res != null)
        //            {
        //                using (Stream ressr = res.GetResponseStream())
        //                {
        //                    using (StreamReader sr = new StreamReader(ressr, responseEncoding, true))
        //                    {
        //                        r = sr.ReadToEnd();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
        //    }
        //    return r;
        //}

        //public Image FetchImageWithProxy(string url, HttpMethod method, string referer, string postData)
        //{
        //    Image img = null;
        //    try
        //    {
        //        //get the response stream
        //        using (HttpWebResponse res = FetchResponseWithProxy(url, method, referer, postData))
        //        {
        //            if (res != null)
        //            {
        //                using (Stream sr = res.GetResponseStream())
        //                {
        //                    using (StreamReader resStream = new StreamReader(sr, responseEncoding, true))
        //                    {
        //                        img = Image.FromStream(resStream.BaseStream);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
        //        string haha = Fetch(url, method, referer, postData);
        //        //EngineLogger.Log("FK==>>>", haha, LogMode.Normal, StaticLog.Main);
        //    }
        //    return img;
        //}

        //public string FetchResponseUriWithProxy(string url, HttpMethod method, string referer, string postData)
        //{
        //    string uri = string.Empty;
        //    try
        //    {
        //        using (HttpWebResponse res = FetchResponseWithProxy(url, method, referer, postData))
        //        {
        //            if (res != null)
        //            {
        //                uri = res.ResponseUri.AbsoluteUri;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
        //    }
        //    return uri;
        //}

        //private HttpWebResponse FetchResponseWithProxy(string url, HttpMethod method, string referer, string postData)
        //{
        //    HttpWebResponse res = null;
        //    try
        //    {
        //        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
        //        {
        //            //don't use proxy
        //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //            request.Proxy = new WebProxy(BCCore.BetEngineStaticConfiguration.ProxyIP, BCCore.BetEngineStaticConfiguration.ProxyPort);
        //            request.UserAgent = USER_AGENT;
        //            request.Accept = accept;
        //            request.KeepAlive = keepAlive;
        //            request.ContentType = contentType;
        //            if (!string.IsNullOrEmpty(referer))
        //                request.Referer = referer;
        //            lock (_cookielocker)
        //            {
        //                request.CookieContainer = cookieContainer;
        //            }
        //            request.Method = method.ToString().ToUpper();
        //            if (method == HttpMethod.Post)
        //            {
        //                //set up the post data to be sent
        //                if (!string.IsNullOrEmpty(postData))
        //                {
        //                    byte[] buf = postDataEncoding.GetBytes(postData);
        //                    request.ContentLength = buf.Length;
        //                    Stream poststr = request.GetRequestStream();
        //                    poststr.Write(buf, 0, buf.Length);
        //                    poststr.Close();
        //                }
        //            }
        //            //Convert to string
        //            //HttpCookieCollection source = cookies;
        //            //string result = source.Cast<HttpCookie>().
        //            //                Aggregate(string.Empty, (current, cookie) =>
        //            //                current + string.Format("{0}={1} ", cookie.Name, cookie.Value));
        //            //string result = "";
        //            //foreach (Cookie c in cookies)
        //            //{
        //            //    result += string.Format("{0}={1} ", c.Name, c.Value);
        //            //}
        //            //System.Windows.Forms.MessageBox.Show(result);
        //            ////Convert back to collection
        //            //HttpCookieCollection dest = new HttpCookieCollection();
        //            //foreach (var pair in result.Split(' '))
        //            //{
        //            //    string[] cookies = pair.Split('=');
        //            //    dest.Add(new HttpCookie(cookies[0], cookies[1]));
        //            //}
        //            //get response
        //            res = (HttpWebResponse)request.GetResponse();
        //            res.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
        //            // Add the cookie to this instance of object, such that the cookies can be reused
        //            // for next request
        //            lock (_cookielocker)
        //            {
        //                cookies.Add(res.Cookies);
        //                cookieContainer.Add(cookies);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CloseResponse(res);
        //        res = null;
        //        //EngineLogger.Log(ex, LogMode.Normal, StaticLog.Main);
        //    }
        //    return res;
        //}
    }
}
