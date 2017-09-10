using System;
using FluentOgByggere.Base;
using NUnit.Framework;
using FluentOgByggere.Extensions;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using FluentOgByggere.Konstanter;
using System.Text.RegularExpressions;

namespace FluentOgByggere.Byggere
{
    public class FinnSøkBygger : FluentBase<FinnSøkBygger>
    {
        public void NavigererTilUrl(string url)
        {
            _webDriver.Navigate().GoToUrl(url);
        }

        public void SøkefeltetEksistere()
        {
            Assert.IsNotNull(_webDriver.FinnElement(By.Id(ElementKonstanter.SøkefeltId)), $"Forventet å finne element med id {ElementKonstanter.SøkefeltId}");
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


        public void SøkeresultaterFor(string søketerm)
        {
            _webDriver.Navigate().GoToUrl($"https://www.finn.no/mc/all/search.html?q={søketerm}");
           
        }

        public void SortererSøkeresultater()
        {
            var sorteringselement = _webDriver.FinnElement(By.Id("sort"));
            sorteringselement.SkrivTekstIElement("Pris lav-høy");
            sorteringselement.SkrivTekstIElement(Keys.Enter);
        }

        public void LavestPrisVæreFørstIListen()
        {
            var prisWrappers = _webDriver.FinnElementer(By.ClassName("result-item"));
            Assert.IsNotNull(prisWrappers?.FirstOrDefault());
            var førstePrisElement = prisWrappers[0].FinnElementer(By.ClassName("inlineblockify"));
            Assert.IsNotNull(prisWrappers[1]);

            var andrePriselement = prisWrappers[1].FinnElementer(By.ClassName("inlineblockify"));
            var førstePris = Regex.Replace(førstePrisElement.Last().Text, @"[^\d]", string.Empty);
            var andrePris = Regex.Replace(andrePriselement.Last().Text, @"[^\d]", string.Empty);

            Assert.LessOrEqual(int.Parse(førstePris), int.Parse(andrePris));
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
