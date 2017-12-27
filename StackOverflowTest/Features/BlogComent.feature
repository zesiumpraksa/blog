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
	| 6F7206A6-7AB2-449E-87EA-2BA7B1F3A1CA    | 	
	Then Proba is Blog Details page
	When Proba insert new Blog Comment
	| Commentar               | BlogId                           |
	| Ovo je test komentart | 6F7206A6-7AB2-449E-87EA-2BA7B1F3A1CA |
	Then User is on Blog Index page 
	