using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation.Steps.MAM
{
 
    //[Binding, Scope(Feature = "Permission Management")]
    [Binding]
    public class PermissionManagementSteps
    {
        private IWebDriver _driver;

        string PlayerMaxMAMURL = "https://playermax-sit.aristocrat.systems";
        

        public PermissionManagementSteps(IWebDriver driver)
        {
            _driver = driver;
        }


    [Given(@"I have navigated to the appropriate MAM URL and I have admin credentials")]
        public void GivenIHaveNavigatedToTheAppropriateMAMURLAndIHaveAdminCredentials()
        {
            _driver.Navigate().GoToUrl(PlayerMaxMAMURL);
        }
        
        [Given(@"I have entered my username and password")]
        public void GivenIHaveEnteredMyUsernameAndPassword()
        {
            IWebElement usernameElement =
            _driver.FindElement(By.Id("txtUserame"));
            usernameElement.SendKeys("TrainingUser2");

            IWebElement PasswordElement =
            _driver.FindElement(By.Id("txtPassword"));
            PasswordElement.SendKeys("Password1234567");
        }
        
        [When(@"I press Sign In")]
        public void WhenIPressSignIn()
        {
            IWebElement SignInElement = _driver.FindElement(By.Id("btnSignIn"));
            SignInElement.Click();            
        }
        
        [Then(@"I should be logged in to MAM")]
        public void ThenIShouldBeLoggedInToMAM()
        {
            String URL = _driver.Url;
            Assert.AreEqual(URL, "https://playermax-sit.aristocrat.systems/Admin/Viewer.aspx");
        }

        [Given]
        public void Given_I_have_entered_wrong_username_and_password()
        {
            IWebElement usernameElement =
           _driver.FindElement(By.Id("txtUserame"));
            usernameElement.SendKeys("WrongUsername");

            IWebElement PasswordElement =
            _driver.FindElement(By.Id("txtPassword"));
            PasswordElement.SendKeys("Wrong Password");
        }

        [Then]
        public void Then_I_should_not_be_logged_into_MAM()
        {
            String URL = _driver.Url;
            Assert.AreNotEqual(URL, "https://playermax-sit.aristocrat.systems/Admin/Viewer.aspx");
        }

        [Then]
        public void Then_I_should_receive_an_error_message_saying_P0(string p0)
        {
            var errorMessage = _driver.FindElement(By.XPath("//p[@id='alertMessage']"));
            Assert.IsTrue(_driver.PageSource.Contains("You don't have privileges to login"));
        }




    }
}
