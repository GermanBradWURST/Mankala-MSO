namespace Mankala {

    public abstract class Pit {

        int amount;

        Pit nextPit;

        public abstract Pit(int amount) {}

        public AddAmount(int add) {

            a = this.amount;
            this.amount = a + add;

        }

    }

    public class HomePit : Pit {

        public HomePit(int amount) {
            this.amount = 0;

        }
    }

    public class SmallPit : Pit {
        
        public SmallPit(int amount) {
            this.amount = amount;
            
        }

        public RemoveAmount(int subtract) {

            int a = this.amount;
            this.amount = a - subtract;
            
        }


    }
}