namespace Mancala
{

    public class Program
    {
        public static int playerAmount = 2;
        public static int size = 6;
        public static int stoneAmount = 4;

        public static void Main()
        {
            Mancala game = new Mancala(playerAmount, size, stoneAmount);
        }
    }
}