using System;

namespace FinnSeleniumTest.Testdata
{
    public static class MotorsykkelGenerator
    {
        private static readonly string[] Motorsykler = 
            { "BMW K1300", "Yamaha FZ1", "Honda", "Husqvarna" };

        public static string Get()
        {
            return Motorsykler[new Random().Next(Motorsykler.Length - 1)];
        }
    }
}
