namespace Mancala
{

    public class Program
    {
        public static int PlayerAmount = 2;
        public static int Size = 6;
        public static int StoneAmount = 4;

        public static void Main()
        {
            while(true)
            {
                Console.WriteLine("Welcom to the Mancala! Type 1 for Mancala, 2 for Wari, 3 for ManWari, and 4 to quit:\n");
                var input = Console.ReadLine();

                if (input == "1")
                {
                    Mancala mancalaGame = new(PlayerAmount, Size, StoneAmount);
                    mancalaGame.GameFlow();
                }
                
                else if (input == "2")
                {
                    Wari wariGame = new(PlayerAmount, Size, StoneAmount);
                    wariGame.GameFlow();
                }

                else if (input == "3")
                {
                    ManWari manwariGame = new(PlayerAmount, Size, StoneAmount);
                    manwariGame.GameFlow();
                }

                else if (input == "4")
                {
                    break;
                }
            }
        }
    }
}