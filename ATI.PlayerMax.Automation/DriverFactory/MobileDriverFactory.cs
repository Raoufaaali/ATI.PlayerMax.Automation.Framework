using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ATI.PlayerMax.Automation.DriverFactory
{
    class MobileDriverFactory
    {

        public IWebDriver Get()
        {
            IWebDriver driver = null;
            //string driverName = ConfigurationManager.AppSettings["BROWSER"];
            //string mode = ConfigurationManager.AppSettings["MAM URL"];
            string apkS3Path = "https://playermaxautomation.s3-us-west-2.amazonaws.com/playerMaxSITUSLatest.apk";

            string driverName = "Android";

            string sauceUserName = "Raoufaaali";
            string sauceAccessKey = "e655c8c9-97df-44aa-9cce-c602adf26fa2";
            switch (driverName.ToLower())
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
                        appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, apkS3Path);
                        appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "com.ati.playermax.sit.multi.us.aws.SIT_Multi_US_AWS");
                        appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.ati.playermax.sit.multi.us.aws");
                        appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
                        appiumOptions.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
                        appiumOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
                        appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AutoGrantPermissions, true);
                        appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.ChromedriverExecutable, @"C:\Users\rali\Desktop\Appium\chromedriver.exe");
                        driver = new AndroidDriver<AppiumWebElement>(
                                          new Uri("http://127.0.0.1:4723/wd/hub"),
                                           // new Uri("https://Raoufaaali:e655c8c9-97df-44aa-9cce-c602adf26fa2@ondemand.saucelabs.com:443/wd/hub"),
                                           appiumOptions);

                        
                        break;
                    }
                case "iphone":
                    {
                       
                        break;
                    }
                case "SauceLabsChrome":
                    {
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
