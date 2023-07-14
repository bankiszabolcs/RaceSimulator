using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RaceSimulator.Truck;

namespace RaceSimulator
{
    internal class Game
    {
        //Dictionary lehet gyorsabb
        private Car[] cars = new Car[10];
        private Motor[] motors = new Motor[10];
        private Truck[] trucks = new Truck[10];
        private Dictionary<String, Vehicle> allCars = new Dictionary<String, Vehicle>();

        private int duration = 1;
        private Random dice = new Random();
        private Timer timer;
        private int? timeOfTrackFailure = null;
        public delegate void BreakDown();
        public static event BreakDown OnFailureEnd;
        private bool isItRaining = false;

        public Game() {
            Truck.OnFailureStart += SaveTruckFailureTime;
            OnFailureEnd += ResetFailureTime;
        }

        public void StartGame()
        {
            CreateVehicles();
            FillFields();
            DisplayRacers();
            Run();
        }

        private void CreateVehicles()
        {
            for (int i = 0; i < 10; i++)
            {
                cars[i] = new Car();
                motors[i] = new Motor();
                trucks[i] = new Truck();
            }
        }

        private void FillFields()
        {
            foreach (var car in cars)
            {
                allCars.Add(car.Name, car);
            }

            foreach (var motor in motors)
            {
                allCars.Add(motor.Name, motor);
            }

            foreach (var truck in trucks)
            {
                allCars.Add(truck.Name, truck);
            }
        }

        private void DisplayRacers()
        {
            Console.WriteLine("Racers:");
            for (int i = 0; i < allCars.Count; i++)
            {
                var actualVehicle = allCars.ElementAt(i).Value;
                
                if(i%3==0)
                {
                    Console.Write("\n");
                }

                Console.Write("{0} ({1}) | ",actualVehicle.Name, actualVehicle.GetType().ToString().Split('.')[1]);
            }
            Console.WriteLine("\n");
        }
       
        private void Run()
        {
            GameEventManager.TriggerGameStart();
            //TODO: kéne egy ide egy pause meg start gomb
            timer = new Timer(StepOneHour, null, 0, 1000);
        }

        private void StepOneHour(Object o)
        {
            if(duration == timeOfTrackFailure + 3 && OnFailureEnd != null) 
            {                
               OnFailureEnd();
                Console.WriteLine("There is no more damaged car on the circuit!");
            }
            if (duration <= 10)
            {
                if (dice.Next(1, 10) > 3)
                {
                    if(isItRaining) GameEventManager.TriggerRainEnd(isItRaining);
                    isItRaining = false;
                    GameEventManager.TriggerOneHourRun(duration);
                }
                else
                {
                    GameEventManager.TriggerRainStart(isItRaining);
                    isItRaining = true;
                    GameEventManager.TriggerOneHourRun(duration);
                }
                if (duration % 5 == 0)
                {
                    DisplayResult(10);
                }
                duration++;
                GC.Collect();
            }
            else
            {
                Stop();
            }
        }

        private void Stop()
        {
            Console.WriteLine("Game has ended. \n Result is the following:");
            DisplayResult(allCars.Count);
            GameEventManager.TriggerGameEnd();
            timer.Dispose();
        }

        private void SaveTruckFailureTime()
        {
            this.timeOfTrackFailure = this.duration;
        }

        private void ResetFailureTime()
        {
            this.timeOfTrackFailure = null;
        }

        private void DisplayResult(int numberOfCars)
        {
           allCars = allCars.OrderBy(x => -x.Value.Distance).ToDictionary(x => x.Key, x => x.Value);

            for (int i = 0; i < numberOfCars; i++)
            {
                Vehicle actualCar = allCars.ElementAt(i).Value;
                Console.WriteLine("{0}. {1} ({2}) {3} km", i+1, actualCar.Name, actualCar.GetType().ToString().Split('.')[1], actualCar.Distance);
            }
        }
    }
}
