namespace Mankala {

    public class Player {

        public string name;
        public int score;

        public Player(string name) {

            this.name = name;
            this.score = 0;

        }

        public int getScore(Player p) {

            return p.score;

        }

        public void increaseScore(Player p, int i) {

            p.score += i;
            
        }
    }
}