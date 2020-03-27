using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATI.PlayerMax.Automation.Utilities
{
    public class Util
    {

        //private Util()
        //{
        //}

        public static string GetConfigs()
        {
            //TODO
            return "Configs";
        }

        public static void SetupExtentReport(ExtentReports extent)
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(@"C:\Users\rali\Desktop\Automation\AutomationReport\Single Run Report\ExtentReport.html");

            //attach only HtmlReporte          
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("MAM_BROWSER", ConfigurationManager.AppSettings["MAM_BROWSER"]);
            extent.AddSystemInfo("MAM_MODE", ConfigurationManager.AppSettings["MAM_MODE"]);
            extent.AddSystemInfo("MOBILE_DEVICE", ConfigurationManager.AppSettings["MOBILE_DEVICE"]);
            extent.AddSystemInfo("MOBILE_MODE", ConfigurationManager.AppSettings["MOBILE_MODE"]);
            extent.AddSystemInfo("MAM_URL", ConfigurationManager.AppSettings["MAM_URL"]);
            extent.AddSystemInfo("ANDROID_APK", ConfigurationManager.AppSettings["ANDROID_APK"]);
            extent.AddSystemInfo("IOS_IPA", ConfigurationManager.AppSettings["IOS_IPA"]);
            extent.AddSystemInfo("Build", "TODO");
            htmlReporter.Config.DocumentTitle = "PlayerMax Automation";
            htmlReporter.Config.ReportName = "PlayerMax Automated Tests " + DateTime.Now.ToString();
            htmlReporter.Config.Theme = Theme.Dark;
            extent.AttachReporter(htmlReporter);
        }

        public static void SetupHistoricalReport(ExtentReports extent, ExtentKlovReporter klov)
        {
            try
            {
                klov = new ExtentKlovReporter();
                klov.InitMongoDbConnection("10.152.74.249", 27017);
                klov.ProjectName = "PlayerMax MAM Automation";
                klov.ReportName = "PlayerMax MAM Post-Deployment Automated Tests " + DateTime.Now.ToString();
                klov.InitKlovServerConnection("http://10.152.74.249:80");
                extent.AttachReporter(klov);
            }
            catch (Exception e)
            {

            }
            
        }



        public static int UploadToS3()
        {
            //TODO Return 1 for success or 0 for error
            return 0;      
        }


        public static void ConnectToDB()
        {
            // TO DO
        }

    }
}
