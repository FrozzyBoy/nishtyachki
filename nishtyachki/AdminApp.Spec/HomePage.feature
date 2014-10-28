@web
Feature: HomePage
	In order to see quee state
	As a authentificated user
	I want to be told what happens with queue

Scenario: Add nishtiak
	Given I have run queue D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe
	And I have been authenticated true	
	When I press add true nishtiak
	Then the result should be appeare true new nihtiak

Scenario: Delete nishtiak
	Given I have run queue D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe
	And I have been authenticated true	
	When I press add false nishtiak
	Then the result should be appeare false new nihtiak