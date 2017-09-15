using OpenQA.Selenium;
using PageObjects.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace FluentOgByggere.PageObjects
{
    public class ResultatKategoriPage : PageObjectBase
    {
        public ResultatKategoriPage(IWebDriver driver) : base(driver)
        {
        }

        public void VelgKategori(string kategoritekst)
        {
            var søkeresultatKategorier = Driver.FinnElementer(By.ClassName("dropdown-link"));
            søkeresultatKategorier.First(x => x.Text.Contains("MC")).Click();
        }

        public List<string> HentKategorier()
        {
            var søkeresultatKategorier = Driver.FinnElementer(By.ClassName("dropdown-link"));
            return søkeresultatKategorier.Select(element => element.Text).ToList();
        }
    }
}
