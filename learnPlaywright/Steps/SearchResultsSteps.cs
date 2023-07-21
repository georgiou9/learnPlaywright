using learnPlaywright.Hooks;
using learnPlaywright.Pages;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace learnPlaywright.Steps
{
    [Binding]
    public class SearchResultsSteps
    {
        private readonly SearchResultsPage _searchResults;

        public SearchResultsSteps(SearchResultsPage searchResults)
        {
            _searchResults = searchResults;
        }

        [Then(@"the search results show '([^']*)' as the first results with link '([^']*)'")]
        public async Task ThenTheSearchResultsShowAsTheFirstResultsWithLink(string expectedResult, string expectedLink)
        {
            //Assert the page content
            await _searchResults.AssertPageContent(expectedResult);

            //Assert the first search result (hence the index of 0)
            await _searchResults.AssertSearchResultAtIndex(expectedResult, 0, expectedLink);
        }
    }
}
