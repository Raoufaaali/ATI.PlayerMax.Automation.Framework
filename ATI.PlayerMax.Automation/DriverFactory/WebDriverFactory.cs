using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace ATI.PlayerMax.Automation.DriverFactory
{
    class WebDriverFactory
    {       

        public IWebDriver Get()
        {
            IWebDriver driver = null;
            string driverName =  ConfigurationManager.AppSettings["BROWSER"];
            string mode = ConfigurationManager.AppSettings["MAM URL"];           

            string sauceUserName = "Raoufaaali";
            string sauceAccessKey = "e655c8c9-97df-44aa-9cce-c602adf26fa2";                       
            switch (driverName.ToLower())
            {
                case "chrome":
                    {
                        driver = new ChromeDriver(".");
                        break;
                    }
                case "firefox":
                    {                       
                        driver = new FirefoxDriver(".");
                        break;
                    }
                case "SauceLabsChrome":
                    {
            
           var chromeOptions = new ChromeOptions()
           {
               BrowserVersion = "latest",
               PlatformName = "Windows 10",
               UseSpecCompliantProtocol = true
           };
           var sauceOptions = new Dictionary<string, object>
           {
               ["username"] = sauceUserName,
               ["accessKey"] = sauceAccessKey,
               ["name"] = TestContext.CurrentContext.Test.Name
           };
           chromeOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);

                        driver = new RemoteWebDriver(new Uri("https://ondemand.saucelabs.com/wd/hub"),
               chromeOptions.ToCapabilities(), TimeSpan.FromSeconds(600));     
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return driver;
        }
    }

}
