using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public class Player
    {
        public string name = "<name>";
        public Player()
        {
            Console.Write("Player name: ");
            string input = Console.ReadLine();

            if (input != "") { name = input; }
        }
    }
}
