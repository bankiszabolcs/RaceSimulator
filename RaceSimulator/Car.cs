using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal class Car : Vehicle, ISlowDown
    {
        private int originalSpeed = 0;

        Random dice = new Random();
        static List<String> carNames = new List<String>() {
            "Alabaster",
            "Blitz",
            "Cobra",
            "Vindicator",
            "Roamer",
            "Blade",
            "Prodigy",
            "Catalyst",
            "Dynamics",
            "Eon"
        };

        static List<String> carNames2 = new List<String>() {
            "Motion",
            "Striker",
            "Jazz",
            "Tracer",
            "Pulse",
            "Intro",
            "Paragon",
            "Eon",
            "Surge",
            "Charm"
        };

        public Car() {
            string actualCarName1 = carNames[dice.Next(0, carNames.Count-1)];
            carNames.Remove(actualCarName1);
            string actualCarName2 = carNames2[dice.Next(0, carNames2.Count-1)];
            carNames2.Remove(actualCarName2);
            this.Name = actualCarName1 + " " + actualCarName2;
            GameEventManager.OnGameStart += Start;
            GameEventManager.OnGameEnd += Stop;
            Truck.OnFailureStart += Slow;
            GameEventManager.OnRun += Run;

        }

        public void Slow()
        {
            this.Speed = 75;
        }

        public void PickUpSpeed()
        {
            this.Speed = this.originalSpeed;
        }

        public override void Start()
        {
            this.Speed = dice.Next(80, 110);
            this.originalSpeed = this.Speed;
        }
    }
}
