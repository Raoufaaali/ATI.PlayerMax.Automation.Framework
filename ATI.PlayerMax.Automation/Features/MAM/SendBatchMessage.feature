Feature: SendBatchMessage
	In order to communicate with PlayerMax patrons
	As a PlayerMax administrator
	I want to be able to send in-app messages (AKA Batch Messages)
	
	@smoke @mam
	Scenario: Send Batch Message Now to All Players
	Given I have logged into PlayerMax Administrator website and navigated to Configuration > Batch  Messages
	When I enter and save a batch message with trigger NOW to all players
	Then My message should be saved to the DB