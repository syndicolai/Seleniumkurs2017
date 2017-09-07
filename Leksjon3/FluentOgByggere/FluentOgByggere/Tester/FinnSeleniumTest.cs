using FluentOgByggere.Byggere;
using NUnit.Framework;

namespace FluentOgByggere.Tester
{
    [TestFixture]
    public class FinnSeleniumTest : FinnSøkBygger
    {
        [Test]
        public void HelloFinn()
        {
            Gitt.At.Vi.NavigererTilUrl("https://www.finn.no/");
            Så.Skal.TittelenPåSidenVære("FINN.no – Mulighetenes marked");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            Gitt.At.Vi.NavigererTilUrl("http://www.finn.no");
            Når.Vi.UtførerSøk("BMW");
            Så.Skal.UrlInneholde("BMW");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_ViserMcSøkeresultatKategori()
        {
            Gitt.At.Vi.NavigererTilUrl("http://www.finn.no");

            Når.Vi.UtførerSøk("BMW K1200");
            Så.Skal.SøkeresultatKategorierInneholdeKategori("MC");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_VisResultater()
        {
            Gitt.At.Vi.Har.SøktPå("BMW K1200");
            Når.Vi.VelgerKategori("MC");
            Så.Skal.Det.VisesResultater();
        }
    }
}
