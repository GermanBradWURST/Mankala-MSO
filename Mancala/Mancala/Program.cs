namespace Mancala
{

    public class Program
    {
        public static int playerAmount = 2;
        public static int size = 6;
        public static int stoneAmount = 4;

        public static void Main()
        {
            Mancala mancalaGame = new Mancala(playerAmount, size, stoneAmount);
            Wari wariGame = new Wari(playerAmount, size, stoneAmount);
            ManWari manwariGame = new ManWari(playerAmount, size, stoneAmount);

            mancalaGame.gameFlow();
            wariGame.gameFlow();
            manwariGame.gameFlow();
        }
    }
}