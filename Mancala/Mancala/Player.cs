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
        private int score = 0;
        public Player()
        {
            Console.Write("Player name: ");
            string input = Console.ReadLine();

            if (input != "") { name = input; }
        }

        public int getScore()
        { return this.score; }

        public void updateScore(int newScore)
        { this.score = newScore; }
    }
}
