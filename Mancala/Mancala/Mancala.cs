using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    
    public abstract class MancalaFactory
    {
        public Player currentPlayer;
        public List<Player> playerList = new List<Player>();
        public Board board;

        public void gameFlow()
        {
            addPlayers();
            currentPlayer = playerList[0];
            board.printBoard(playerList, currentPlayer);

            gameLoop();

            printWinner();
        }

        public abstract void gameLoop();

        public void addPlayers()
        {
            for (int i = 0; i < board.playerAmount; i++)
            {
                Player p = new Player();
                playerList.Add(p);
            }
        }

        public int getIndex(Player p)
        {
            return playerList.IndexOf(p);
        }

        public void nextPlayer()
        {
            int index = getIndex(currentPlayer);

            if (index == board.playerAmount - 1)
            {
                currentPlayer = playerList[0];
            }

            else
            {
                currentPlayer = playerList[index + 1];
            }
        }

        public void updateScores()
        {
            foreach (Player p in playerList)
            {
                int points = getPoint(p);
                p.updateScore(points);
            }
        }

        private List<Player> getWinner()
        {
            List<Player> pL = new List<Player>();
            int baseline = 0;

            foreach (Player p in playerList)
            {
                int pS = p.getScore();
                if (pS > baseline)
                {
                    pL.Clear();
                    baseline = pS;
                    pL.Add(p);
                }

                else if (pS == baseline)
                {
                    pL.Add(p);
                }

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
                    Console.WriteLine(p.name, $"| {getPoint(p)} points");
                }
            }
            else
            {
                Player p = winPlayers[0];
                Console.WriteLine($"The winner is: {p.name} with {getPoint(p)} points");
            }
        }

        public int getPoint(Player p)
        {
            int index = getIndex(p);
            return board.boardArray[board.size, index];

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
                        if (board.checkNotEmpty(pitIndex - 1, getIndex(currentPlayer)))
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


    }


    public class Mancala : MancalaFactory
    {
        public Mancala(int playerAmount, int size, int stoneAmount)
        {
            board = new Board(playerAmount, size, stoneAmount);
        }

        public override void gameLoop()
        {
            while (board.checkIfBoardEmpty() == false)
            {
                if (board.checkIfRowEmpty(getIndex(currentPlayer)) == false)
                {
                    board.printBoard(playerList, currentPlayer);
                    int choice = takeInput();
                    sowing(choice);
                }

                updateScores();
                nextPlayer();
            }
        }

        public void sowing(int i)
        {
            int [,] boardArray = board.boardArray;
            int indexPlayer = getIndex(currentPlayer);
            int indexPit = i - 1;
            int amount = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (amount > 0)
            {
                if (indexPit == board.size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == board.playerAmount)
                    {
                        indexPlayer = 0;
                    }
                    else
                    {
                        indexPlayer++;
                    }
                }

                else
                {
                    indexPit++;
                }

                amount--;
                boardArray[indexPit, indexPlayer]++;

            }

            board.boardArray = boardArray;
        }
    }


    public class Wari : MancalaFactory
    {
        public Wari(int playerAmount, int size, int stoneAmount)
        {
            board = new Board(playerAmount, size, stoneAmount);
        }

        public override void gameLoop()
        {
            
        }

    }


    public class ManWari : MancalaFactory
    {
        public ManWari(int playerAmount, int size, int stoneAmount)
        {
            board = new Board(playerAmount, size, stoneAmount);
        }

        public override void gameLoop()
        {
            
        }

    }

}