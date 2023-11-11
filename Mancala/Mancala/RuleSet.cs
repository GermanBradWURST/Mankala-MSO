using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class RuleSet //The game always has a sowing and winning rule, therefore they are given a variable
    {
        public Rule Sowing;
        public Rule WinRule;
        public List<Rule> Rules = new List<Rule>(); //Other rules are put in a list, when creating a RuleSet, the order in which you add Rules to this list matters

        public RuleSet() { }

    }

    public interface Rule
    {
        public void applyRule(MancalaTemplate game);
    }


    public class MancalaSowing : Rule
    {
        public MancalaSowing() { }

        public void applyRule(MancalaTemplate game)
        {
            int[,] boardArray = game.board.boardArray;
            int indexPit = game.lastHole.Item1;
            int indexPlayer = game.lastHole.Item2;
            int StonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (StonesPickedUp > 0)
            {
                if (indexPit == game.board.size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == game.board.playerAmount)
                    {
                        indexPlayer = 0;
                    }
                    else
                    {
                        indexPlayer++;
                    }
                }

                else
                {
                    indexPit++;
                }

                StonesPickedUp--;
                boardArray[indexPit, indexPlayer]++;
            }

            game.lastHole = (indexPit, indexPlayer);
            game.board.boardArray = boardArray;

            int stonesInHole = boardArray[indexPit, indexPlayer];

            if (indexPit != game.board.size && stonesInHole > 1)
            {
                applyRule(game);
            }
        }
    }

    public class WariSowing : Rule
    {
        public WariSowing() { }

        public void applyRule(MancalaTemplate game)
        {
            int[,] boardArray = game.board.boardArray;
            int indexPit = game.lastHole.Item1;
            int indexPlayer = game.lastHole.Item2;
            int StonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (StonesPickedUp > 0)
            {
                if (indexPit + 1 == game.board.size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == game.board.playerAmount)
                    {
                        indexPlayer = 0;
                    }
                    else
                    {
                        indexPlayer++;
                    }
                }

                else
                {
                    indexPit++;
                }

                StonesPickedUp--;
                boardArray[indexPit, indexPlayer]++;

            }

            game.board.boardArray = boardArray;
            game.lastHole = (indexPit, indexPlayer);

        }
    }

    public class Wari2or3 : Rule
    {
        public Wari2or3() { }
        public void applyRule(MancalaTemplate game)
        {
            int pitIndex = game.lastHole.Item1;
            int playerIndex = game.lastHole.Item2;
            int stonesInHole = game.board.boardArray[pitIndex, playerIndex];

            if (stonesInHole == 2 || stonesInHole == 3)
            {
                game.board.boardArray[pitIndex, playerIndex] = 0;
                game.board.boardArray[game.board.size, game.getIndex(game.currentPlayer)] += stonesInHole;
            }
        }
    }

    public class LastInHomePit : Rule
    {
        public LastInHomePit() { }
        public void applyRule(MancalaTemplate game)
        {
            int pitIndex = game.lastHole.Item1;
            int playerIndex = game.lastHole.Item2;
            int index = game.getIndex(game.currentPlayer);

            if (pitIndex == game.board.size && playerIndex == index && !game.board.checkIfRowEmpty(index))
            {

            }
            else
            {
                game.nextPlayer();
            }
        }

    }

    public class StdWinRule : Rule
    {
        public StdWinRule() { }
        public void applyRule(MancalaTemplate game)
        {
            int index = game.getIndex(game.currentPlayer);
            if (game.board.checkIfRowEmpty(index))
            {
                game.gameEnded = true;
            }
        }
    }

    public class Capturing : Rule
    {
        public Capturing() { }

        public void applyRule(MancalaTemplate game)
        {
            int currentPlayerIndex = game.getIndex(game.currentPlayer);

            int playerIndex = game.lastHole.Item2;
            int pitIndex = game.lastHole.Item1;

            (int, int) oppositeHole = game.board.findOppositeHole(game.lastHole);
            int oppositePit = oppositeHole.Item1;
            int oppositePlayer = oppositeHole.Item2;

            bool isNotEmpty = game.board.checkNotEmpty(oppositePit, oppositePlayer);

            if (currentPlayerIndex == playerIndex && isNotEmpty)
            {
                game.board.boardArray[game.board.size, currentPlayerIndex] += game.board.boardArray[oppositePit, oppositePlayer];
                game.board.boardArray[oppositePit, oppositePlayer] = 0;
            }
        }
    }

    public class StdNextPlayer : Rule
    {
        public StdNextPlayer() { }

        public void applyRule(MancalaTemplate game)
        {
            game.nextPlayer();
        }
    }
}
