Feature: BlogComent
	This feature will test insert new blog comment functionality

@mytag
Scenario:  Successful create new blog comment
	Given Client is on Index page
	When Client enter UserName and Password in form and press Login	
	| Username | Password |
	| Proba     | Proba1   |	
	Then  Proba is on his Dashboard
	When Proba click on Blogs
	Then Proba is on Blog page
	When Proba clicks on blog details
	| BlogId								   |
	| 477C17A6-071D-4387-A191-46005304D35E     | 	
	Then Proba is Blog Details page
	When Proba insert new Blog Comment
	| Commentar               | BlogId                           |
	| Ovo je test komentart | 477C17A6-071D-4387-A191-46005304D35E |
	Then User is on Blog Index page 
	