using OpenQA.Selenium;
using PageObjects.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace FluentOgByggere.PageObjects
{
    public class SøkeresultatPage : PageObjectBase
    {
        public SøkeresultatPage(IWebDriver driver) : base(driver)
        {
        }

        public List<IWebElement> HentSøkeresultater()
        {
            var resultContainer = Driver.FinnElement(By.Id("page-results"));

            return resultContainer.FinnElementer(By.ClassName("result-item")).ToList();
        }

        public void SorterResultaterEtter(string kriterie)
        {
            var sorteringselement = Driver.FinnElement(By.Id("sort"));
            sorteringselement.SkrivTekstIElement(kriterie);
            sorteringselement.SkrivTekstIElement(Keys.Enter);
        }

        public int FinnPris(int resultatRad)
        {
            var resultatliste = Driver.FinnElementer(By.ClassName("result-item"));
       
            var prisElement = resultatliste[resultatRad].FinnElementer(By.ClassName("inlineblockify")).Last();

            return int.Parse(Regex.Replace(prisElement.Text, @"[^\d]", string.Empty));
        }
    }
}
