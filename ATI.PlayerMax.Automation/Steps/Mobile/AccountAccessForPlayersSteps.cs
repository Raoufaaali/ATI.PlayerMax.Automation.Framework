using OpenQA.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace ATI.PlayerMax.Automation.Steps.Mobile
{
    [Binding]
    public class AccountAccessForPlayersSteps
    {
        IWebDriver _driver;

        public AccountAccessForPlayersSteps(IWebDriver driver)
        {
            _driver = driver;            
        }

        
        [Given]
        public void Given_I_have_launched_PlayerMax_mobile_app()
        {
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-page-home-default/ion-content/ion-content/div/div/ion-button"));
        }
        
        [When]
        public void When_I_navigate_to_PlayerMax_sign_in_page_and_enter_my_valid_credentials_and_press_login()
        {          
            
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-page-home-default/ion-content/ion-content/div/div/ion-button")).Click();
            //_driver.FindElement(By.XPath("//div[contains(text(),'Suncoast')]")).Click();
            _driver.FindElement(By.XPath("//*[contains(text(), 'Charles')]")).Click();

            // _driver.FindElement(By.XPath("//div[. = 'TextToFind']")).Click();


            //_driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-property-list/ion-content/div/div/div[2]/div[1]/ion-list/ion-item/ion-grid/ion-row[1]")).Click();
            //Login
            _driver.FindElement(By.XPath("//*[contains(text(), 'SIGN IN')]")).Click();
            //_driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-tabs/ion-app/ion-tabs/div/ion-router-outlet/ati-home2/ion-content/div/div[2]/span[2]/ion-button")).Click();

            //_driver.FindElement(By.XPath("//*[contains(@class, 'ati-forgot-label') and text() = 'Forgot?']")).Click();
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[1]")).SendKeys("ali");
            
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[2]")).SendKeys("Password1!!");
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/div/ion-button")).Click();
            Thread.Sleep(5000); //wait while we log in

        }      

        [Then]
        public void Then_I_should_be_logged_in_and_be_able_to_access_the_messages_tab()
        {
            _driver.FindElement(By.CssSelector("#tab-button-messages > svg")).Click();
        }
        
    }
}
