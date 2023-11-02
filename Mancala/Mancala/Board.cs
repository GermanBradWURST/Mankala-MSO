using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public class Board
    {
        public List<Player> playerList;
        public int size;
        public int stoneAmount;
        public int[,] boardArray;
        public int playerAmount;
        public int totalStones;
        public Player currentP;

        public Board(List<Player> pl, int s, int sA)
        {
            this.playerList = pl;
            this.size = s;
            this.stoneAmount = sA;
            this.playerAmount = playerList.Count();
            this.currentP = playerList[0];
            this.totalStones = size * stoneAmount * playerAmount;
            makeBoard();

        }

        private void makeBoard()
        {
            boardArray = new int[(size + 1), playerAmount];


            for (int y = 0; y < playerAmount; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    boardArray[x, y] = 4;
                }

                boardArray[size, y] = 0;
            }

        }

        public void printBoard()
        {
            string cpstring = "    ";

            for (int y = 0; y < playerAmount; y++)
            {
                string smallPit = "";

                for (int x = 0; x < size; x++)
                {
                    smallPit = smallPit + $"{boardArray[x, y]}";
                }

                string homePit = $"{boardArray[size, y]}";

                if (playerList[y] == currentP)
                {
                    cpstring += smallPit + " | " + homePit;
                }
                else
                {
                    Console.WriteLine(homePit + " | " + smallPit);
                }
            }

            Console.WriteLine(cpstring);
            Console.WriteLine(currentP.name);
        }

        public int getIndex(Player p)
        {
            return playerList.IndexOf(p);
        }

        public void sowing(int i)
        {
            int indexPlayer = getIndex(currentP);
            int indexPit = i - 1;
            int amount = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (amount > 0)
            {
                if (indexPit == size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == playerAmount)
                    {
                        indexPlayer = 0;
                    }
                    else
                    {
                        indexPlayer ++;
                    }
                }

                else
                {
                    indexPit++;
                }

                amount--;
                boardArray[indexPit, indexPlayer]++;

            }
        }

        public void nextPlayer()
        {
            int index = getIndex(currentP);

            if (index == playerAmount - 1)
            {
                currentP = playerList[0];
            }

            else
            {
                currentP = playerList[index + 1];
            }
        }

        public int getPoint(Player p)
        {
            int index = getIndex(p);
            return boardArray[size, index];

        }

        public List<int> getPoints()
        {
            List<int> points = new List<int>();

            foreach (Player p in playerList)
            {
                int index = getIndex(p);
                points.Add(boardArray[size, index]);
            }
            return points;
        }

        public void printPoints()
        {
            List<int> points = getPoints();
            for (int x = 0; x < points.Count(); x++)
            {
                Player p = playerList[x];
                Console.WriteLine($"{p.name} : {points[x]}");
            }

        }

        public bool checkNotEmpty(int i)
        {
            int indexPit = i;
            int indexPlayer = getIndex(currentP);
            if (boardArray[indexPit, indexPlayer] != 0)
                return true;
            return false;
        }

        public bool checkIfBoardEmpty()
        {
            for (int y = 0; y < playerAmount; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (boardArray[x, y] != 0)
                        return false;
                }
            }

            return true;

        }

        public bool checkIfRowEmpty()
        {
            int i = getIndex(currentP);

            for (int x = 0; x < size; x++)
            {
                if (boardArray[x, i] != 0)
                    return false;
            }

            return true;
        }
    }
}
