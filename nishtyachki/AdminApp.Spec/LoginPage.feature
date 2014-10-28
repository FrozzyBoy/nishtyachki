@web
Feature: LoginPage
	In order to provide security
	As a web app user
	I want to manage account

Scenario: Log In positive
	Given I have been authenticated false
	And I have entered Allonsi in field UserName
	And I have entered Fidelio in field Password
	When I press to log true
	Then log in result must be true

Scenario: Log Off
	Given I have been authenticated true
	When I press to log false
	Then log in result must be false

Scenario: Log In negative
	Given I have been authenticated false
	And I have entered Lolik Name in field UserName
	And I have entered Lolik Password in field Password
	When I press to log true
	Then log in result must be false