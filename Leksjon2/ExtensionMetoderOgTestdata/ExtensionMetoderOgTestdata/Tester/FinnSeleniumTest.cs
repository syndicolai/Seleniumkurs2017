using FluentSeleniumOgStruktur.Extensions;
using FluentSeleniumOgStruktur.Testdata;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FluentSeleniumOgStruktur.Tester
{
    [TestFixture]
    public class FinnSeleniumTest
    {
        [Test]
        public void Finn_Startside_SøkefeltEksisterer()
        {
            _webDriver.Navigate().GoToUrl("https://www.finn.no/");
            var resultat = _webDriver.FinnElement(By.Id("search"));

            Assert.IsNotNull(resultat);
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            _webDriver.GåTilUrl("http://www.finn.no");
            var søkefelt = _webDriver.FinnElement(By.Id("search"));
            var motorsykkel = MotorsykkelData.Get();
            søkefelt.SkrivTekstIElement(motorsykkel);

            _webDriver.FinnElement(By.ClassName("primary")).Klikk();

            Assert.That(_webDriver.Url.Contains(motorsykkel));
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_ViserMcSøkeresultatKategori()
        {
            _webDriver.GåTilUrl("http://www.finn.no");
            var søkefelt = _webDriver.FinnElement(By.Id("search"));
            søkefelt.SkrivTekstIElement(MotorsykkelData.Get());

           _webDriver.FinnElement(By.ClassName("primary")).Klikk();
            
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));

            Assert.That(søkeresultatKategorier.Any(kategori => kategori.Text.Contains("MC")), "Forventet å finne søkekategori 'MC'");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_VisResultater()
        {
            var søketerm = MotorsykkelData.Get();
            _webDriver.GåTilUrl($"https://www.finn.no/globalsearchlander.html?searchKeys=&q={søketerm}");
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));
            søkeresultatKategorier.First(x => x.Text.Contains("MC")).Click();

            var resultContainer = _webDriver.FinnElement(By.Id("page-results"));
            var results = resultContainer.FinnElementer(By.ClassName("result-item"));

            Assert.That(results.Any(), "Forventet å finne søkeresultater.");
        }

        [Test]
        public void Finn_Resultatliste_SorterPåPris_LaverstFørst()
        {
            var søketerm = MotorsykkelData.Get();
            _webDriver.GåTilUrl($"https://www.finn.no/mc/all/search.html?q={søketerm}");
            var sorteringselement = _webDriver.FinnElement(By.Id("sort"));
            sorteringselement.SkrivTekstIElement("Pris lav-høy");
            sorteringselement.SkrivTekstIElement(Keys.Enter);
            
            var prisWrappers = _webDriver.FinnElementer(By.ClassName("result-item"));
            Assert.IsNotNull(prisWrappers?.FirstOrDefault());
            var førstePrisElement = prisWrappers[0].FinnElementer(By.ClassName("inlineblockify"));
            Assert.IsNotNull(prisWrappers[1]);

            var andrePriselement = prisWrappers[1].FinnElementer(By.ClassName("inlineblockify"));
            var førstePris = Regex.Replace(førstePrisElement.Last().Text, @"[^\d]", string.Empty);
            var andrePris = Regex.Replace(andrePriselement.Last().Text, @"[^\d]", string.Empty);

            Assert.LessOrEqual(int.Parse(førstePris), int.Parse(andrePris));
        }

        protected IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
        }
    }
}
