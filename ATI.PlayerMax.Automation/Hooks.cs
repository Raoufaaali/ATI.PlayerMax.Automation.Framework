using ATI.PlayerMax.Automation.DriverFactory;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using OpenQA.Selenium;
using System;
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
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentTest _test;

        public Hooks(IObjectContainer container)
        {
            this.container = container;
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
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (driver != null)
            {
                driver.Quit();
            }

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

            klov = new ExtentKlovReporter();
            klov.InitMongoDbConnection("localhost", 27017);
            klov.ProjectName = "PlayerMax MAM Automation";
            klov.ReportName = "PlayerMax MAM Post-Deployment Automated Tests " + DateTime.Now.ToString();
            klov.InitMongoDbConnection("localhost", 27017);
            klov.InitKlovServerConnection("http://localhost:90");

      
          
            extent.AttachReporter(htmlReporter, klov);
          
        }


        [AfterTestRun]
        public static void TearDownReport()
        {
 
            extent.Flush();
        }


        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);           
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "When")
                                scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "Then")
                                scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if(stepType == "And")
                                scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if(ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "Then") {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
                else if(stepType == "And")
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                }
            }
        }
    }
}
