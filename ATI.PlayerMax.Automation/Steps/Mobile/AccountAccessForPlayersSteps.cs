using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation
{
    [Binding]
    public class AccountAccessForPlayersSteps
    {

        private IWebDriver driver;



        public AccountAccessForPlayersSteps(IWebDriver driver)
        {
            this.driver = driver;
        }
        


        [Given]
        public void GivenIHaveALaunchedPlayerMaxMobileApp()
        {
            Thread.Sleep(5000);
        }
        
        [Given]
        public void GivenIHaveLaunchedPlayerMaxMobileApp()
        {
            Thread.Sleep(5000);
        }
        
        [When]
        public void WhenINavigateToPlayerMaxSignInPageAndEnterMyValidCredentialsAndPressLogin()
        {
            driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-property-list/ion-content/div/div/div[2]/div[1]/ion-list/ion-item/ion-grid/ion-row[1]")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-tabs/ion-app/ion-tabs/div/ion-router-outlet/ati-home2/ion-content/div/div[2]/span[2]/ion-button")).Click();
            driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[1]")).SendKeys("ali");
            driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[2]")).SendKeys("Password1!");
            driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/div/ion-button")).Click();
            Thread.Sleep(5000); //wait while we log in
        }
        
        [When]
        public void WhenINavigateToPlayerMaxSignInPageAndClickOnForgotLinkLocatedInThePasswordField()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void WhenIEnterMyEmailPlayerIDLastNameAndDOBAndPressSend()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void WhenOnTheNextPageIEnterTheCodeFromEmailNewPasswordAndNewPasswordConfirmationAndPressSubmit()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void ThenIShouldBeLoggedInAndBeAbleToAccessTheMessagesTab()
        {
            driver.FindElement(By.CssSelector("#tab-button-messages > svg")).Click();
        }
        
        [Then]
        public void ThenAToastThatSays_P0_ShouldBeDisplayedAndIShouldBeLoggedInAutomatically(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
