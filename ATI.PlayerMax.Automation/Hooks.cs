using ATI.PlayerMax.Automation.DriverFactory;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation
{
    [Binding]
    class Hooks
    {
        private IWebDriver driver;
        private readonly IObjectContainer container;
        protected ExtentTest _test;

        //Global Variable for Extend report
        public static AventStack.ExtentReports.ExtentReports extent;
        private static ExtentKlovReporter klov;
        private static ExtentTest featureName;
        private static ExtentTest scenario;


        public Hooks(IObjectContainer container)
        {
            this.container = container;
        }

        [BeforeScenario]
        public void SetThingsUp()
        {
            _test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            //Here, initilize the driver from the factory and create the reporter..etc
            WebDriverFactory webDriverFactory = new WebDriverFactory();
            driver = webDriverFactory.Get();                
         
            // Make 'driver' available for DI
            container.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }

        }

        [AfterScenario]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            //extent.Flush();
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //initialize the HtmlReporter

            //ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(@"C:\Users\rali\Desktop\AutomationReport\ExtentReport.html");
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(@"C:\Users\rali\Desktop\AutomationReport\ExtentReport.html");

            //attach only HtmlReporte
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("MAM URL", "https://playermax-sit.aristocrat.systems");
            extent.AddSystemInfo("Browser", "Chrome");
            htmlReporter.Config.DocumentTitle = "PlayerMax Automation";
            htmlReporter.Config.ReportName = "PlayerMax Automation WhiteLabel";
            htmlReporter.Config.Theme = Theme.Dark;

            // htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //TODO 
            // Generate historical reporting:

            /*
            klov = new ExtentKlovReporter();
            klov.InitMongoDbConnection("localhost", 27017);
            klov.ProjectName = "PlayerMax Automation";
            //URL of the KLOV server
            klov.KlovUrl = "http://localhost:5689";
            klov.ReportName = "Automation Report" + DateTime.Now.ToString();
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter, klov);
            */

        }


        [AfterTestRun]
        public static void TearDownReport()
        {
            string test = "I ran";           
            extent.Flush();
        }


        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            //featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }
    }
}
