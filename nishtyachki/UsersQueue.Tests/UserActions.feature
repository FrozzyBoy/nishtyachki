@queue
Feature: UserActions
	In order to avoid silly mistakes
	As an application user
	I want to use queue

Scenario: Stay in queue
	Given I have run queue D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe
	And I have been connect to server
	When I press InQueue in queue
	Then the result should InQueue

Scenario: Start use nihtiak
	Given I have run queue D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe
	And I have been connect to server
	When I press InQueue in queue
	And I press to use nishtiak
	Then I must be notifyid to use it