using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public class Player
    {
        public string Name = "<name>";
        private int Score = 0;

        public Player()
        {
            Console.Write("Player name: ");
            string input = Console.ReadLine();

            if (input != "") { Name = input; }
        }

        public int GetScore()
        { return Score; }

        public void UpdateScore(int newScore)
        { Score = newScore; }

    }
}
