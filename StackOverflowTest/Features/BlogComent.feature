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
	| ABF501EA-8A6C-4A69-A5A6-0E235E0CECFC     | 	
	Then Proba is Blog Details page
	When Proba insert new Blog Comment
	| Commentar               | BlogId                           |
	| Ovo je test komentart | ABF501EA-8A6C-4A69-A5A6-0E235E0CECFC |
	Then User is on Blog Index page 
	