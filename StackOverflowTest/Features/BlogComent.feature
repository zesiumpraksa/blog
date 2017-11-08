Feature: BlogComent
	This feature will test insert new blog comment functionality

@mytag
Scenario:  Successful create new blog comment
	Given User is on Index page
	When User enter UserName and Password in form and press Login	
	| Username | Password |
	| Test     | Test1   |	
	Then User is on his Dashboard
	When Test go to Blog Index page
	Then Test clicks on blog details
	| BlogId								   |
	| CBC48C18-47A6-46D3-B8FA-01334B07C406     | 	
	When Test insert new Blog Comment
	| Commentar               | BlogId                           |
	| Ovo je test komentart | CBC48C18-47A6-46D3-B8FA-01334B07C406 |
	Then User is on Blog Index page 
	