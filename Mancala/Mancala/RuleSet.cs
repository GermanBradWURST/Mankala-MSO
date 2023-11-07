using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class RuleSet
    {
        public Rule WinRule;
        public Rule Sowing;

        public RuleSet() { }

    }

    public abstract class Rule
    {
        public abstract int[,] applyRule(Board board, int indexPlayer, int choice);
    }



    public class MancalaSowing : Rule
    {
        public MancalaSowing() { }

        public override int[,] applyRule(Board board, int indexPlayer, int choice)
        {
            int[,] boardArray = board.boardArray;
            int indexPit = choice - 1;
            int StonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (StonesPickedUp > 0)
            {
                if (indexPit == board.size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == board.playerAmount)
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

            return boardArray;
        }
    }

    public class WariSowing : Rule
    {
        public WariSowing() { }

        public override int[,] applyRule(Board board, int indexPlayer, int choice)
        {
            int[,] boardArray = board.boardArray;
            int indexPit = choice - 1;
            int StonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (StonesPickedUp > 0)
            {
                if (indexPit + 1 == board.size)
                {
                    indexPit = 0;

                    if (indexPlayer + 1 == board.playerAmount)
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

            return boardArray;
        }
    }

}
