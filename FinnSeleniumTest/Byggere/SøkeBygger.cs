using System;
using System.Linq;
using FinnSeleniumTest.Ordliste;
using FinnSeleniumTest.PageObjects;
using FinnSeleniumTest.Testdata;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace FinnSeleniumTest.Byggere
{
    public class SøkeBygger : FluentOrdliste<SøkeBygger>
    {
        protected ChromeDriver WebDriver;
        protected StartsidePage Startside;
        protected KategoriResultatPage KategoriResultatPage;
        protected SøkeresultatPage SøkeresultatPage;

        [SetUp]
        public void Oppsett()
        {
            WebDriver = new ChromeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Startside = new StartsidePage(WebDriver);
            KategoriResultatPage = new KategoriResultatPage(WebDriver);
            SøkeresultatPage = new SøkeresultatPage(WebDriver);
        }

        public void SøkefeltetVærePåSiden()
        {
            Assert.IsNotNull(Startside.SøkefeltEksisterer(), $"Fant ikke søkefelt på startsiden. \n{WebDriver.Url}");
        }

        public void NavigererTilStartsiden()
        {
            Startside.Hjem();
        }
    
        [TearDown]
        public void Opprydning()
        {
            WebDriver.Quit();
            Startside.Dispose();
        }

        public void SøkerPåMotorsykkel()
        {
            Startside.SøkerPåMotorsykkel();
        }

        public void KategorisidenViseTreff()
        {
            var kategorier = KategoriResultatPage.HentKategorier();
            Assert.That(kategorier.Any(x => x.Contains("MC")), "Forventet å finne kategori MC");
        }

        public void SøktPåMotorsykler()
        {
            KategoriResultatPage.Tilstand = $"?searchKeys=&q={MotorsykkelGenerator.Get()}";
            KategoriResultatPage.Hjem();
        }

        public void VelgerMotorsykkelkategori()
        {
            KategoriResultatPage.VelgMotorsykkelKategori();
        }

        public void SøkeresultaterVises()
        {
            Assert.IsTrue(SøkeresultatPage.ErPåSiden, "Forventet å være på søkeresultatsiden.");
        }

        public void SøkeresultatlistenInneholderResultater()
        {
            SøkeresultatPage.Tilstand = MotorsykkelGenerator.Get();
            SøkeresultatPage.Hjem();
        }

        public void SortererPåPris()
        {
            SøkeresultatPage.SorterPåPris();
        }

        public void LavestePrisLiggeFørst()
        {
            var førstePris = SøkeresultatPage.FinnPris(0);
            var andrePris = SøkeresultatPage.FinnPris(1);

            Assert.LessOrEqual(førstePris, andrePris, "Forventet at laveste pris kom først!");
        }
    }
}
