namespace Mankala {

    public abstract class RuleSet {


        public List<Rules> extraRules;
        
        public RuleSet() {}

        public abstract bool checkValidity();

        public List<Rules> makeRuleSet() {
            List<Rules> rulelist = new List<Rules>();
            Console.WriteLine("Do you wish to add Capturing to the ruleset?: Y/N");
            string cansw = Console.ReadLine();
            if (cansw == "Y") {
                rulelist.Add(new DecoratorCapturing());
            }
            Console.WriteLine("Do you wish to add RelaySowing to the ruleset?: Y/N");
            string rsansw = Console.ReadLine();
            if (rsansw == "Y") {
                rulelist.Add(new DecoratorRelaySowing());
            }
            
            return rulelist;
        }
    }


    public class ConcreteRules : RuleSet {

        public ConcreteRules() {
            extraRules = makeRuleSet();
        }

        public override bool checkValidity()
        {
            return true;
        }
    }

    public abstract class Rules : RuleSet {



    }

    public class DecoratorCapturing : Rules {

        public DecoratorCapturing() {}

        public override bool checkValidity() {
            return true;
        }

    }

    public class DecoratorRelaySowing : Rules {

        public DecoratorRelaySowing() {}

        public override bool checkValidity() {
            return true;
        }

    }
}