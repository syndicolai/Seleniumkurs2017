using PageObjects.Byggere;
using NUnit.Framework;
using FluentOgByggere.Testdata;

namespace PageObjects.Tester
{
    [TestFixture]
    public class FinnSeleniumTest : FinnSøkBygger
    {
        [Test]
        public void Finn_Startside_SøkefeltEksisterer()
        {
            Gitt.At.Vi.NavigerTilStartside();
            Så.Skal.SøkefeltetEksistere();
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_EndrerSøkIUrl()
        {
            var søketerm = MotorsykkelData.Get();
            Gitt.At.Vi.NavigerTilStartside();
            Når.Vi.UtførerSøk(søketerm);
            Så.Skal.UrlInneholde(søketerm);
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_ViserMcSøkeresultatKategori()
        {
            Gitt.At.Vi.NavigerTilStartside();

            Når.Vi.UtførerSøk(MotorsykkelData.Get());
            Så.Skal.SøkeresultatKategorierInneholdeKategori("MC");
        }

        [Test]
        public void Finn_SøkPåMotorsykkel_VisResultater()
        {
            Gitt.At.Vi.Har.SøktPå(MotorsykkelData.Get());
            Når.Vi.VelgerKategori("MC");
            Så.Skal.Det.VisesResultater();
        }

        [Test]
        public void Finn_Resultatliste_SorterPåPris_LaverstFørst()
        {
            Gitt.At.Vi.Har.SøkeresultaterFor(MotorsykkelData.Get());
            Når.Vi.SortererSøkeresultater();
            Så.Skal.LavestPrisVæreFørstIListen();
        }
    }
}
