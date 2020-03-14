using ATI.PlayerMax.Automation.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using Reportium.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Policy;
using System.Text;

namespace ATI.PlayerMax.Automation.DriverFactory
{
    class MobileDriverFactory
    {



        public IWebDriver Get()
        {

            IWebDriver driver = null;
           
            //Set the default browser and mode for MAM to Chrome and local respectively then try to obtain the values from the config file
            string mobileDevice = "android";
            string mobileMode = "local";
            mobileDevice = Configs.MOBILE_DEVICE.ToLower();
            mobileMode = Configs.MOBILE_MODE.ToLower();

            string apiPath = Configs.ANDROID_APK;

            //Next, create the driver based on the config file

            if (mobileMode == "local")
            {
                switch (mobileDevice.ToLower())
                {
                    case "android":
                        {
                            AppiumOptions appiumOptions = new AppiumOptions();
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Nexus_5");
                            // appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel GoogleAPI Emulator");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
                            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
                            //appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
                            //  appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, "sauce-storage:PlayerMaxSIT");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, Configs.ANDROID_APK);
                            //appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.ati.playermax.sit.multi.us.aws.SIT_Multi_US_AWS");
                            //appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.ati.playermax.sit.multi.us.aws");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AutoGrantPermissions, true);
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.ChromedriverExecutable, @"C:\Users\rali\Desktop\Automation\Appium\chromedriver.exe");
                            driver = new AndroidDriver<AppiumWebElement>(
                                              new Uri("http://127.0.0.1:4723/wd/hub"),
                                               // new Uri("https://Raoufaaali:e655c8c9-97df-44aa-9cce-c602adf26fa2@ondemand.saucelabs.com:443/wd/hub"),
                                               appiumOptions);
                            break;
                         
                        }
                    case "ios":
                        {
                            //TODO
                            break;
                        }

                    default:
                        {
                            //TODO
                            break;
                        }
                }
            }


            if (mobileMode == "perfecto")
            {
                //Get Perfecto settings from the environmnet variables
                string securityToken = Configs.PERFECTO_SECURITYTOKEN;
                string perfectoHost = Configs.PERFECTO_HOST;
                switch (mobileDevice.ToLower())
                {
                    case "android":
                        {                          
                            AppiumOptions appiumOptions = new AppiumOptions();
                            
                            appiumOptions.AddAdditionalCapability("securityToken", securityToken);
                            appiumOptions.AddAdditionalCapability("platformName", "Android");
                            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, Configs.ANDROID_APK);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AutoGrantPermissions, true);

                            Uri url = new Uri(string.Format("https://{0}/nexperience/perfectomobile/wd/hub", perfectoHost));
                            driver = new AndroidDriver<IWebElement>(url, appiumOptions);

                            break;                                                              
                        }
                    case "ios":
                        {
                            AppiumOptions appiumOptions = new AppiumOptions();

                            appiumOptions.AddAdditionalCapability("securityToken", securityToken);
                            appiumOptions.AddAdditionalCapability("platformName", "iOS");
                            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, Configs.IOS_IPA);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                            appiumOptions.AddAdditionalCapability(IOSMobileCapabilityType.AutoAcceptAlerts, true);

                            Uri url = new Uri(string.Format("https://{0}/nexperience/perfectomobile/wd/hub", perfectoHost));
                            driver = new IOSDriver<IWebElement>(url, appiumOptions);

                            break;

                        }

                    default:
                        {
                            //TODO
                            break;
                        }
                }
            }


            if (mobileMode == "saucelabs")
            {             
             

                string sauceRemoteServer = Configs.SAUCELABS_REMOTESERVER;
                string sauceUserName = Configs.SAUCELABS_USERNAME;
                string sauceAccessKey = Configs.SAUCELABS_ACCESSKEY;

                Uri sauceUrl = new Uri(string.Format("http://{0}:{1}@ondemand.saucelabs.com:80/wd/hub", sauceUserName, sauceAccessKey));
                
                switch (mobileDevice.ToLower())
                {
                    case "android":
                        {
                            AppiumOptions appiumOptions = new AppiumOptions();
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Google Pixel 3 XL GoogleAPI Emulator");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "10.0");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, Configs.ANDROID_APK);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.ati.playermax.sit.multi.us.aws.SIT_Multi_US_AWS");
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.ati.playermax.sit.multi.us.aws");
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                            appiumOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AutoGrantPermissions, true);
                            appiumOptions.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name);

                            driver = new RemoteWebDriver(sauceUrl,
                                appiumOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
                            break;
                        }

                    case "ios":
                        {
                            //TODO
                            break;
                        }

                    default:
                        {
                            //TODO
                            break;
                        }
                }         

            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            return driver;
        }


    }
}
