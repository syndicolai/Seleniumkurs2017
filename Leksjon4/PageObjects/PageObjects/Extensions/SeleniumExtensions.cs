using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PageObjects.Extensions
{
    public static class SeleniumExtensions
    {
        public static IWebElement FinnElement(this IWebDriver driver, By by)
        {
            var elements = driver.FindElements(by);
            if (elements != null && elements.Any())
            {
                return elements.First();
            }

            throw new System.Exception($"Fant ikke element {by}");
        }

        public static List<IWebElement> FinnElementer(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).ToList();
        }


        public static List<IWebElement> FinnElementer(this IWebElement element, By by)
        {
            return element.FindElements(by).ToList();
        }

        public static void GåTilUrl(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void SkrivTekstIElement(this IWebElement element, string tekst)
        {
            element.SendKeys(tekst);
        }

        public static void Klikk(this IWebElement element)
        {
            element.Click();
        }

        public static void SettVentetid(this IWebDriver driver, TimeSpan ventetid)
        {
            driver.Manage().Timeouts().ImplicitWait = ventetid;
        }
    }
}
