using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static RaceSimulator.Truck;

namespace RaceSimulator
{
    internal class Game
    {
        private Car[] cars = new Car[10];
        private Motor[] motors = new Motor[10];
        private Truck[] trucks = new Truck[10];
        public static Dictionary<String, Vehicle> allCars = new Dictionary<String, Vehicle>();

        public static int duration = 1;
        public static bool isPlaying = false;
        private static Random dice = new Random();
        private static Timer timer;
        private static int? timeOfTrackFailure = null;
        public delegate void BreakDown();
        public static event BreakDown OnFailureEnd;
        private static bool isItRaining = false;
        private static int TIMELEAP = 2;

        public Game() {
            Truck.OnFailureStart += SaveTruckFailureTime;
            OnFailureEnd += ResetFailureTime;
        }

        public void StartGame()
        {
            CreateVehicles();
            FillFields();
            DisplayRacers();
            Run(0);
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
            Console.WriteLine("Versenyzők:");
            for (int i = 0; i < allCars.Count; i++)
            {
                var actualVehicle = allCars.ElementAt(i).Value;
                
                if(i%3==0)
                {
                    Console.Write("\n");
                }

                Console.Write("{0} ({1}) | ",actualVehicle.Name, actualVehicle.GetNameWithType());
            }
            Console.WriteLine("\n");
            Console.WriteLine("Folytatáshoz nyomj meg egy billentyűt");
            Console.ReadKey();
        }
       
        private static void Run(int startingPoint)
        {
            GameEventManager.TriggerGameStart();
            isPlaying = true;
            timer = new Timer(StepOneHour, null, startingPoint, TIMELEAP*1000);
        }
              
        private static void StepOneHour(Object o)
        {
            Console.Clear();
            if (duration == timeOfTrackFailure + 3 && OnFailureEnd != null) 
            {                
               OnFailureEnd();
            }
            if (duration <= 15)
            {
                if (dice.Next(1, 10) > 3)
                {
                    if (isItRaining) GameEventManager.TriggerRainEnd(isItRaining, duration);
                    isItRaining = false;
                    GameEventManager.TriggerOneHourRun(duration);
                }
                else
                {
                    GameEventManager.TriggerRainStart(isItRaining, duration);
                    isItRaining = true;
                    GameEventManager.TriggerOneHourRun(duration);
                }
                duration++;
                GC.Collect();
            }
            else
            {
                Stop();
            }
        }

        private static void Stop()
        {
            Console.WriteLine("Verseny véget ért. \n Az eredmény a következő");
            DisplayResult(allCars.Count, allCars);
            GameEventManager.TriggerGameEnd();
            timer.Dispose();
            isPlaying = false;
        }

        public static void Pause()
        {
            isPlaying = false;
            timer.Dispose();
        }

        public static void Continue(int time)
        {
            isPlaying = true;
            timer = new Timer(StepOneHour, null, time, TIMELEAP * 1000);
        }

        private void SaveTruckFailureTime()
        {
            timeOfTrackFailure = duration;
        }

        private void ResetFailureTime()
        {
            timeOfTrackFailure = null;
            string actualEvent = "Nincs több lerobbant autó a pályán!";
            Logger.eventContainer.Add(Logger.getId() + EventType.CIRCUIT_CLEAR.ToString(), Logger.getFullEventString(actualEvent, duration-1));
        }

        public static void DisplayResult(int numberOfCars, Dictionary<String, Vehicle> allCars)
        {
           allCars = allCars.OrderBy(x => -x.Value.Distance).ToDictionary(x => x.Key, x => x.Value);
            for (int i = 0; i < numberOfCars; i++)
            {
                Vehicle actualCar = allCars.ElementAt(i).Value;
                Console.WriteLine("{0}. {1} ({2}) {3} km - {4} km/h", i+1, actualCar.Name, actualCar.GetNameWithType(), actualCar.Distance, actualCar.Speed);
            }
        }
    }
}
