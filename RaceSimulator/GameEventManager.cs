using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal class GameEventManager
    {
        public delegate void GameEvent();

        public static event GameEvent OnGameStart, OnGameEnd, OnRun, OnRainStart,
            OnRainEnd;

        public static void TriggerGameStart()
        {
            if(OnGameStart != null)
            {
                Console.WriteLine("Verseny elkezdődött.");
                OnGameStart();
            }
        }

        public static void TriggerGameEnd()
        {
            if(OnGameEnd != null)
            {
                Console.WriteLine("Verseny véget ért.");
                OnGameEnd();
            }
        }

        public static void TriggerOneHourRun(int hour)
        {
            if(OnRun != null){ 
                OnRun();
                Console.WriteLine("{0}:00 - Verseny állása:", hour);
                Game.DisplayResult(10,Game.allCars);

                Console.WriteLine("ESEMÉNYEK");
                for (int i = 0; i < Logger.eventContainer.Count; i++)
                {
                    Console.WriteLine(Logger.eventContainer.ElementAt(i));
                } 
            }
        }

        public static void TriggerRainStart(bool isItRaining, int hour)
        {
            if(OnRainStart != null)
            {
                string actualEvent = isItRaining? "Még mindig esik.":"Az eső rákezdett.";
                Logger.eventContainer.Add(Logger.getId(), hour+":00: "+actualEvent);
                OnRainStart();
            }
        }

        public static void TriggerRainEnd(bool isItRaining, int hour)
        {
            if (OnRainEnd != null)
            {
                string actualEvent = "Eső befejeződött";
                Logger.eventContainer.Add(Logger.getId(), hour + ":00: " + actualEvent);
                OnRainEnd();
            }
        }
    }
}
