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
                Console.WriteLine("  _____ ___ _   _ ___ ____  _   _ \r\n |  ___|_ _| \\ | |_ _/ ___|| | | |\r\n | |_   | ||  \\| || |\\___ \\| |_| |\r\n |  _|  | || |\\  || | ___) |  _  |\r\n |_|   |___|_| \\_|___|____/|_| |_|\r\n                                  ");
                OnGameEnd();
            }
        }

        public static void TriggerOneHourRun(int hour)
        {
            if(OnRun != null){ 
                OnRun();
                Console.WriteLine("  ____                \r\n |  _ \\ __ _  ___ ___ \r\n | |_) / _` |/ __/ _ \\\r\n |  _ < (_| | (_|  __/\r\n |_| \\_\\__,_|\\___\\___|");
                Console.WriteLine("\n{0}:00 - Verseny állása:", hour);
                Game.DisplayResult(10,Game.allCars);

                ListEvents();
            }
        }

        private static void ListEvents()
        {
            var logs = Logger.eventContainer;
            //Console.WriteLine("\033[30m \033[107m ESEMÉNYEK ");
            Console.WriteLine("\n\x1b[30;47mESEMÉNYEK");
            for (int i = logs.Count <= 10? 0 : logs.Count-10; i < logs.Count; i++)
            {
                if (logs.ElementAt(i).Key.Contains(EventType.WEATHER.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (logs.ElementAt(i).Key.Contains(EventType.FAILURE.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (logs.ElementAt(i).Key.Contains(EventType.CIRCUIT_CLEAR.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine((i+1)+". "+logs.ElementAt(i).Value);
                Console.ResetColor();
            }
        }

        public static void TriggerRainStart(bool isItRaining, int hour)
        {
            if(OnRainStart != null)
            {
                string actualEvent = isItRaining? "Még mindig esik.":"Az eső rákezdett.";
                Logger.eventContainer.Add(Logger.getId()+EventType.WEATHER.ToString(), hour+":00: "+actualEvent);
                OnRainStart();
            }
        }

        public static void TriggerRainEnd(bool isItRaining, int hour)
        {
            if (OnRainEnd != null)
            {
                string actualEvent = "Eső befejeződött.";
                Logger.eventContainer.Add(Logger.getId() + EventType.WEATHER.ToString(), Logger.getFullEventString(actualEvent, hour));
                OnRainEnd();
            }
        }
    }
}
