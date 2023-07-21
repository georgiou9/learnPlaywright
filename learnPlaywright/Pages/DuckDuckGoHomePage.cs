using Microsoft.Playwright;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnPlaywright.Pages
{
    public class DuckDuckGoHomePage
    {
        /*
         * this class will follow a pattern with the page object models splitting them into 3 parts
         * Constructors | Selectors | Actions and assertions
         */

        #region Constructors
        private readonly IPage _page;

        public DuckDuckGoHomePage(Hooks.Hooks hooks) 
        {
            _page = hooks.Page;
        }
        #endregion

        #region Selectors
        // all page objects (DOM elements) are declared here
        // they'll be used in actions and assertions
        private ILocator SearchInput => _page.Locator("input[id='searchbox_input']");
        private ILocator SearchButton => _page.Locator("button[type='submit']");
        #endregion

        #region Actions and Assertions
        public async Task AssertPageContent()
        {
            //Assert that the correct URL has been reached
            _page.Url.Should().Contain("https://duckduckgo.com/");

            //Assert that the search input is visible
            var searchInputVisibility = await SearchInput.IsVisibleAsync();
            searchInputVisibility.Should().BeTrue();

            //Assert that the search button is visible
            var searchBtnVisibility = await SearchButton.IsVisibleAsync();
            searchBtnVisibility.Should().BeTrue();
        }

        public async Task SearchAndEnter (string searchTerm)
        {
            //Type the search term into the serach input
            await SearchInput.TypeAsync(searchTerm);

            //Assert that the search input has the text entered
            var searchInputInnerText = await SearchInput.InputValueAsync(); 
            searchInputInnerText.Should().Be(searchTerm);

            //Click the search button to submit the search
            await SearchButton.ClickAsync();
        }

        #endregion

    }
}
