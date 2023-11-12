namespace Mancala
{

    public class Board
    {
        public int Size;
        public int StoneAmount;
        public int[,] BoardArray;
        public int PlayerAmount;
        public int TotalStones;

        public Board(int playerAmount, int size, int stoneAmount)
        {
            Size = size;
            StoneAmount = stoneAmount;
            PlayerAmount = playerAmount;
            TotalStones = size * stoneAmount * playerAmount;
            MakeBoard();
        }

        private void MakeBoard()
        {
            BoardArray = new int[(Size + 1), PlayerAmount];

            for (int y = 0; y < PlayerAmount; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    BoardArray[x, y] = StoneAmount;
                }

                BoardArray[Size, y] = 0;
            }
        }

        public void PrintBoard(List<Player> playerList, Player currentPlayer)
        {
            string currentPlayerString = "  | ";

            for (int y = PlayerAmount - 1; y >= 0; y--)
            {
                string smallPit = "";
                bool isCurrentPlayer = playerList[y] == currentPlayer;

                for (int i = 0; i < Size; i++)
                {
                    string divider = "  | ";
                    int displayNumber = BoardArray[i, y];
                    if (displayNumber > 9)
                        divider = " | ";

                    if (isCurrentPlayer)
                    {
                        smallPit = smallPit + $"{displayNumber}{divider}";
                    }
                    else
                    {
                        smallPit = $"{displayNumber}{divider}" + smallPit;
                    }
                }

                string homePit = $"{BoardArray[Size, y]}";

                if (isCurrentPlayer)
                {
                    currentPlayerString += smallPit + homePit;
                }
                else
                {
                    Console.WriteLine(homePit + " | " + smallPit);
                }
            }

            Console.WriteLine(currentPlayerString);
            Console.WriteLine(currentPlayer.Name);
        }

        public bool CheckIfBoardEmpty()
        {
            for (int y = 0; y < PlayerAmount; y++)
            {
                if (!CheckIfRowEmpty(y))
                    return false;
            }

            return true;

        }

        public bool CheckIfRowEmpty(int i)
        {
            for (int x = 0; x < Size; x++)
            {
                if (CheckNotEmpty(x, i))
                    return false;
            }

            return true;
        }

        public bool CheckNotEmpty(int indexPit, int indexPlayer)
        {
            if (BoardArray[indexPit, indexPlayer] != 0)
                return true;

            return false;

        }



        public (int, int) FindOppositeHole((int, int) hole)
        {
            int pitIndex = hole.Item1;
            int playerIndex = hole.Item2;
            int counter = -1;

            while (pitIndex < Size)
            {
                counter++;
                pitIndex++;
            }

            if (playerIndex + 1 == PlayerAmount)
            {
                playerIndex = 0;
            }
            else
            {
                playerIndex++;
            }

            if (counter < 0)
            {
                counter = Size;
            }

            return (counter,  playerIndex);
        }


    }
}
