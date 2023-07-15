﻿using System;
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

                ListEvents();
            }
        }

        private static void ListEvents()
        {
            Console.WriteLine("ESEMÉNYEK");
            for (int i = 0; i < Logger.eventContainer.Count; i++)
            {
                if (Logger.eventContainer.ElementAt(i).Key.ToString().Contains(EventType.WEATHER.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Logger.eventContainer.ElementAt(i).Key.ToString().Contains(EventType.FAILURE.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (Logger.eventContainer.ElementAt(i).Key.ToString().Contains(EventType.CIRCUIT_CLEAR.ToString()))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine((i+1)+". "+Logger.eventContainer.ElementAt(i).Value);
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
