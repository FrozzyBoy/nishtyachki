@clientApp
Feature: UserApp
	In order to avoid silly mistakes
	As a user
	I want to manage my state in queue

Scenario: use nishtiak and stop use with client
	Given I have started D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe and D:/Projects/nishtyachki/nishtyachki/MvvmUserApp/bin/Debug/MvvmUserApp.exe
	When I press stay in queue
	And I press use true nishtiak
	And I press stop use nishtiak
	Then I must see start page

Scenario: Dismiss offer to use nishtiak with client
	Given I have started D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe and D:/Projects/nishtyachki/nishtyachki/MvvmUserApp/bin/Debug/MvvmUserApp.exe
	When I press stay in queue
	And I press use false nishtiak
	Then I must see start page

	