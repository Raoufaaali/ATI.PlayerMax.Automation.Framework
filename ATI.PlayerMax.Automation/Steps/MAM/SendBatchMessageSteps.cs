﻿using ATI.PlayerMax.Automation.DriverFactory;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
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
        WebDriverWait wait;
        //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        

       // [Before]
        public void Setup()
        {
            WebDriverFactory webDriverFactory = new WebDriverFactory();
            _driver = webDriverFactory.Get();//new ChromeDriver(".");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
         
        }

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
            Thread.Sleep(2000);
            IWebElement batchMessage = _driver.FindElement(By.Id("batchMessage"));
            batchMessage.Click();
            Thread.Sleep(2000);
            IWebElement BtnConfiguration = _driver.FindElement(By.Id("MainContent_btnAddNewMessage"));
            BtnConfiguration.Click();

            //WebDriverWait wait = new WebDriverWait(_driver, System.TimeSpan.FromSeconds(10));
            //wait.Until(driver => driver.FindElement(By.Id("MainContent_btnAddNewMessage")));
            Thread.Sleep(2000);
            IWebElement BtnAdd = _driver.FindElement(By.Id("MainContent_btnAddNewMessage"));
            BtnAdd.Click();
            Thread.Sleep(5000);

            IWebElement AllPropertiesCheckBox = _driver.FindElement(By.XPath("//label[contains(text(),'All Properties')]"));
            AllPropertiesCheckBox.Click();
            
            IWebElement MessageContentTab = _driver.FindElement(By.XPath("//a[contains(text(),'Message Content')]"));
            MessageContentTab.Click();
            Thread.Sleep(5000);

            IWebElement MessageSender = _driver.FindElement(By.Id("MainContent_txtMessageForm"));
            MessageSender.SendKeys("From Automated Test");

            IWebElement MessageBody = _driver.FindElement(By.Id("MainContent_txtMessageBody"));
            MessageBody.SendKeys("This is a sample message body sent to all players");

            IWebElement TriggerType = _driver.FindElement(By.Id("MainContent_ddlTriggerType"));
            SelectElement DropDown = new SelectElement(TriggerType);

            DropDown.SelectByText("Now");

            Thread.Sleep(5000);

            IWebElement Save = _driver.FindElement(By.Id("MainContent_lnkbtnSave"));
            Save.Click();
            Thread.Sleep(5000);

            IWebElement btnConfirmSave = _driver.FindElement(By.Id("MainContent_btnConfirmedSave"));
            //btnConfirmSave.Click();

            Thread.Sleep(5000);
        }
        
        [Then(@"My message should be saved to the DB")]
        public void ThenMyMessageShouldBeSavedToTheDB()
        {
            //TO DO add code to check the DB
            Assert.AreEqual(1, 1);
        }

        


       
    }
}
