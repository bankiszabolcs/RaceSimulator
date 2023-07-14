using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal abstract class Vehicle
    {
        public string Name { get; set; }
        public int Speed { get; set; } = 0;

        public int Distance { get; set; } = 0;

  /*      public Vehicle() {
            GameEventManager.OnRun += Run;
        }*/

        protected virtual void Run()
        {
            this.Distance += this.Speed;
        }

        public abstract void Start();

        public void Stop()
        {
            this.Speed = 0;
        }

        public string GetNameWithType()
        {
            return this.GetType().ToString().Split('.')[1];
        }
    }
}
