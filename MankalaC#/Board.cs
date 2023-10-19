namespace Mankala {

    public class Board {

        public HomePit head;
        public RuleSet rules;
        private int pitAmount;
        private int playerAmount;
        private int seedAmount;


        public Board(int pitAmount, int playerAmount, int seedAmount, RuleSet rules) {

            this.rules = rules;
            this.pitAmount = pitAmount;
            this.playerAmount = playerAmount;
            this.seedAmount = seedAmount;

            MakeList();
        }


        private void MakeList() {

            int am = 10;
            head = new HomePit(am);


        }

    }
}