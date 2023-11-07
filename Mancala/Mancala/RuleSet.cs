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
            int indexPit = game.lastChoice - 1;
            int indexPlayer = game.getIndex(game.currentPlayer);
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


            game.board.boardArray = boardArray;
            game.lastHole = (indexPit, indexPlayer);
        }
    }

    public class WariSowing : Rule
    {
        public WariSowing() { }

        public override void applyRule(MancalaFactory game)
        {
            int[,] boardArray = game.board.boardArray;
            int indexPit = game.lastChoice - 1;
            int indexPlayer = game.getIndex(game.currentPlayer);
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

    public class LastInHomePit : Rule
    {
        public override void applyRule(MancalaFactory game)
        {
            int i1 = game.lastHole.Item1;
            int i2 = game.lastHole.Item2;

            if (i1 == game.board.size && i2 == game.getIndex(game.currentPlayer))
            {

            }
            else
            {
                game.nextPlayer();
            }
        }

    }

}
