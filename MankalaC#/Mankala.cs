namespace Mankala {

    public interface IBoardFactory 
    {
        void newBoard(int i, RuleSet r);
    }
    

    public class Mankala : IBoardFactory 
    {

        public Player currentPlayer;
        public List<Player> playerList;
        public Board board;

        int playerAmount;
        public RuleSet rules;

        public Mankala() {}
        
        public void startGame()
        {
            currentPlayer = playerList[0];
            newBoard(4, getRuleSet());
            
        }

        public void doMove()
        {
            
        }

        public void newBoard(int i, RuleSet r)
        {   
            playerAmount = playerList.Count;
            int pitAmount = i;
            int seedAmount = 4;

            CircularLinkedList<Pit> bord = new CircularLinkedList<Pit>();
            foreach(Player p in playerList)
            {
                for(int j = 0; j < i+1; j++)
                {
                    if(j == 0)
                        bord.AddLast(new HomePit(0, p));
                    else if(j < i+1)
                        bord.AddLast(new SmallPit(seedAmount, p));
                }
            }

            board = new Board(pitAmount, playerAmount, seedAmount, r);
        }


        public void newPlayer(string s) 
        {

            Player p = new Player(s);
            playerList.Add(p);

        }

        public RuleSet getRuleSet() 
        {
            rules = new ConcreteRules();
            return rules;
        }

        public bool validMove() 
        {
            rules.checkValidity();
            return true;
        }

        

        public void nextPlayer() 
        {
            
            int index = playerList.IndexOf(currentPlayer);

            if (index == playerAmount - 1) 
            {
                currentPlayer = playerList[0];
            }

            else 
            {
                currentPlayer = playerList[index + 1];
            }
        }

    }
}

