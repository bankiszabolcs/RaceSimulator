using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal static class Logger
    {
        private static int Id=1;
        public static Dictionary<int, string> eventContainer = new Dictionary<int, string>();

        public static int getId()
        {
            return Id++;
        }
    }
}
