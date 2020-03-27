using ATI.PlayerMax.Automation.Utilities;
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
            MamBrowser =  Configs.MAM_BROWSER.ToLower();
            MamMode = Configs.MAM_MODE.ToLower();

            //Next, create the driver based on the config file

            if (MamMode == "local")
            {
                switch (MamBrowser.ToLower())
                {
                    case "chrome":
                        {
                            driver = new ChromeDriver();
                            break;
                        }
                    case "firefox":
                        {
                            driver = new FirefoxDriver();
                            break;
                        }

                    default:
                        {
                            driver = new ChromeDriver();
                            break;
                        }
                }
            }


            if (MamMode == "saucelabs")
            {

                string sauceRemoteServer = Configs.SAUCELABS_REMOTESERVER;
                string sauceUserName = Configs.SAUCELABS_USERNAME;
                string sauceAccessKey = Configs.SAUCELABS_ACCESSKEY;
                Uri sauceUrl = new Uri(string.Format("http://{0}:{1}@ondemand.saucelabs.com:80/wd/hub", sauceUserName, sauceAccessKey));

                switch (MamBrowser)
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

                            driver = new RemoteWebDriver(sauceUrl,
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
                //Get Perfecto settings from the environmnet variables
                string securityToken = Configs.PERFECTO_SECURITYTOKEN;
                string perfectoHost = Configs.PERFECTO_HOST;

                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddAdditionalCapability("securityToken", securityToken);
                chromeOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
                chromeOptions.AddAdditionalCapability("browserName", "Chrome");
                Uri url = new Uri(string.Format("https://{0}/nexperience/perfectomobile/wd/hub", perfectoHost));
                driver = new RemoteWebDriver(url, chromeOptions);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }

}
