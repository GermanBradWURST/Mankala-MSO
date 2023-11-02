using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public class Board
    {
        public int size;
        public int stoneAmount;
        public int[,] boardArray;
        public int playerAmount;
        public int totalStones;

        public Board(int playerAmount, int size, int stoneAmount)
        {
            this.size = size;
            this.stoneAmount = stoneAmount;
            this.playerAmount = playerAmount;
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
                    boardArray[x, y] = stoneAmount;
                }

                boardArray[size, y] = 0;
            }
        }

        public void printBoard(List<Player> playerList, Player currentPlayer)
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

                if (playerList[y] == currentPlayer)
                {
                    cpstring += smallPit + " | " + homePit;
                }
                else
                {
                    Console.WriteLine(homePit + " | " + smallPit);
                }
            }

            Console.WriteLine(cpstring);
            Console.WriteLine(currentPlayer.name);
        }

        public bool checkNotEmpty(int indexPit, int indexPlayer)
        {
            //int indexPlayer = getIndex(currentP);
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

        public bool checkIfRowEmpty(int i)
        {
            //int i = getIndex(currentP);

            for (int x = 0; x < size; x++)
            {
                if (boardArray[x, i] != 0)
                    return false;
            }

            return true;
        }


    }
}
