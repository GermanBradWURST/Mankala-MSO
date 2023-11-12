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

                ChangeSettings();

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

        public static void ChangeSettings()
        {
            Console.WriteLine("How many players?\n");
            var input = Console.ReadLine();
            try
            {
                var i = int.Parse(input);
                PlayerAmount = i;
            }
            catch { }

            Console.WriteLine("How many pits excluding the homepit?\n");
            input = Console.ReadLine();
            try
            {
                var i = int.Parse(input);
                Size = i;
            }
            catch { }

            Console.WriteLine("How many stones in each pit?\n");
            input = Console.ReadLine();
            try
            {
                var i = int.Parse(input);
                StoneAmount = i;
            }
            catch { }
        }
    }
}