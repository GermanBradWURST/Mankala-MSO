using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{

    public interface RuleSetFactory
    {
        public RuleSet newRuleSet();
    }

    public abstract class MancalaFactory : RuleSetFactory
    {
        public Player currentPlayer;
        public List<Player> playerList = new List<Player>();
        public Board board;
        public RuleSet ruleSet;
        public int lastChoice;
        public (int, int) lastHole;
        public bool gameEnded = false;

        public MancalaFactory (int playerAmount, int size, int stoneAmount) //board creation doesn't differ between game modes so it is made in this constructor (subclass constructors refer to their base)
        {
            board = new Board(playerAmount, size, stoneAmount);
        }

        public abstract RuleSet newRuleSet();

        public void gameFlow()
        {
            ruleSetInitialization();
            addPlayers();
            currentPlayer = playerList[0];

            gameLoop();

            printWinner();
        }

        public void ruleSetInitialization()
        {
            ruleSet = newRuleSet();
        }

        public abstract void gameLoop();
        

        public void addPlayers()
        {
            for (int i = 0; i < board.playerAmount; i++)
            {
                Player p = new Player();
                playerList.Add(p);
            }
        }

        public int getIndex(Player p) // the index of a player in the playerList is needed so much that it deserved its own function
        {
            return playerList.IndexOf(p);
        }

        public void nextPlayer()
        {
            int index = getIndex(currentPlayer);

            if (index == board.playerAmount - 1)
            {
                currentPlayer = playerList[0];
            }

            else
            {
                currentPlayer = playerList[index + 1];
            }
        }

        public void handleGame()
        {
            int indexCurrentPlayer = getIndex(currentPlayer);
            if (board.checkIfRowEmpty(indexCurrentPlayer) == false)
            {
                board.printBoard(playerList, currentPlayer);
                lastChoice = takeInput();
                lastHole = (lastChoice - 1, indexCurrentPlayer);
                ruleSet.Sowing.applyRule(this);
                foreach (Rule r in ruleSet.Rules)
                {
                    r.applyRule(this);
                }
            }
        }

        public void updateScores() // even though the score of each player can be found in the boardArray, it is also stored in a Player for clarity and accesibility
        {
            foreach (Player p in playerList)
            {
                int points = getPoint(p);
                p.updateScore(points);
            }
        }

        private List<Player> getWinner() // This returns a list because more players could be tied in first (in which case a tie is declared)
        {
            List<Player> pL = new List<Player>();
            int baseline = 0;

            foreach (Player p in playerList)
            {
                int pS = p.getScore();
                if (pS > baseline)
                {
                    pL.Clear();
                    baseline = pS;
                    pL.Add(p);
                }

                else if (pS == baseline)
                {
                    pL.Add(p);
                }

            }

            return pL;
        }
        private void printWinner()
        {
            List<Player> winPlayers = getWinner();

            if (winPlayers.Count() > 1)
            {
                Console.WriteLine("It's a draw between the following players: ");
                foreach (Player p in winPlayers)
                {
                    Console.WriteLine(p.name, $"| {getPoint(p)} points");
                }
            }
            else
            {
                Player p = winPlayers[0];
                Console.WriteLine($"The winner is: {p.name} with {getPoint(p)} points");
            }
        }

        public int getPoint(Player p)
        {
            int index = getIndex(p);
            return board.boardArray[board.size, index];

        }


        public int takeInput() // besides taking input, this function also handles invalid input. The only thing to do after invalid input is to retake input, so it handles everything in one function.
        {
            while (true)
            {
                Console.WriteLine($"From which pit would you like to sow the seeds? (left->right | 1->{board.size}): ");
                string input = Console.ReadLine();
                try
                {
                    int pitIndex = int.Parse(input);
                    if (1 <= pitIndex && pitIndex <= board.size)
                    {
                        if (board.checkNotEmpty(pitIndex - 1, getIndex(currentPlayer)))
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


    public class Mancala : MancalaFactory
    {
        public Mancala(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override void gameLoop()
        {
            while (gameEnded == false)
            {
                
                int indexCurrentPlayer = getIndex(currentPlayer);
                if (board.checkIfRowEmpty(indexCurrentPlayer) == false)
                {
                    board.printBoard(playerList, currentPlayer);
                    lastChoice = takeInput();
                    lastHole = (lastChoice - 1, indexCurrentPlayer);
                    ruleSet.Sowing.applyRule(this);
                    foreach (Rule r in  ruleSet.Rules)
                    {
                        r.applyRule(this);
                    }
                }
                
                //handleGame();
                ruleSet.WinRule.applyRule(this);
                updateScores();
            }
        }

        public override RuleSet newRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new MancalaSowing();
            ruleSet.WinRule = new StdWinRule();


            ruleSet.Rules.Add(new LastInHomePit());
            return ruleSet;
        }


    }


    public class Wari : MancalaFactory
    {
        public Wari(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override void gameLoop()
        {
            while (gameEnded == false)
            {
                int indexCurrentPlayer = getIndex(currentPlayer);
                if (board.checkIfRowEmpty(indexCurrentPlayer) == false)
                {
                    board.printBoard(playerList, currentPlayer);
                    lastChoice = takeInput();
                    lastHole = (lastChoice - 1, indexCurrentPlayer);
                    ruleSet.Sowing.applyRule(this);
                    foreach (Rule r in ruleSet.Rules)
                    {
                        r.applyRule(this);
                    }
                }

                updateScores();
                nextPlayer();
                ruleSet.WinRule.applyRule(this);
            }
        }

        public override RuleSet newRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new WariSowing();
            ruleSet.WinRule = new StdWinRule();
            ruleSet.Rules.Add(new Capturing());
            ruleSet.Rules.Add(new Wari2or3());
            return ruleSet;
        }

    }


    public class ManWari : MancalaFactory
    {
        public ManWari(int playerAmount, int size, int stoneAmount) : base(playerAmount, size, stoneAmount) { }

        public override void gameLoop()
        {
            int indexCurrentPlayer = getIndex(currentPlayer);
            while (gameEnded == false)
            {
                if (board.checkIfRowEmpty(indexCurrentPlayer) == false)
                {
                    board.printBoard(playerList, currentPlayer);
                    lastChoice = takeInput();
                    lastHole = (lastChoice - 1, indexCurrentPlayer);
                    ruleSet.Sowing.applyRule(this);
                    foreach (Rule r in ruleSet.Rules)
                    {
                        r.applyRule(this);
                    }
                }

                updateScores();
                nextPlayer();
                ruleSet.WinRule.applyRule(this);
            }
        }

        public override RuleSet newRuleSet()
        {
            RuleSet ruleSet = new RuleSet();
            ruleSet.Sowing = new WariSowing();
            ruleSet.WinRule = new StdWinRule();
            return ruleSet;
        }

    }

}