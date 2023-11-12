namespace Mancala
{

    public class Program
    {
        public static int PlayerAmount = 2;
        public static int Size = 6;
        public static int StoneAmount = 4;

        public static void Main()
        {
            Mancala mancalaGame = new Mancala(PlayerAmount, Size, StoneAmount);
            Wari wariGame = new Wari(PlayerAmount, Size, StoneAmount);
            ManWari manwariGame = new ManWari(PlayerAmount, Size, StoneAmount);

            wariGame.GameFlow();
            mancalaGame.GameFlow();
            wariGame.GameFlow();
            manwariGame.GameFlow();
        }
    }
}