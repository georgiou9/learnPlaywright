using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace learnPlaywright;

[TestFixture]
public class Tests
{

    [SetUp]
    public async Task Setup()
    {

    }

    [Test]
    public async Task Test1()
    {
        //Playwright
        using var playwrigth = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwrigth.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");

        var lnkLogin = page.Locator("text=Login");
        await lnkLogin.ClickAsync();
        await page.FillAsync("#UserName", "admin");
        await page.FillAsync("#Password", "password");

        var btnLogin = page.Locator("input", new PageLocatorOptions { HasTextString = "Log in" });
        await btnLogin.ClickAsync();
        var isPresent = await page.Locator("text='Employee Details'").IsVisibleAsync();

        Assert.IsTrue(isPresent, "Could not locate the Emplyee Details element");
    }
}