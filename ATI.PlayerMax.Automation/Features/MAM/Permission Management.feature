Feature: Permission Management
	In order to control access to PlayerMax Administrator (AKA MAM)
	As a PlayerMax administrator
	I want to be sure that only authorized users can login to MAM and that users are restricted what they can view, edit or delete bases on thier defined role

@mam @smoke
Scenario: Login to MAM as an Administrator
	Given I have navigated to the appropriate MAM URL and I have admin credentials
	And I have entered my username and password
	When I press Sign In
	Then I should be logged in to MAM


	Scenario: MAM Attempt To Login with Invalid Credentials
	Given I have navigated to the appropriate MAM URL and I have admin credentials
	And I have entered wrong username and password
	When I press Sign In
	Then I should not be logged into MAM
	And I should receive an error message saying "You don't have privileges to login"


	@mam @smoke @ignore
Scenario: MAM Read-Only Access
	Given I have navigated to the appropriate MAM URL
	And I have used my read-only username and password to login 
	When I try to edit or add content
	Then I shouldn not see the save button

	@ignore
Scenario: MAM Limited Access 
	Given I have navigated to the appropriate MAM URL
	And My user role is not allowed to view permission management screen 
	And I have entered my username and password 
	When I navigate to Configuration 
	Then I shouldn not see Permission Management 

