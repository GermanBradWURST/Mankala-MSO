namespace Mancala
{
    public class RuleSet //The game always has a sowing and winning rule, therefore they are given a variable
    {
        public IRule Sowing;
        public IRule WinRule;
        public List<IRule> Rules = new(); //Other rules are put in a list, when creating a RuleSet, the order in which you add Rules to this list matters

    }

    public interface IRule
    {
        public void ApplyRule(MancalaTemplate game);
    }


    public class MancalaSowing : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int[,] boardArray = game.Board.BoardArray;
            int indexPit = game.LastHole.Item1;
            int indexPlayer = game.LastHole.Item2;
            int stonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (stonesPickedUp > 0)
            {
                (int, int) result = NextIndex(game, indexPit, indexPlayer);
                indexPit = result.Item1;
                indexPlayer = result.Item2;
                
                stonesPickedUp--;
                boardArray[indexPit, indexPlayer]++;
            }


            game.LastHole = (indexPit, indexPlayer);
            game.Board.BoardArray = boardArray;

            int stonesInHole = boardArray[indexPit, indexPlayer];

            if (indexPit != game.Board.Size && stonesInHole > 1)
            {
                ApplyRule(game);
            }
        }

        public static (int, int) NextIndex(MancalaTemplate game, int currentPitIndex, int currentPlayerIndex)
        {
            if (currentPitIndex == game.Board.Size)
            {
                currentPitIndex = 0;

                if (currentPlayerIndex + 1 == game.Board.PlayerAmount)
                {
                    currentPlayerIndex = 0;
                }
                else
                {
                    currentPlayerIndex++;
                }
            }

            else
            {
                currentPitIndex++;
            }

            return (currentPitIndex, currentPlayerIndex);
        }
    }

    public class WariSowing : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int[,] boardArray = game.Board.BoardArray;
            int indexPit = game.LastHole.Item1;
            int indexPlayer = game.LastHole.Item2;
            int stonesPickedUp = boardArray[indexPit, indexPlayer];
            boardArray[indexPit, indexPlayer] = 0;

            while (stonesPickedUp > 0)
            {
                (int, int) result = NextIndex(game, indexPit, indexPlayer);
                indexPit = result.Item1;
                indexPlayer = result.Item2;

                stonesPickedUp--;
                boardArray[indexPit, indexPlayer]++;

            }

            game.Board.BoardArray = boardArray;
            game.LastHole = (indexPit, indexPlayer);

        }

        public static (int, int) NextIndex(MancalaTemplate game, int currentPitIndex, int currentPlayerIndex)
        {
            if (currentPitIndex + 1 == game.Board.Size)
            {
                currentPitIndex = 0;

                if (currentPlayerIndex + 1 == game.Board.PlayerAmount)
                {
                    currentPlayerIndex = 0;
                }
                else
                {
                    currentPlayerIndex++;
                }
            }

            else
            {
                currentPitIndex++;
            }

            return (currentPitIndex, currentPlayerIndex);
        }
    }

    public class Wari2Or3 : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int pitIndex = game.LastHole.Item1;
            int playerIndex = game.LastHole.Item2;
            int stonesInHole = game.Board.BoardArray[pitIndex, playerIndex];

            if (stonesInHole == 2 || stonesInHole == 3)
            {
                game.Board.BoardArray[pitIndex, playerIndex] = 0;
                game.Board.BoardArray[game.Board.Size, game.GetIndex(game.CurrentPlayer)] += stonesInHole;
            }
        }
    }

    public class LastInHomePit : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int pitIndex = game.LastHole.Item1;
            int playerIndex = game.LastHole.Item2;
            int index = game.GetIndex(game.CurrentPlayer);

            if (pitIndex == game.Board.Size && playerIndex == index && !game.Board.CheckIfRowEmpty(index))
            {

            }
            else
            {
                game.NextPlayer();
            }
        }

    }

    public class StdWinRule : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int index = game.GetIndex(game.CurrentPlayer);
            if (game.Board.CheckIfRowEmpty(index))
            {
                game.GameEnded = true;
            }
        }
    }

    public class Capturing : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            int currentPlayerIndex = game.GetIndex(game.CurrentPlayer);

            int playerIndex = game.LastHole.Item2;

            (int, int) oppositeHole = game.Board.FindOppositeHole(game.LastHole);
            int oppositePit = oppositeHole.Item1;
            int oppositePlayer = oppositeHole.Item2;

            bool isNotEmpty = game.Board.CheckNotEmpty(oppositePit, oppositePlayer);

            if (currentPlayerIndex == playerIndex && isNotEmpty)
            {
                game.Board.BoardArray[game.Board.Size, currentPlayerIndex] += game.Board.BoardArray[oppositePit, oppositePlayer];
                game.Board.BoardArray[oppositePit, oppositePlayer] = 0;
            }
        }
    }

    public class StdNextPlayer : IRule
    {
        public void ApplyRule(MancalaTemplate game)
        {
            game.NextPlayer();
        }
    }
}
