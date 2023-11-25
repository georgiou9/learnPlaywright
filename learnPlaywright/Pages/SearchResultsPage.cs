using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace learnPlaywright.Pages
{
    public class SearchResultsPage
    {
        #region Constructors
        private readonly IPage _page;

        public SearchResultsPage(Hooks.Hooks hooks)
        {
            _page = hooks.Page;
        }

        #endregion

        #region Locators
        // this is being set in the action/assertions below
        private int _resultIndex;

        private ILocator SearchInput => _page.Locator("input[id='search_form_input']");
        private ILocator SearchResults => _page.Locator("section[data-testid='mainline']");

        //Notice how the selector below user the 'SearchResults' locator instead of the IPage to locate the element
        //The 'nth' locator is used to select an element at a specific index when there are multiple elements found
        private ILocator ResultArticle => SearchResults.Locator("article").Nth(_resultIndex);

        //We're using the single search result that we've located as 'ResultArticle' to locate the next 2 selectors
        private ILocator ResultHeading => ResultArticle.Locator("h2");
        private ILocator ResutlLink => ResultArticle.Locator("a[data-testid='result-title-a']");
        #endregion

        #region Actions and Assertions
        //Assert the page url 
        public async Task AssertPageContent(string searchTerm)
        {
            //Assert the page url
            await _page.WaitForURLAsync(new Regex("https://duckduckgo.com/\\?va=d&"));

            //Assert the search input has the search term
            var searchInputInnerText = await SearchInput.InputValueAsync();
            searchInputInnerText.Should().Be(searchTerm);
        }

        public async Task AssertSearchResultAtIndex(string searchTerm, int resultIndex, string expectedResultLink) 
        {
            _resultIndex = resultIndex;
            //Assert the first result text
            var firstResultInnerText = await ResultHeading.InnerTextAsync();
            firstResultInnerText.Should().Contain(searchTerm);

            //Assert the first result link
            var firstResultLink = await ResutlLink.GetAttributeAsync("href");
            firstResultLink.Should().Be(expectedResultLink);

        }
        #endregion
    }
}
