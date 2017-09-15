using System.Linq;
using OpenQA.Selenium;

namespace FinnSeleniumTest.Utvidelser
{
    public static class WebDriverUtvidelser
    {
        public static IWebElement FinnUniktElement(this IWebDriver driver, By by)
        {
            var element = driver.FindElements(by);
            return element.Count != 1 ? null : element.First();
        }
    }
}
