using FluentSeleniumOgStruktur.Base;
using FluentSeleniumOgStruktur.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace FluentSeleniumOgStruktur
{
    [TestFixture]
    public class FinnSeleniumTest : TestBase
    {
        [Test]
        public void HelloFinn()
        {
            _webDriver.GåTilUrl("https://www.finn.no/");
            Assert.AreEqual("FINN.no – Mulighetenes marked", _webDriver.Title);
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            _webDriver.GåTilUrl("http://www.finn.no");
            var søkefelt = _webDriver.FinnElement(By.Id("search"));
            søkefelt.SkrivTekstIElement("BMW");

            _webDriver.FinnElement(By.ClassName("primary")).Klikk();

            Assert.That(_webDriver.Url.Contains("BMW"));
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_ViserMcSøkeresultatKategori()
        {
            _webDriver.GåTilUrl("http://www.finn.no");
            var søkefelt = _webDriver.FinnElement(By.Id("search"));
            søkefelt.SkrivTekstIElement("BMW K1200");

           _webDriver.FinnElement(By.ClassName("primary")).Klikk();
            
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));

            Assert.That(søkeresultatKategorier.Any(kategori => kategori.Text.Contains("MC")), "Forventet å finne søkekategori 'MC'");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_VisResultater()
        {
            var søketerm = "BMW K1200";
            _webDriver.GåTilUrl($"https://www.finn.no/globalsearchlander.html?searchKeys=&q={søketerm}");
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));
            søkeresultatKategorier.First(x => x.Text.Contains("MC")).Click();

            var resultContainer = _webDriver.FinnElement(By.Id("page-results"));
            var results = resultContainer.FinnElementer(By.ClassName("result-item"));

            Assert.That(results.Any(), "Forventet å finne søkeresultater.");
        }
    }
}
