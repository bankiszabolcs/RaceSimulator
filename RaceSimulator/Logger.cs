using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    enum EventType { CIRCUIT_CLEAR, WEATHER, FAILURE }
    internal static class Logger
    {
        
        private static int Id=1;
        public static Dictionary<string, string> eventContainer = new Dictionary<string, string>();

        public static int getId()
        {
            return Id++;
        }

        public static string getFullEventString(string actualEvent, int hour) {
            return hour + ":00: " + actualEvent;
        }
    }
}
