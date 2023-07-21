Feature: DuckDuckGoSearch
		Search for Playwright on DuckDuckGo and go to the Playwright website from the search results

@smoke
Scenario: Scenario for Playwright on DuckDuckGo
	Given the user is on the DuckDuckGo homepage
	When the user searches for 'Playwright'
	Then the search results show 'Playwright' as the first results with link 'https://playwright.dev/'
