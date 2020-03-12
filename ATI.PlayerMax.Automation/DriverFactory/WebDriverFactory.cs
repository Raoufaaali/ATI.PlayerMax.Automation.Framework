using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ATI.PlayerMax.Automation.DriverFactory
{
    class WebDriverFactory
    {       

        public IWebDriver Get()
        {
            IWebDriver driver = null;

            //Set the default browser and mode for MAM to Chrome and local respectively then try to obtain the values from the config file
            string MamBrowser = "chrome";
            string MamMode = "local";
            MamBrowser =  ConfigurationManager.AppSettings["MAM_BROWSER"].ToLower();
            MamMode = ConfigurationManager.AppSettings["MAM_MODE"].ToLower();

            //Next, create the driver based on the config file

            if (MamMode == "local")
            {
                switch (MamBrowser.ToLower())
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

                    default:
                        {
                            driver = new ChromeDriver(".");
                            break;
                        }
                }
            }


            if (MamMode == "saucelabs")
            {
                //string sauceUserName = "Raoufaaali";
                //string sauceAccessKey = "e655c8c9-97df-44aa-9cce-c602adf26fa2";
                string sauceRemoteServer = ConfigurationManager.AppSettings["SAUCELABS_REMOTESERVER"];
                string sauceUserName = ConfigurationManager.AppSettings["SAUCELABS_USERNAME"];
                string sauceAccessKey = ConfigurationManager.AppSettings["SAUCELABS_PASSWORDS"];
                
                switch (MamBrowser.ToLower())
                {
                    case "chrome":
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
                            //TODO what to do if the mode was Saucelabs and the browser was set to something strange?
                            break;
                        }
                }

            }


            //TODO Pefecto mode
            if (MamMode == "perfecto")
            {
                //Return a driver that is pointed to Pefecto lab
            }

            return driver;
        }
    }

}
