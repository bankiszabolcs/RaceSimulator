using System;
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
        public static event BreakDown OnFailureStart, OnFailureEnd;
        private bool brokedDown = false;
        public Truck() {

            this.Name = GetUniqueName();
            GameEventManager.OnGameStart += Start;
            GameEventManager.OnGameEnd += Stop;
            GameEventManager.OnRun += Run;
            Game.OnFailureEnd += Run;
            Game.OnFailureEnd += Repaired;
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

        private void Repaired()
        {
            if (this.brokedDown)
            {
                Console.WriteLine("Truck {0} has been repaired", this.Name);
                this.brokedDown = !this.brokedDown;
            }
        }

        protected override void Run()
        {
            
            if (dice.Next(0, 100) <= 2)
            {
                Stop();
                this.brokedDown = true;
                if (OnFailureStart != null)
                {
                    Console.WriteLine("{0} has broken down on the circuit.", this.Name);
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
