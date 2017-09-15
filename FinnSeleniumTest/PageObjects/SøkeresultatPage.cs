using System.Linq;
using System.Text.RegularExpressions;
using FinnSeleniumTest.Utvidelser;
using OpenQA.Selenium;

namespace FinnSeleniumTest.PageObjects
{
    public class SøkeresultatPage : PageObjectBase
    {
        private readonly By _sorteringsSelector = By.Id("sort");
        private readonly By _resultatSelector = By.ClassName("result-item");
        private readonly By _resultatInfoSelector = By.ClassName("inlineblockify");

        private readonly string PrisLavHøyKriterie = "Pris lav-høy";   
    

        public string Tilstand { get; set; } = string.Empty;
        public SøkeresultatPage(IWebDriver driver) : base(driver)
        {
        }

        public override string StartUrl => $"https://www.finn.no/mc/all/search.html?q={Tilstand}";

        public void SorterPåPris()
        {
            var sorternedtrekksliste = Driver.FinnUniktElement(_sorteringsSelector);
            sorternedtrekksliste.SendKeys(PrisLavHøyKriterie);
            sorternedtrekksliste.SendKeys(Keys.Enter);
        }

        public int FinnPris(int resultatRad)
        {
            var resultatliste = Driver.FindElements(_resultatSelector);

            var prisElement = resultatliste[resultatRad].FindElements(_resultatInfoSelector).Last();

            return int.Parse(Regex.Replace(prisElement.Text, @"[^\d]", string.Empty));
        }
    }
}
