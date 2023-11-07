using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class RuleSet
    {
        public Rule Sowing;
        public Rule WinRule;
        public List<Rule> Rules = new List<Rule>();

        public RuleSet() { }

    }

    public abstract class Rule
    {
        public abstract void applyRule(MancalaFactory game);
    }



    public class MancalaSowing : Rule
    {
        public MancalaSowing() { }

        public override void applyRule(MancalaFactory game)
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

        public override void applyRule(MancalaFactory game)
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
        public override void applyRule(MancalaFactory game)
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
        public override void applyRule(MancalaFactory game)
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
        public override void applyRule(MancalaFactory game)
        {
            int index = game.getIndex(game.currentPlayer);
            if (game.board.checkIfRowEmpty(index))
            {
                game.GameEnded = true;
            }
        }
    }

}
