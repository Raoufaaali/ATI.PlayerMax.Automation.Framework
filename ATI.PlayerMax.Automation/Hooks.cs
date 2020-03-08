using ATI.PlayerMax.Automation.DriverFactory;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Reflection;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation
{
    [Binding]
    class Hooks
    {
        private IWebDriver driver;
        private IWebDriver mobileDriver;
        private readonly IObjectContainer container;

        //Variables for Extend report
        public static AventStack.ExtentReports.ExtentReports extent;
        private static ExtentKlovReporter klov;
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentTest _test;
        private static  FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;  

        public Hooks(IObjectContainer container,  ScenarioContext scenarioContext)
        {
            this.container = container;
            _scenarioContext = scenarioContext;
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
            htmlReporter.Config.ReportName = "PlayerMax MAM Post-Deployment Automated Tests " + DateTime.Now.ToString();
            htmlReporter.Config.Theme = Theme.Dark;

            // htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //TODO 
            // Generate historical reporting:

            /* klov = new ExtentKlovReporter();
             klov.InitMongoDbConnection("localhost", 27017);
             klov.ProjectName = "PlayerMax MAM Automation";
             klov.ReportName = "PlayerMax MAM Post-Deployment Automated Tests " + DateTime.Now.ToString();
             klov.InitMongoDbConnection("localhost", 27017);
             klov.InitKlovServerConnection("http://localhost:90");
             extent.AttachReporter(htmlReporter, klov);
             */
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            //Create feature name
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
        }
    

        [BeforeScenario]
        public void SetThingsUp()
        {            
            //Here, initilize the driver from the factory for web and mobile and create the reporter..etc
            WebDriverFactory webDriverFactory = new WebDriverFactory();
            driver = webDriverFactory.Get();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            // MobileDriverFactory mobileDriverFactory = new MobileDriverFactory();
            // mobileDriver = mobileDriverFactory.Get();
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            container.RegisterInstanceAs<IWebDriver>(driver);

            //Create scenario name dynamically for extend reports
            // scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);

            //Create feature name
            //featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            //Get scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        //[AfterScenario]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            extent.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.ToString());

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {

                extent.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.FullName).Fail("Failllll");
            }

            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                extent.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.FullName).Pass("Passsssssss");
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
            {
                extent.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.FullName).Skip("Skippedddddd");
            }

            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Inconclusive)
            {
                extent.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.FullName).Skip("Skippedddddd");
            }


        }

        [AfterScenario]
        public void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            string status = _scenarioContext.ScenarioExecutionStatus.ToString();

            if (status == "StepDefinitionPending" || status == "UndefinedStep" || status == "Skipped")
            {
                scenario.Skip("Skipped");
            }
        }               


        [AfterTestRun]
        public static void TearDownReport()
        { 
            extent.Flush();
        }


        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string status = _scenarioContext.ScenarioExecutionStatus.ToString();

            if (status == "TestError")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);

            }

            if (status == "OK")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("Passed");
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass("Passed");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass("Passed");

            }

             if (status == "StepDefinitionPending" || status == "UndefinedStep" || status == "Skipped")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Skip(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Skip(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Skip(_scenarioContext.TestError.InnerException);

            }            

        }
        
    }


}
