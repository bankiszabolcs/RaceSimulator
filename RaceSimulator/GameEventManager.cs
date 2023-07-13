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

        //public static Dictionary<String, Vehicle> allCars = new Dictionary<String, Vehicle>();

        public static void TriggerGameStart()
        {
            if(OnGameStart != null)
            {
                Console.WriteLine("The game has started");
                OnGameStart();
            }
        }

        public static void TriggerGameEnd()
        {
            if(OnGameEnd != null)
            {
                Console.WriteLine("The game has ended");
                OnGameEnd();
            }
        }

        public static void TriggerOneHourRun(int hour)
        {
            if(OnRun != null){ 
                if(hour % 5 == 0)
                {
                Console.WriteLine("{0}. hour result is the following:", hour);
                }
                else
                {
                Console.WriteLine("{0}. hour", hour);
                }
                OnRun();
            }
        }

        public static void TriggerRainStart()
        {
            if(OnRainStart != null)
            {
                Console.WriteLine("It has started to rain.");
                OnRainStart();
            }
        }

        public static void TriggerRainEnd()
        {
            if (OnRainEnd != null)
            {
                Console.WriteLine("It ceases raining");
                OnRainEnd();
            }
        }
    }
}
