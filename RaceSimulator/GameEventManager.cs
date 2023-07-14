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
                OnRun();
                if(hour % 5 == 0)
                {
                Console.WriteLine("{0}. hour - Result is the following:", hour);
                    Game.DisplayResult(10,Game.allCars);   
                }
                else
                {
                Console.WriteLine("{0}. hour", hour);
                }
            }
        }

        public static void TriggerRainStart(bool isItRaining)
        {
            if(OnRainStart != null)
            {
               Console.WriteLine(isItRaining? "It is still raining.":"It has started to rain.");
               OnRainStart();
            }
        }

        public static void TriggerRainEnd(bool isItRaining)
        {
            if (OnRainEnd != null)
            {
                Console.WriteLine("It ceases raining");
                OnRainEnd();
            }
        }
    }
}
