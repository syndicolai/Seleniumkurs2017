using OpenQA.Selenium;
using PageObjects.Konstanter;
using PageObjects.Extensions;
using System;

namespace FluentOgByggere.PageObjects
{
    public class StartsidePage : PageObjectBase
    {
        private const string StartsideUrl = "http://www.finn.no";

        public StartsidePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigerTil()
        {
            NavigerTil(StartsideUrl);
        }

        public void UtførSøk(string søkestring)
        {
            var søkefelt = Driver.FinnElement(By.Id(ElementKonstanter.SøkefeltId));
            søkefelt.SkrivTekstIElement(søkestring);
            Driver.FinnElement(By.ClassName(ElementKonstanter.SøkeknappKlasse)).Klikk();
        }

        public IWebElement HentSøkeelement()
        {
           return Driver.FinnElement(By.Id(ElementKonstanter.SøkefeltId));
        }
    }
}
