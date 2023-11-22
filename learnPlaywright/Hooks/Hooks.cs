using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace learnPlaywright.Hooks
{
    [Binding]
    public class Hooks
    {
        // this property will be called in the tests
        // this property will give us more control over the browser context
        public IPage Page { get; private set; } = null;
        public IBrowserContext Context1 { get; private set; }

        // will be excecuting these steps before each schenario
        [BeforeScenario]
        public async Task RegisterSingleInstancePractitioner()
        {
            // initialise Playwrigth
            var playwright = await Playwright.CreateAsync();

            // initialise Browser
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Timeout = 3_000
            });

            // setup browser context
            Context1 = await browser.NewContextAsync();

            // initialise a page on the browser context
            Page = await Context1.NewPageAsync();
        }

        [AfterScenario]
        public async Task TearDownBrowserAsync()
        {
            await Context1.CloseAsync();
            await Page.CloseAsync();
        }
    }
}
