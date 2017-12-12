using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScanOdds
{
    public class SeleniumHelper
    {
        public IWebDriver driver;

        public SeleniumHelper()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(driverService, options);
        }

        public void GotoURL(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        public ICookieJar GetCookie()
        {
            return driver.Manage().Cookies;
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public void Close()
        {
            driver.Close();
            driver = null;
        }

        public string GetRootUrl(string type)
        {
            string url = "";
            int index = 0;
            if (type == "http")
            {
                url = driver.Url;
                index = url.IndexOf("/", "http://".Length);
            }
            else if (type == "https")
            {
                url = driver.Url;
                index = url.IndexOf("/", "https://".Length);
            }
            return url.Substring(0, index);
        }

        public string GetUrl(int numberslash)
        {
            string url = driver.Url;
            int index = 0;
            for (int i = 0; i < numberslash; i++)
            {
                index = url.IndexOf("/", index);
                index++;
            }
            return driver.Url.Substring(0, index);
        }

        public void GoToFrame(string xpath)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(this.FindElement(By.XPath(xpath)));
        }

        public void WaitForElementDisplay(string[] input, int seconds)
        {
            IWebElement element = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                if (input[0].ToLower() == "id")
                    element = wait.Until(drv => drv.FindElement(By.Id(input[1])));
                else if (input[0].ToLower() == "name")
                    element = wait.Until(drv => drv.FindElement(By.Name(input[1])));
                else
                    element = wait.Until(drv => drv.FindElement(By.XPath(input[1])));
            }
            catch (Exception)
            {

            }
        }

        public IWebElement FindElement(By by, int timeout = 30)
        {
            IWebElement element = null;
            Stopwatch s = new Stopwatch();
            if (timeout > 0)
            {
                try
                {
                    s.Start();
                    element = driver.FindElement(by);
                }
                catch (Exception ex)
                {
                    try { Thread.Sleep(500); }
                    catch { }

                    s.Stop();
                    timeout = (int)(timeout - s.Elapsed.TotalSeconds);
                    element = FindElement(by, timeout);
                }
            }
            return element;
        }

        public void SendKeys(IWebElement element, string input)
        {
            element.Clear();
            element.SendKeys(input);
        }

        public void CaptureScreenShot(IWebElement element)
        {
            Byte[] byteArray = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(new System.IO.MemoryStream(byteArray));
            System.Drawing.Rectangle croppedImage = new System.Drawing.Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
            screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
            screenshot.Save(String.Format(@"D:\test.jpg", System.Drawing.Imaging.ImageFormat.Jpeg));
        }
    }
}
