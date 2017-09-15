using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace FinnSeleniumTest.PageObjects
{
    public class KategoriResultatPage : PageObjectBase
    {
        public KategoriResultatPage(IWebDriver driver) : base(driver)
        {
        }

        public override string StartUrl => $"https://www.finn.no/globalsearchlander.html?{Tilstand}";
        public string Tilstand { get; set; }

        public List<string> HentKategorier()
        {
            var kategoriElementer = HentKategoriElementer;

            return kategoriElementer.Select(x => x.Text).ToList();
        }

        public void VelgMotorsykkelKategori()
        {
            var kategoriElementer = HentKategoriElementer;
            kategoriElementer.First(x => x.Text.Contains("MC")).Click();
        }

        private ReadOnlyCollection<IWebElement> HentKategoriElementer => Driver.FindElements(By.ClassName("dropdown-link"));
    }
}
