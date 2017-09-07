using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

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
            Assert.That(_webDriver.Url.Equals("https://www.finn.no/"));
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            _webDriver.Navigate().GoToUrl("http://www.finn.no");
            var søkefelt = _webDriver.FindElement(By.Id("search"));
            søkefelt.SendKeys("BMW");

            var søkeknapp = _webDriver.FindElements(By.ClassName("primary")).First();
            søkeknapp.Click();

            Assert.That(_webDriver.Url.Contains("BMW"));
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

        [TearDown]
        public void TearDown()
        {
            _webDriver.Dispose();
        }
    }
}
