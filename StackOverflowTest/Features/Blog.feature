Feature: Blog
	This feature will test insert new blog functionality

//mozda bi trbalo koristiti background

@mytag
Scenario: Successful create new blog
	
	Given User is on Index page
	When User enter UserName and Password in form and press Login	
	| Username | Password |
	| Test     | Test1   |	
	Then User is on his Dashboard	
	When User Test click Create new blog
	Then User is on CreateNewBlogPage
	When Test insert values for new blogs and press Create
	| Titile     | Content      |
	| Test title | Test content |
	Then User is on Blog Index page 
	#povrsno je uradjen zadnji korak