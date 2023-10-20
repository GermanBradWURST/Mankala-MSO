namespace Mankala {

    public abstract class Pit 
    {

        public int amount;

        protected Pit nextPit;

        protected Player p;

        public Pit() {}

        public void AddAmount(int add) 
        {

            int a = this.amount;
            this.amount = a + add;

        }


    }

    public class HomePit : Pit 
    {

        public HomePit(int amount, Player p) 
        {
            this.amount = 0;
            this.p = p;

        }
    }

    public class SmallPit : Pit 
    {
        
        SmallPit oppositePit;

        public SmallPit(int amount, Player p) 
        {
            this.amount = amount;
            this.p = p;
            
        }

        public void RemoveAmount(int subtract) 
        {

            int a = this.amount;
            this.amount = a - subtract;
            
        }

        //public SmallPit findOpposite(SmallPit p) {

        //}


    }
}