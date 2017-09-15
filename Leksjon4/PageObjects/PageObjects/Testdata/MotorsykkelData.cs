using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentOgByggere.Testdata
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
