using System;
using FluentOgByggere.Base;
using NUnit.Framework;
using FluentOgByggere.Extensions;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using FluentOgByggere.Konstanter;

namespace FluentOgByggere.Byggere
{
    public class FinnSøkBygger : FluentBase<FinnSøkBygger>
    {
        public void NavigererTilUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public void TittelenPåSidenVære(string forventetTittel)
        {
            Assert.AreEqual(forventetTittel, _webDriver.Title);
        }

        public void UtførerSøk(string søkestring)
        {
            var søkefelt = _webDriver.FinnElement(By.Id(ElementKonstanter.SøkefeltId));
            søkefelt.SkrivTekstIElement(søkestring);
            _webDriver.FinnElement(By.ClassName(ElementKonstanter.SøkeknappKlasse)).Klikk();
        }

        public void UrlInneholde(string forventetInnhold)
        {
            Assert.That(_webDriver.Url.Contains(forventetInnhold));
        }

        public void SøkeresultatKategorierInneholdeKategori(string forventetKategori)
        {
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));
            Assert.That(søkeresultatKategorier.Any(kategori => kategori.Text.Contains(forventetKategori)), $"Forventet å finne søkekategori '{forventetKategori}'");
        }

        public void VisesResultater()
        {
            var resultContainer = _webDriver.FinnElement(By.Id("page-results"));
            var results = resultContainer.FinnElementer(By.ClassName("result-item"));

            Assert.That(results.Any(), "Forventet å finne søkeresultater.");
        }

        public void VelgerKategori(string kategoritekst)
        {
            var søkeresultatKategorier = _webDriver.FinnElementer(By.ClassName("dropdown-link"));
            søkeresultatKategorier.First(x => x.Text.Contains("MC")).Click();

        }

        public void SøktPå(string søketerm)
        {
            _webDriver.GåTilUrl($"https://www.finn.no/globalsearchlander.html?searchKeys=&q={søketerm}");
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
