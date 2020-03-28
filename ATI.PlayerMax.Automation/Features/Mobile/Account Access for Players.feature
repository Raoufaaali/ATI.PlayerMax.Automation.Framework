Feature: Account Access for Players
	As an existing casino patron, I want to be able to perform the following operations:
	Login to an existing account
	register a new account
	retreive username
	change password
	forgot password
	reset password after admin account reset 

@tc:403747
@smoke @mobile 
Scenario: Player Login to Existing Account
	Given I have launched PlayerMax mobile app
	When I navigate to PlayerMax sign in page and enter my valid credentials and press login
	Then I should be logged in and be able to access the messages tab
	
@tc:403748
@smoke @mobile @ignore
Scenario: Player Register a New Account
	Given I have launched PlayerMax mobile app
	When I navigate to PlayerMax sign up page and validate my player club number, last name and DOB
	And I enter valid email account and a password the meets the policy
	Then I should receive and email with a verifiction link 
	And I can login with my new credentials after veryfing my email 

@tc:403749
@smoke @mobile @ignore
Scenario: Player Retreive Username
	Given I have launched PlayerMax mobile app
	When I navigate to PlayerMax sign in page and click on Forgot? link located in the username field
	And I enter my email, player ID, Last name and DOB and press retreive
	Then I should see my username on the screen

@tc:403750
@smoke @mobile @ignore
Scenario: Player Change Password

@tc:403751
@smoke @mobile @ignore
Scenario: Player Forgot Password
	Given I have launched PlayerMax mobile app
	When I navigate to PlayerMax sign in page and click on Forgot? link located in the password field
	And I enter my email, player ID, Last name and DOB and press Send
	And on the next page I enter the code from email, new password and new password confirmation and press submit
	Then A toast that says "Password reset successfully" should be displayed and I should be logged in automatically

	
@tc:403752
@smoke @mobile @ignore
Scenario: Player Reset Password After Admin Account Reset  
	Given Test Skip
	When Test Skip
	And Test Skip
