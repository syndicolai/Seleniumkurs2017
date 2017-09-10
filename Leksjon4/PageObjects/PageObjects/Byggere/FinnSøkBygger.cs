using System;
using PageObjects.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Chrome;
using FluentOgByggere.PageObjects;
using PageObjects.Konstanter;
using System.Text.RegularExpressions;

namespace PageObjects.Byggere
{
    public class FinnSøkBygger : FluentBase<FinnSøkBygger>
    {
        private IWebDriver _webDriver;
        private StartsidePage _startsidePage;
        private SøkeresultatPage _søkeresultatPage;
        private ResultatKategoriPage _resultatKategoriPage;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            _startsidePage = new StartsidePage(_webDriver);
            _søkeresultatPage = new SøkeresultatPage(_webDriver);
            _resultatKategoriPage = new ResultatKategoriPage(_webDriver);
        }

        public void SøkefeltetEksistere()
        {
            var element = _startsidePage.HentSøkeelement();
            Assert.IsNotNull(element, $"Forventet å finne element med id {ElementKonstanter.SøkefeltId}");
        }

        public void NavigerTilStartside()
        {
            _startsidePage.NavigerTil();
        }

        public void TittelenPåSidenVære(string forventetTittel)
        {
            Assert.AreEqual(forventetTittel, _webDriver.Title);
        }

        public void UtførerSøk(string søkestring)
        {
            _startsidePage.UtførSøk(søkestring);
        }

        public void LavestPrisVæreFørstIListen()
        {
            var førstePris = _søkeresultatPage.FinnPris(0);
            var andrePris = _søkeresultatPage.FinnPris(1);
            
            Assert.LessOrEqual(førstePris, andrePris);
        }

        public void UrlInneholde(string forventetInnhold)
        {
            Assert.That(_webDriver.Url.Contains(forventetInnhold));
        }

        public void SøkeresultatKategorierInneholdeKategori(string forventetKategori)
        {
            var kategorier = _resultatKategoriPage.HentKategorier();
            Assert.That(kategorier.Any(kategori => kategori.Contains(forventetKategori)), $"Forventet å finne søkekategori '{forventetKategori}'");
        }

        public void VisesResultater()
        {
            var results = _søkeresultatPage.HentSøkeresultater();
            Assert.That(results.Any(), "Forventet å finne søkeresultater.");
        }

        public void VelgerKategori(string kategoritekst)
        {
            _resultatKategoriPage.VelgKategori(kategoritekst);
        }

        public void SøktPå(string søketerm)
        {
            _resultatKategoriPage.NavigerTil($"https://www.finn.no/globalsearchlander.html?searchKeys=&q={søketerm}");
        }
        
        public void SøkeresultaterFor(string søketerm)
        {
            _webDriver.Navigate().GoToUrl($"https://www.finn.no/mc/all/search.html?q={søketerm}");
        }

        public void SortererSøkeresultater()
        {
            _søkeresultatPage.SorterResultaterEtter("Pris lav-høy");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
        }
    }
}
