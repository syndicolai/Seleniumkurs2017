using FinnSeleniumTest.Konstanter;
using FinnSeleniumTest.Testdata;
using FinnSeleniumTest.Utvidelser;
using OpenQA.Selenium;

namespace FinnSeleniumTest.PageObjects
{
    public class StartsidePage : PageObjectBase
    {
        public StartsidePage(IWebDriver driver) : base(driver)
        {

        }

        public override string StartUrl => "https://www.finn.no/";

        public void SøkerPåMotorsykkel()
        {
            var søkefelt = Driver.FinnUniktElement(Konstantliste.SøkefeltSelector);
            søkefelt.SendKeys(MotorsykkelGenerator.Get());
            søkefelt.SendKeys(Keys.Enter);
        }

        public bool SøkefeltEksisterer()
        {
            return Driver.FinnUniktElement(Konstantliste.SøkefeltSelector) != null;
        }
    }
}
