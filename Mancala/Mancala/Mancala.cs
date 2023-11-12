using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public interface IMakeRuleSet
    {
        public RuleSet NewRuleSet();
    }

    public abstract class MancalaTemplate : IMakeRuleSet
    {
        public Player CurrentPlayer;
        public List<Player> PlayerList = new List<Player>();
        public Board Board;
        public RuleSet RuleSet;
        public int LastChoice;
        public (int, int) LastHole;
        public bool GameEnded = false;

        public MancalaTemplate (int playerAmount, int size, int stoneAmount) //Board creation doesn't differ between game modes so it is made in this constructor (subclass constructors refer to their base)
        {
            Board = new Board(playerAmount, size, stoneAmount);
        }

        public abstract RuleSet NewRuleSet();

        public void GameFlow()
        {
            RuleSetInitialization();
            AddPlayers();
            CurrentPlayer = PlayerList[0];

            GameLoop();

            PrintWinner();
        }

        public void RuleSetInitialization()
        {
            RuleSet = NewRuleSet();
        }

        public void GameLoop()
        {
            while (GameEnded == false)
            {
                HandleGame();
                UpdateScores();
                RuleSet.WinRule.ApplyRule(this);
            }
        }

        public void HandleGame()
        {
            int indexCurrentPlayer = GetIndex(CurrentPlayer);
            if (Board.CheckIfRowEmpty(indexCurrentPlayer) == false)
            {
                Board.PrintBoard(PlayerList, CurrentPlayer);
                LastChoice = TakeInput();
                LastHole = (LastChoice - 1, indexCurrentPlayer);
                RuleSet.Sowing.ApplyRule(this);
                foreach (IRule r in RuleSet.Rules)
                {
                    r.ApplyRule(this);
                }
            }
        }

        public void AddPlayers()
        {
            for (int i = 0; i < Board.PlayerAmount; i++)
            {
                Player p = new Player();
                PlayerList.Add(p);
            }
        }

        public int GetIndex(Player p) // the index of a player in the PlayerList is needed so much that it deserved its own function
        {
            return PlayerList.IndexOf(p);
        }

        public void NextPlayer()
        {
            int index = GetIndex(CurrentPlayer);

            if (index == Board.PlayerAmount - 1)
            {
                CurrentPlayer = PlayerList[0];
            }

            else
            {
                CurrentPlayer = PlayerList[index + 1];
            }
        }

        public void UpdateScores() // even though the Score of each player can be found in the BoardArray, it is also stored in a Player for clarity and accesibility
        {
            foreach (Player p in PlayerList)
            {
                int points = GetPoint(p);
                p.UpdateScore(points);
            }
        }

        private List<Player> GetWinner() // This returns a list because more players could be tied in first (in which case a tie is declared)
        {
            List<Player> playerList = new List<Player>();
            int highestScore = 0;

            foreach (Player p in this.PlayerList)
            {
                int playerScore = p.GetScore();
                if (playerScore > highestScore)
                {
                    playerList.Clear();
                    highestScore = playerScore;
                    playerList.Add(p);
                }

                else if (playerScore == highestScore)
                {
                    playerList.Add(p);
                }

            }

            return playerList;
        }
        private void PrintWinner()
        {
            List<Player> winPlayers = GetWinner();

            if (winPlayers.Count() > 1)
            {
                Console.WriteLine("It's a draw between the following players: ");
                foreach (Player p in winPlayers)
                {
                    Console.WriteLine(p.Name, $"| {GetPoint(p)} points");
                }
            }
            else
            {
                Player p = winPlayers[0];
                Console.WriteLine($"The winner is: {p.Name} with {GetPoint(p)} points");
            }
        }

        public int GetPoint(Player p)
        {
            int index = GetIndex(p);
            return Board.BoardArray[Board.Size, index];

        }


        public int TakeInput() // besides taking input, this function also handles invalid input. The only thing to do after invalid input is to retake input, so it handles everything in one function.
        {
            while (true)
            {
                Console.WriteLine($"From which pit would you like to sow the seeds? (left->right | 1->{Board.Size}): ");
                string input = Console.ReadLine();
                try
                {
                    int pitIndex = int.Parse(input);
                    if (1 <= pitIndex && pitIndex <= Board.Size)
                    {
                        if (Board.CheckNotEmpty(pitIndex - 1, GetIndex(CurrentPlayer)))
                        {
                            return pitIndex;
                        }
                        else
                        {
                            Console.WriteLine("The pit you chose is empty");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The pit you chose is out of bounds");
                    }
                }
                catch { Console.WriteLine("Please enter a whole number"); }
            }

        }
    }


    public class Mancala : MancalaTemplate
    {
        public Mancala(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override RuleSet NewRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new MancalaSowing();
            ruleSet.WinRule = new StdWinRule();


            ruleSet.Rules.Add(new LastInHomePit());
            return ruleSet;
        }


    }


    public class Wari : MancalaTemplate
    {
        public Wari(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override RuleSet NewRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new WariSowing();
            ruleSet.WinRule = new StdWinRule();
            ruleSet.Rules.Add(new Wari2or3());
            ruleSet.Rules.Add(new StdNextPlayer());
            return ruleSet;
        }

    }


    public class ManWari : MancalaTemplate
    {
        public ManWari(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override RuleSet NewRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new WariSowing();
            ruleSet.WinRule = new StdWinRule();
            ruleSet.Rules.Add(new StdNextPlayer());
            return ruleSet;
        }

    }

}