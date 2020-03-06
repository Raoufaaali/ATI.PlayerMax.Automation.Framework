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

           // _driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(5000);

            IWebElement usernameElement =
            _driver.FindElement(By.Id("txtUserame"));
            usernameElement.SendKeys("TrainingUser2");

            IWebElement PasswordElement =
            _driver.FindElement(By.Id("txtPassword"));
            PasswordElement.SendKeys("Password1234567");
            System.Threading.Thread.Sleep(2000);
        }
        
        [When(@"I press Sign In")]
        public void WhenIPressSignIn()
        {

            IWebElement SignInElement = _driver.FindElement(By.Id("btnSignIn"));
            SignInElement.Click();
            System.Threading.Thread.Sleep(1000);
        }
        
        [Then(@"I should be logged in to MAM")]
        public void ThenIShouldBeLoggedInToMAM()
        {
            String URL = _driver.Url;
            Assert.AreEqual(URL, "https://playermax-sit.aristocrat.systems/Admin/Viewer.aspx");
        }

    }
}
