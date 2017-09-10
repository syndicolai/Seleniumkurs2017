using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Text.RegularExpressions;

namespace Leksjon1
{
    [TestFixture]
    public class FinnSeleniumTest
    {
        IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
        }

        [Test]
        public void HelloFinn()
        {
            _webDriver.Navigate().GoToUrl("https://www.finn.no/");
            Assert.AreEqual("FINN.no – Mulighetenes marked", _webDriver.Title);
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            var søketerm = "BMW K1200";
            _webDriver.Navigate().GoToUrl("http://www.finn.no");
            var søkefelt = _webDriver.FindElement(By.Id("search"));
            søkefelt.SendKeys(søketerm);

            var søkeknapp = _webDriver.FindElements(By.ClassName("primary")).First();
            søkeknapp.Click();

            Assert.That(_webDriver.Url.Contains("BMW+K1200"), $"Forventet at url skulle inneholde søketerm 'BMW'");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_ViserMcSøkeresultatKategori()
        {
            _webDriver.Navigate().GoToUrl("http://www.finn.no");
            var søkefelt = _webDriver.FindElement(By.Id("search"));
            søkefelt.SendKeys("BMW K1200");

            var søkeknapp = _webDriver.FindElements(By.ClassName("primary")).First();

            søkeknapp.Click();

            var søkeresultatKategorier = _webDriver.FindElements(By.ClassName("dropdown-link"));

            Assert.That(søkeresultatKategorier.Any(kategori => kategori.Text.Contains("MC")));
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_VisResultater()
        {
            var søketerm = "BMW K1200";
            _webDriver.Navigate().GoToUrl($"https://www.finn.no/globalsearchlander.html?searchKeys=&q={søketerm}");
            var søkeresultatKategorier = _webDriver.FindElements(By.ClassName("dropdown-link"));
            søkeresultatKategorier.First(x => x.Text.Contains("MC")).Click();

            var resultContainer = _webDriver.FindElement(By.Id("page-results"));
            var results = resultContainer.FindElements(By.ClassName("result-item"));

            Assert.That(results.Any(), "Forventet å finne søkeresultater.");
        }

        [Test]
        public void Finn_Resultatliste_SorterPåPris_LaverstFørst()
        {
            _webDriver.Navigate().GoToUrl("https://www.finn.no/mc/all/search.html?q=bmw%20k1200");
            var sorteringselement = _webDriver.FindElement(By.Id("sort"));
            sorteringselement.SendKeys("Pris lav-høy");
            sorteringselement.SendKeys(Keys.Enter);

            // Hent ut resultat-lister, deretter finn elementer som inneholder pris, formater tekststrengen og konverter til
            // heltall, sjekk at prisen i den første raden er mindre enn den andre (billigst først).
            var prisWrappers = _webDriver.FindElements(By.ClassName("result-item"));
            Assert.IsNotNull(prisWrappers?.FirstOrDefault());
            var førstePrisElement = prisWrappers[0].FindElements(By.ClassName("inlineblockify"));
            Assert.IsNotNull(prisWrappers[1]);
            var andrePriselement = prisWrappers[1].FindElements(By.ClassName("inlineblockify"));
            var førstePris = Regex.Replace(førstePrisElement.Last().Text, @"[^\d]", string.Empty);
            var andrePris = Regex.Replace(andrePriselement.Last().Text, @"[^\d]", string.Empty);
            Assert.LessOrEqual(int.Parse(førstePris), int.Parse(andrePris));
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
        }
    }
}
