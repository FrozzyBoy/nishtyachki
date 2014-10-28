Feature: UserAppLogic
	In order to avoid silly mistakes
	As a user
	I want to be told where am i in queue

@watchViewModel
Scenario: Change callback class to enqueu and wath how change view model
	Given I have init callback
	When I say to callback that i stay in queue
	Then Buttons state must be changed to enqueu state

@watchViewModel
Scenario: Change callback class to offer use and wath how change view model
	Given I have init callback
	When I say to callback that i mist be offered to use nishtiak
	Then Buttons state must be changed to offer nishtiak state

@watchViewModel
Scenario: Change callback class to agree offer and wath how change view model
	Given I have init callback
	When I say to callback that i offered to use nistiak
	Then Buttons state must be changed using nishtiak