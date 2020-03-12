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
            
        }
        
        [When]
        public void When_I_navigate_to_PlayerMax_sign_in_page_and_enter_my_valid_credentials_and_press_login()
        {
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-page-home-default/ion-content/ion-content/div/div/ion-button")).Click();
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-property-list/ion-content/div/div/div[2]/div[1]/ion-list/ion-item/ion-grid/ion-row[1]")).Click();
            //Login
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-tabs/ion-app/ion-tabs/div/ion-router-outlet/ati-home2/ion-content/div/div[2]/span[2]/ion-button")).Click();
            System.Threading.Thread.Sleep(5000);
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[1]")).SendKeys("ali");
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/ion-list/input[2]")).SendKeys("Password1!!");
            _driver.FindElement(By.XPath("/html/body/app-root/ion-app/ion-router-outlet/app-login/ion-content/div/div/form/div/ion-button")).Click();
            Thread.Sleep(5000); //wait while we log in

        }
        
        [When]
        public void When_I_navigate_to_PlayerMax_sign_up_page_and_validate_my_player_club_number_last_name_and_DOB()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_enter_valid_email_account_and_a_password_the_meets_the_policy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_navigate_to_PlayerMax_sign_in_page_and_click_on_Forgot_link_located_in_the_username_field()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_enter_my_email_player_ID_Last_name_and_DOB_and_press_retreive()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_navigate_to_PlayerMax_sign_in_page_and_click_on_Forgot_link_located_in_the_password_field()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_I_enter_my_email_player_ID_Last_name_and_DOB_and_press_Send()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When]
        public void When_on_the_next_page_I_enter_the_code_from_email_new_password_and_new_password_confirmation_and_press_submit()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_I_should_be_logged_in_and_be_able_to_access_the_messages_tab()
        {
            _driver.FindElement(By.CssSelector("#tab-button-messages > svg")).Click();
        }
        
        [Then]
        public void Then_I_should_receive_and_email_with_a_verifiction_link()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_I_can_login_with_my_new_credentials_after_veryfing_my_email()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_I_should_see_my_username_on_the_screen()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then]
        public void Then_A_toast_that_says_P0_should_be_displayed_and_I_should_be_logged_in_automatically(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
