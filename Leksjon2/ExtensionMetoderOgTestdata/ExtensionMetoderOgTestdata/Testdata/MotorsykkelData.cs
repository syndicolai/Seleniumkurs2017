using System;

namespace FluentSeleniumOgStruktur.Testdata
{
    public static class MotorsykkelData
    {
        private static readonly Random Random = new Random();

        private static readonly string[] Data = { "BMW", "Suzukie", "Honda", "Kawasakie" };

        public static string Get()
        {
            return Data[Random.Next(Data.Length - 1)];
        }

    }
}
