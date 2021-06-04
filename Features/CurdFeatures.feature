Feature: CurdFeatures
	This files will contains features related to the curd operations.
	Creating , Editing , Deleting , Getting Info



@Add_employee
Scenario:  Post employee (Adding a employee)
	Given I set POST employee Details api endpoint
    When Set request Body giving information of employee
	| Name                                  |
	| Khush						|    
	And Send a POST HTTP request 
	Then I receive valid HTTP response code 201
    


@Edit_Employee
Scenario: Edit employee (Changing the details)
	Given I set Patch employee Details api endpoint
	| Id                                   | Name			   |
	| 7b22e983-195c-4b41-a263-0dfa256c301a |   Akush Kotnala   |
	And Set request Body of Patch request
	And Send a Patch HTTP request 
	Then I receive valid HTTP Response code 200


@delete_Employee
Scenario: Delete employee(delete the details of the employee)
	Given I set Delete employee Details api endpoint
	| id                                   |
	| bd8d2bbe-4bd4-4ec1-8f8c-fb61f48a83af | 
	When I send Delete HTTP request
	Then I receive valid Http response Code of 200 
	



@Getting_EmployeeInfo
Scenario: Getting the details of the Employee with given id.
	Given I set GET Employee api endpoint with id
	When id is given 
	| id                                   |
	| eb4910f2-ad3f-4dca-bcc9-2493c191db68 |      
	And send Get Http request
	Then we receive valid Http response code 200
	