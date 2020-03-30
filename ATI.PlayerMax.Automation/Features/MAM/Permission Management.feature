Feature: Permission Management
	In order to control access to PlayerMax Administrator (AKA MAM)
	As a PlayerMax administrator
	I want to be sure that only authorized users can login to MAM and that users are restricted what they can view, edit or delete bases on thier defined role

@tc:403742
@mam @smoke
Scenario: Login to MAM as an Administrator
	Given I have navigated to the appropriate MAM URL and I have admin credentials
	And I have entered my username and password
	When I press Sign In
	Then I should be logged in to MAM


@tc:403743
	Scenario: MAM Attempt To Login with Invalid Credentials
	Given I have navigated to the appropriate MAM URL and I have admin credentials
	And I have entered wrong username and password
	When I press Sign In
	Then I should not be logged into MAM
	And I should receive an error message saying "You don't have privileges to login"


@tc:403744
	@mam @smoke @ignore
Scenario: MAM Read-Only Access   
	Given I have navigated to the appropriate MAM URL
	And I have used my read-only username and password to login 
	When I try to edit or add content
	Then I shouldn not see the save button



@tc:403755
	@ignore
Scenario: Scenarios TFS Sync
	Given I added a new scenario
	And I am using PlayerMax automation framework 
	When I queue a new build
	Then the scenario should be added to TFS as a test case 


@ignore
Scenario Outline: MAM Limited Access 
Given I have navigated to the appropriate MAM URL
And I have entered my username as <Username> and my password as <Password>
When I navigate to page <Page> 
Then I shouldn <Result>  

Examples:
| Username      | Password  | Page                  | Result                            |
| admin         | Password1 | Permission Management | Can view, edit and delete         |
| marketinguser | Password2 | Permission Management | cant view, cant edit, cant delete |
| IT_only       | Password3 | Dining                | Can view, cant edit, cant delete  |	



