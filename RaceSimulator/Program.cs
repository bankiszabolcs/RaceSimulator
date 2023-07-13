﻿using System.Runtime.CompilerServices;
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
                "- A könnyebb tesztelhetőség végett 1 óra 2 másodpercnek felel meg. \n" +
                "- Az egyes versenyzők előrehaladását óránként vizsgáljuk. \n" +
                "- A változékony időjárás befolyásolja a versenyben részt vevő járművek viselkedését. \n" +
                "- Minden órában 30% az esély arra, hogy esik az eső. \n" +
                "- A verseny végeztével az eredmény kiírása kerül.\n");
            Console.WriteLine("A játék kezdéséhez üss egy \"Enter\"-t");
            int yourCharAscii = Console.Read();
            if (yourCharAscii == 13) {
                Game newGame = new Game();
                newGame.StartGame();
                Console.ReadKey();
            }
        }

       
    }
}
