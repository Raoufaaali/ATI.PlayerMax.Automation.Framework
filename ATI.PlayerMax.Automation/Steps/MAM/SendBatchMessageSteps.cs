using ATI.PlayerMax.Automation.DriverFactory;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation.Steps.MAM
{
    //[Binding, Scope(Feature = "SendBatchMessage")]
    [Binding]
    public class SendBatchMessageSteps
    {
        private IWebDriver _driver;
        string PlayerMaxMAMURL = "https://playermax-sit.aristocrat.systems";
       
                   
        public SendBatchMessageSteps(IWebDriver driver)
        {
            _driver = driver;
        }



        [Given(@"I have logged into PlayerMax Administrator website and navigated to Configuration > Batch  Messages")]
        public void GivenIHaveLoggedIntoPlayerMaxAdministratorWebsiteAndNavigatedToConfigurationBatchMessages()
        {
            _driver.Navigate().GoToUrl(PlayerMaxMAMURL);
            _driver.Manage().Window.Maximize();
            IWebElement usernameElement =
            _driver.FindElement(By.Id("txtUserame"));
            usernameElement.SendKeys("TrainingUser2");
            IWebElement PasswordElement =
            _driver.FindElement(By.Id("txtPassword"));
            PasswordElement.SendKeys("Password1234567");
            IWebElement SignInElement = _driver.FindElement(By.Id("btnSignIn"));
            SignInElement.Click();
        }


        
        [When(@"I enter and save a batch message with trigger NOW to all players")]
        public void WhenIEnterAndSaveABatchMessageWithTriggerNOWToAllPlayers()
        {

            IWebElement configurationBtn = _driver.FindElement(By.Id("configurationBtn"));
            configurationBtn.Click();
            IWebElement batchMessage = _driver.FindElement(By.Id("batchMessage"));
            batchMessage.Click();
            IWebElement BtnConfiguration = _driver.FindElement(By.Id("MainContent_btnAddNewMessage"));
            BtnConfiguration.Click();

            //WebDriverWait wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(10));
            //wait.Until(driver => driver.FindElement(By.Id("MainContent_btnAddNewMessage")));
            IWebElement BtnAdd = _driver.FindElement(By.Id("MainContent_btnAddNewMessage"));
            BtnAdd.Click();

            IWebElement AllPropertiesCheckBox = _driver.FindElement(By.XPath("//label[contains(text(),'All Properties')]"));
            AllPropertiesCheckBox.Click();
            
            IWebElement MessageContentTab = _driver.FindElement(By.XPath("//a[contains(text(),'Message Content')]"));
            MessageContentTab.Click();

            IWebElement MessageSender = _driver.FindElement(By.Id("MainContent_txtMessageForm"));
            MessageSender.SendKeys("From Automated Test");

            IWebElement MessageBody = _driver.FindElement(By.Id("MainContent_txtMessageBody"));
            MessageBody.SendKeys("This is a sample message body sent to all players");

            IWebElement TriggerType = _driver.FindElement(By.Id("MainContent_ddlTriggerType"));
            SelectElement DropDown = new SelectElement(TriggerType);

            DropDown.SelectByText("Now");


            IWebElement Save = _driver.FindElement(By.Id("MainContent_lnkbtnSave"));
            Save.Click();
  

            IWebElement btnConfirmSave = _driver.FindElement(By.Id("MainContent_btnConfirmedSave"));
            //btnConfirmSave.Click();

        }
        
        [Then(@"My message should be saved to the DB")]
        public void ThenMyMessageShouldBeSavedToTheDB()
        {
            //TO DO add code to check the DB
            Assert.AreEqual("Popeyes", "KFC");
        }
            


       
    }
}
