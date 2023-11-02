using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public interface BoardFactory
    {
        Board createBoard(int pA, int s, int sA);
    }


    public class Mancala : BoardFactory
    {
        protected Board board;
        protected RuleSet ruleset;

        public Mancala(int pA, int s, int sA)
        {
            board = createBoard(pA, s, sA);
            board.printBoard();

            gameLoop();

            wait();
        }

        public void gameLoop()
        {
            while (gameEnd() == false)
            {
                if (board.checkIfRowEmpty() == false)
                {
                    board.printBoard();
                    int choice = takeInput();
                    board.sowing(choice);
                }
                
                board.nextPlayer();
            }

            printWinner();

        }

        public int takeInput()
        {
            while (true)
            {
                Console.WriteLine($"From which pit would you like to sow the seeds? (left->right | 1->{board.size}): ");
                string input = Console.ReadLine();
                try
                {
                    int pitIndex = int.Parse(input);
                    if (1 <= pitIndex && pitIndex <= board.size)
                    {
                        if (board.checkNotEmpty(pitIndex-1))
                        {
                            return pitIndex;
                        }
                        else
                        {
                            Console.WriteLine("The pit you chose is empty");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The pit you chose is out of bounds");
                    }
                }
                catch { Console.WriteLine("Please enter a whole number"); }
            }

        }

        public Board createBoard(int pA, int s, int sA)
        {
            List<Player> playerList = addPlayers(pA);
            return new Board(playerList, s, sA);
        }

        private List<Player> addPlayers(int pA)
        {
            List<Player> playerList = new List<Player>();
            for (int i = 0; i < pA; i++)
            {
                Player p = new Player();
                playerList.Add(p);
            }
            return playerList;
        }

        private void wait()
        {
            Console.ReadLine();
        }

        private bool gameEnd()
        {
            return board.checkIfBoardEmpty();
        }

        private List<Player> getWinner()
        {
            List<Player> pL = new List<Player>();
            List<int> points = board.getPoints();
            List<int> indexes = new List<int>();
            int baseline = points.Max();
            int index = 0;

            foreach (int i in points)
            {
                if (i == baseline)
                    indexes.Add(index);

                index++;
            }

            foreach (int i in indexes)
            {
                pL.Add(board.playerList[i]);
            }

            return pL;
        }

        private void printWinner()
        {
            List<Player> winPlayers = getWinner();

            if (winPlayers.Count() > 1)
            {
                Console.WriteLine("It's a draw between the following players: ");
                foreach (Player p in winPlayers)
                {
                    Console.WriteLine(p.name, $"| {board.getPoint(p)} points");
                }
            }
            else
            {
                Player p = winPlayers[0];
                Console.WriteLine($"The winner is: {p.name} with {board.getPoint(p)} points");
            }
        }

    }
}