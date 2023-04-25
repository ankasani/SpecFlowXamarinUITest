Feature: Launch App
	Simple Launch App

Background:
	Given Click On Ok Button 'First'

@mytag
Scenario: Launch App
	And  Again Click On Ok Button 'Second'
	When Two Click Are Done
	Then Page Need To Load with Video