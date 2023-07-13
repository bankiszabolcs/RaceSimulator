using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator 
{
    internal class Motor : Vehicle, ISlowDown
    {
        Random dice = new Random();

        private static int currentId;
        public Motor() {
            this.Name = $"Motor {this.getNextId()}";
            GameEventManager.OnGameStart += Start;
            GameEventManager.OnGameEnd += Stop;
            GameEventManager.OnRainStart += Slow;
            GameEventManager.OnRainEnd += Start;
            GameEventManager.OnRun += Run;
        }

        public void Slow()
        {
            this.Speed -= dice.Next(5, 10);
        }

        public override void Start()
        {
            this.Speed = 100;
        }

        protected int getNextId()
        {
            return currentId++; 
        }

    }
}
