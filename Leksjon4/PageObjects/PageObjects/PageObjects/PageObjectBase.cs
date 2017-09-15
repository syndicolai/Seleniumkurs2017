using OpenQA.Selenium;
using System;

namespace FluentOgByggere.PageObjects
{
    public abstract class PageObjectBase : IDisposable
    {
        protected IWebDriver Driver;
        public PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Dispose()
        {
            Driver?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void NavigerTil(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}
