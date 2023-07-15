using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RaceSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            } while (!int.TryParse(Console.ReadLine(), out Game.TIMELEAP));      
            
            Console.WriteLine("A játék kezdéséhez üss egy \"Enter\"-t");
            int yourCharAscii = Console.Read();
            if (yourCharAscii == 13) {
                Game newGame = new Game();
                newGame.StartGame();
                Console.ReadKey();
            }

           while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape && Game.isPlaying == true))
            {
                Game.Pause();
                if ((Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter))
                {
                    Game.Continue(Game.duration);
                    Console.ReadKey();
                }
            }

           

        }

       
    }
}
