Feature: Login
	This feature will test a LogIn and LogOut functionality

@mytag
Scenario: Successful Login with valid credintials
	Given User is on Index page
	When User enter UserName and Password in form and press Login	
	| Username | Password |
	| Test     | Test1   |	
	Then User is on his Dashboard

Scenario: Login with invalid credintials
	Given User is on Index page
	When User enter UserName and Password in form and press Login	
	| Username | Password |
	| Test     | Test   |
	Then User is on Index page with first warning information	
		

Scenario: Login without any credintials
	Given User is on Index page
	When User enter UserName and Password in form and press Login	
	| Username | Password |
	|		   |          |
	Then User is on Index page with second warning information	
		

Scenario: Successfull Logout
	Given User is on Index page
	When User click on Logout
	Then User is on on Index page and have message to log in