namespace Mankala {

    public class Board 
    {

        public HomePit head;
        public RuleSet rules;
        private int pitAmount;
        private int playerAmount;
        private int seedAmount;


        public Board(int pitAmount, int playerAmount, int seedAmount, RuleSet rules) 
        {

            this.rules = rules;
            this.pitAmount = pitAmount;
            this.playerAmount = playerAmount;
            this.seedAmount = seedAmount;

            //MakeList();
        }

        public void drawBoard(List<Player> playerList, Player currentPlayer)
        {   
            Pit currentPit = head;
            string currentPlayerString = "";

            foreach (Player pl in playerList)
            {   
                string s = "|";
                string homebase = "";
                string pitstring = "";
                                           
                for(int j = 0; j < pitAmount+1; j++)
                {
                    if(j == 0)
                        homebase = currentPit.amount.ToString();

                    else if(j < pitAmount+1)
                        pitstring = pitstring + "-" + currentPit.amount.ToString();

                }
                int currentamount = currentPit.amount;
                
                if (pl == currentPlayer) 
                {
                    currentPlayerString = pitstring + "- | "+ homebase + " | : " + pl.name; 
                }

                else
                {
                Console.WriteLine("|", homebase, "|", pitstring + "- :", pl.name);
                }
            }
            Console.WriteLine(currentPlayerString);
        }


    }
}