namespace Mankala {

    public abstract class Pit {

        protected int amount;

        protected Pit nextPit;

        protected Player p;

        public Pit() {}

        public void AddAmount(int add) {

            int a = this.amount;
            this.amount = a + add;

        }


    }

    public class HomePit : Pit {

        public HomePit(int amount) {
            this.amount = 0;

        }
    }

    public class SmallPit : Pit {
        
        SmallPit oppositePit;

        public SmallPit(int amount) {
            this.amount = amount;
            
        }

        public void RemoveAmount(int subtract) {

            int a = this.amount;
            this.amount = a - subtract;
            
        }

        //public SmallPit findOpposite(SmallPit p) {

        //}


    }
}