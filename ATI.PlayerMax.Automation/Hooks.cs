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
using System.Linq;
using System.Configuration;
using TestContext = NUnit.Framework.TestContext;
using Reportium.Client;
using Reportium.Model;
using Reportium.Test.Result;
using ATI.PlayerMax.Automation.Utilities;

namespace ATI.PlayerMax.Automation
{
    [Binding]
    class Hooks
    {
        private IWebDriver driver;
        private readonly IObjectContainer container;

        //Variables for Extend report
        public static ExtentReports extent;
        private static ExtentKlovReporter klov;
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private  ExtentTest scenario;
        private static FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public static ReportiumClient reportiumClient;
        Reportium.Test.TestContext testContext;

        public Hooks(IObjectContainer container,  ScenarioContext scenarioContext)
        {
            this.container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //initialize the single run report
            extent = new ExtentReports();
            Util.SetupExtentReport(extent);

            //initilize the historical reports
            klov = new ExtentKlovReporter();
            Util.SetupHistoricalReport(extent,klov);            
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            //Create feature name for reporting for local mode
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);      
        }

        [BeforeScenario]
        public void SetThingsUp()
        {
            //Get scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

            string[] tagsArray = _scenarioContext.ScenarioInfo.Tags;
            var isMobile = tagsArray.Contains("mobile");
            testContext = new Reportium.Test.TestContext(tagsArray);

            if (isMobile)
            {
                MobileDriverFactory mobileDriverFactory = new MobileDriverFactory();

                try {
                    driver = mobileDriverFactory.Get();
                }
                catch (Exception e)
                {
                    scenario.Fail(e.Message);
                    scenario.Info(e.Message);
                    scenario.Log(Status.Error, "Logging an error");
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }


                if (Configs.MOBILE_MODE.ToLower() == "perfecto")
                {
                    reportiumClient = CreateReportingClient(driver);
                    reportiumClient.TestStart(_scenarioContext.ScenarioInfo.Title, testContext);
                }

                container.RegisterInstanceAs<IWebDriver>(driver);
            }

            else
            {
                //Here, initilize the driver from the factory for web and mobile and create the reporter..etc
                WebDriverFactory webDriverFactory = new WebDriverFactory();
                driver = webDriverFactory.Get();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                container.RegisterInstanceAs<IWebDriver>(driver);

            }            
        }


        [AfterScenario]
        public void CloseDriver()
        {           

            string status = _scenarioContext.ScenarioExecutionStatus.ToString();

            if (status == "StepDefinitionPending" || status == "UndefinedStep" || status == "Skipped")
            {
                scenario.Skip("Skipped");
            }

            if (status == "TestError")
            {
                scenario.Fail(_scenarioContext.TestError.Message);
            }


            //Perfeco reporting if perfeco is used
            string[] tagsArray = _scenarioContext.ScenarioInfo.Tags;
            var isMobile = tagsArray.Contains("mobile");
            if (isMobile)
            {
                if (Configs.MOBILE_MODE.ToLower() == "perfecto")
                {
                    if (status == "OK")
                    {
                        reportiumClient.TestStop(TestResultFactory.CreateSuccess());
                    }

                    if (status == "TestError")
                    {
                        reportiumClient.TestStop(TestResultFactory.CreateFailure(_scenarioContext.TestError.Message, _scenarioContext.TestError.InnerException));
                    }
                }

            }      


            if (driver != null)
            {
                driver.Quit();
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
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("Ok");
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass("Ok");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass("Ok");          
                               
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

        private static ReportiumClient CreateReportingClient(IWebDriver driver)
        {
                PerfectoExecutionContext perfectoExecutionContext = new PerfectoExecutionContext.PerfectoExecutionContextBuilder()
               //.WithProject(new Project("Perfecto Sample Project", "v1.0")) //optional
               //.WithContextTags(new[] { "Perfecto", "Sample", "C#" }) //optional
               //.WithJob(new Job("Sample C# Job", 1)) //optional
               .WithWebDriver(driver)
               .Build();
            return PerfectoClientFactory.CreatePerfectoReportiumClient(perfectoExecutionContext);

        }
        
    }




}
