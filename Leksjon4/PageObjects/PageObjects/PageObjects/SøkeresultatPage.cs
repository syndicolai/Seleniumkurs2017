using OpenQA.Selenium;
using PageObjects.Extensions;
using System.Collections.Generic;
using System.Linq;

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
    }
}
