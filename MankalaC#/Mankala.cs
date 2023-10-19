namespace Mankala {

    public interface IBoardFactory {
        void newBoard(int i, RuleSet r);
    }
    

    public class Mankala : IBoardFactory {

        Player currentPlayer;
        List<Player> playerList;
        Board board;

        int playerAmount;
        //RuleSet rules;

        public void newBoard(int i, RuleSet r)
        {   
            playerAmount = playerList.Count;
            int pitAmount = i;
            int seedAmount = 4 * i;

            board = new Board(pitAmount, playerAmount, seedAmount, r);
        }


        public void newPlayer(string s) {

            Player p = new Player(s);
            playerList.Add(p);

        }


        


        public void nextPlayer() {
            
            int index = playerList.IndexOf(currentPlayer);

            if (index == playerAmount - 1) {
                currentPlayer = playerList[0];
            }

            else {
                currentPlayer = playerList[index + 1];
            }
        }

    }
}
