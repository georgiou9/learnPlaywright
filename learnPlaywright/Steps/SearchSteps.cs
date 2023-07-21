using learnPlaywright.Pages;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace learnPlaywright.Steps
{
    [Binding]
    public class SearchSteps
    {
        private readonly IPage _page;
        private readonly DuckDuckGoHomePage _duckDuckGoHomePage;

        public SearchSteps(Hooks.Hooks hooks, DuckDuckGoHomePage duckDuckGoHomePage)
        {
            _page = hooks.Page;
            _duckDuckGoHomePage = duckDuckGoHomePage;
        }

        [Given(@"the user is on the DuckDuckGo homepage")]
        public async Task GivenTheUserIsOnTheDuckDuckGoHomepage()
        {
            await _page.GotoAsync("https://duckduckgo.com/");
            await _duckDuckGoHomePage.AssertPageContent();
        }

        [When(@"the user searches for '([^']*)'")]
        public async Task WhenTheUserSearchesFor(string searchTerm)
        {
            await _duckDuckGoHomePage.SearchAndEnter(searchTerm);
        }

    }
}
