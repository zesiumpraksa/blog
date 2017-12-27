Feature: Registration
	This feature will test Registration functionality

@mytag
Scenario: Succesful registration
	Given Client is on Index page
	Then Client go on Register page
	When Client enter valid values and press Create
	| Email     | UserName | FirstName | LastName | Password | RepeatPassword |
	| Toni111@gmail | Toni111     | Toni111      | Toni111     | Toni111    | Toni111          |
	Then Client is on Index page
	


Scenario: Unsuccesful registration with bad Password input
	Given Client is on Index page
	Then Client go on Register page
	When Client enter invalid values and press Create
	| Email     | UserName | FirstName | LastName | Password | RepeatPassword |
	| testMail | test     | test      | test   | test    | test          |
	Then Client get Password warning message


Scenario: Unsuccesful registration with bad Email input
	Given Client is on Index page
	Then Client go on Register page
	When Client enter invalid values and press Create
	| Email | UserName | FirstName | LastName | Password | RepeatPassword |
	|   testMail    |     test4     |  test4         |    test4      |   Test4       |     Test4      |  
	Then Client get another  warning message


Scenario: Unsuccesful registration without inputs
	Given Client is on Index page
	Then Client go on Register page
	When Client enter invalid values and press Create
	| Email | UserName | FirstName | LastName | Password | RepeatPassword |
	|                |          |           |          |          |                |	
	Then Client get warning message for required fields




#Scenario Outline: Unsuccesful registration
#	Given Client is on Register page
#	When Clent enter invalid values<Email>,<UserName>,<FirstName>,<LastName>,<Password>,<RepeatPassword> and press Create
#	Then Client get warning message
#
#	Examples: 
#	| Email          | UserName | FirstName | LastName | Password | RepeatPassword |
#	| testMail       | test     | test      | test     | test     | test           |
#	|                |          |           |          |          |                |	
#	| test@gmail.com | Test     | Test      | Test     | Test     | Test           |
#	