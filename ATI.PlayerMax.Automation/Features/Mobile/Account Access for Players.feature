Feature: Account Access for Players
	As an existing casino patron, I want to be able to perform the following operations:
	Login to an existing account
	register a new account
	retreive username
	change password
	forgot password
	reset password after admin account reset 

@smoke @mobile @ignore
Scenario: Player Login to Existing Account
	Given I have a launched PlayerMax mobile app
	When I navigate to PlayerMax sign in page and enter my valid credentials and press login
	Then I should be logged in and be able to access the messages tab
	
@smoke @mobile @ignore
Scenario: Player Register a New Account

@smoke @mobile @ignore
Scenario: Player Retreive Username


@smoke @mobile @ignore
Scenario: Player Change Password


@smoke @mobile @ignore
Scenario: Player Forgot Password
	Given I have launched PlayerMax mobile app
	When I navigate to PlayerMax sign in page and click on Forgot? link located in the password field
	And I enter my email, player ID, Last name and DOB and press Send
	And on the next page I enter the code from email, new password and new password confirmation and press submit
	Then A toast that says "Password reset successfully" should be displayed and I should be logged in automatically

	
@smoke @mobile 
Scenario: Player Reset Password After Admin Account Reset  
