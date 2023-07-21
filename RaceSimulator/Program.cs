using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using WindowsInput;

namespace RaceSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Race Simulator";
            Console.WriteLine("- A versenyben 3 típusú jármű vesz részt: Autó, Motor, Kamion. \n" +
                "- Mindegyik típusból 10 db versenyez 50 órán keresztül.\n" +
                "- A könnyebb tesztelhetőség végett 1 óra X másodpercnek felel meg, ahol az X értéket a felhasználó adhatja meg. \n" +
                "- Az egyes versenyzők előrehaladását óránként vizsgáljuk. \n" +
                "- A változékony időjárás befolyásolja a versenyben részt vevő járművek viselkedését. \n" +
                "- Minden órában 30% az esély arra, hogy esik az eső. \n" +
                "- A verseny végeztével az eredmény kiírása kerül.\n");

            do
            {
                Console.Write("Add meg egy óra hány másodpercnek feleljen meg a szimulációba: ");
            } while (!int.TryParse(Console.ReadLine(), out Game.TIMELEAP) || Game.TIMELEAP < 1);      
            
            Console.WriteLine("A játék kezdéséhez üss egy \"Enter\"-t");
            int yourCharAscii = Console.Read();
            if (yourCharAscii == 13) {
                Game newGame = new Game();
                newGame.StartGame();
                Console.ReadKey();
            }

            int counter = 0;

            while (true)
            {  
                if(counter == 0)
                {
                InputSimulator inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                    counter++;
                }


                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Escape && Game.isPlaying)
                    {
                        Game.Pause();
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Game.Continue(Game.duration);
                        Console.ReadKey(true);
                        counter = 0;
                    }
                }
            }

        }

       
    }
}
