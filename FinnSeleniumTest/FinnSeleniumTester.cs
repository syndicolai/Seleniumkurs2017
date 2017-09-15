
using FinnSeleniumTest.Byggere;
using NUnit.Framework;

namespace FinnSeleniumTest
{
    [TestFixture]
    public class FinnSeleniumTester : SøkeBygger
    {
       
        [Test]
        public void Startside_Åpnet_SøkefeltetEksisterer()
        {
            Når.LilleMartin.NavigererTilStartsiden();
            Så.Skal.SøkefeltetVærePåSiden();
        }

        [Test]
        public void Startside_SøkerEtterMotorsykkel_KategoriResultat()
        {
            Gitt.At.LilleMartin.NavigererTilStartsiden();
            Når.LilleMartin.SøkerPåMotorsykkel();
            Så.Skal.KategorisidenViseTreff();
        }

        [Test]
        public void KategoriResultat_VelgerMotorsykkelKategori_ViserSøkeresultat()
        {
            Gitt.At.LilleMartin.Har.SøktPåMotorsykler();
            Når.LilleMartin.VelgerMotorsykkelkategori();
            Så.Skal.SøkeresultaterVises();
        }

        [Test]
        public void Søkeresultat_SorterPåLavHøyPris_LavestFørst()
        {
            Gitt.At.SøkeresultatlistenInneholderResultater();
            Når.LilleMartin.SortererPåPris();
            Så.Skal.LavestePrisLiggeFørst();
        }
    }
}
