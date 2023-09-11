using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace End2EndTesting
{
    public class CostCalculationPageTests : PageTest
    {       
        [TestCase(1, 0, 0, "6.4")]
        [TestCase(5, 1, 0, "15.5")]
        [TestCase(2, 2, 0, "3.0")]
        [Category("L4")]
        public async Task ConfirmCorrectCost_WhenEnteringNumberOfPeople(int duration, int cover, int period, string estimate)
        {   
            string uri = "http://localhost:5113/";

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEST_URI"))) {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                uri = Environment.GetEnvironmentVariable("TEST_URI");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }

#pragma warning disable CS8604 // Possible null reference argument.
            await Page.GotoAsync(uri);
#pragma warning restore CS8604 // Possible null reference argument.

            await Expect(Page).ToHaveTitleAsync(new Regex("Estimate - Web"));

            await Page.Locator("#num_people").FillAsync(duration.ToString());
            await Page.Locator("#num_cover").SelectOptionAsync(cover.ToString());
            await Page.Locator("#num_period").SelectOptionAsync(period.ToString());

            var getStarted = Page.GetByRole(AriaRole.Button, new() { Name = "Calculate" });

            await getStarted.ClickAsync();

            await Expect(Page.Locator("#dec_cost")).ToHaveTextAsync(new Regex($".*{estimate}"), new LocatorAssertionsToHaveTextOptions { Timeout=1000});
        }
    }
}