﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal class Truck : Vehicle
    {
        int[] names = new int[10];
        Random dice = new Random();
        public delegate void BreakDown();
        public static event BreakDown OnFailureStart;
        private bool brokedDown = false;
        private int failureHourCounter = 0;
        private int runningCounter = 0;
        public Truck() {

            this.Name = GetUniqueName();
            GameEventManager.OnGameStart += Start;
            GameEventManager.OnGameEnd += Stop;
            GameEventManager.OnRun += Run;
        }

        private string GetUniqueName()
        {
            int actualName = dice.Next(0, 1000);
            if (names.Contains(actualName))
            {
               return GetUniqueName();
            }
            else
            {
                return actualName.ToString();
            }
        }

        public override void Start()
        {
            this.Speed = 100;
        }

        protected override void Run()
        {
            runningCounter++;
            if(this.failureHourCounter == 2)
            {
                Start();
                this.failureHourCounter = 0;
                this.Run();
                string actualEvent = ($"Truck {this.Name} javítása befejeződött. Folytathatja a versenyt.");
                Logger.eventContainer.Add(Logger.getId(), runningCounter + ":00: " + actualEvent);
            }
            if (this.Speed == 0)
            {
                failureHourCounter++;
                return;
            }
            if (dice.Next(0, 100) <= 5)
            {
                Stop();
                this.brokedDown = true;
                failureHourCounter++;
                if (OnFailureStart != null)
                {
                    string actualEvent = $"Truck {this.Name} lerobbant a pályán.";
                    Logger.eventContainer.Add(Logger.getId(), runningCounter + ":00: " + actualEvent);
                    OnFailureStart();
                }
            }
            else
            {
                base.Run();
            }
        }

    }
}
