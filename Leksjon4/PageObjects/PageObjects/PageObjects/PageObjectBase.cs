using OpenQA.Selenium;

namespace FluentOgByggere.PageObjects
{
    public class PageObjectBase
    {
        protected IWebDriver Driver;
        public PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigerTil(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}
