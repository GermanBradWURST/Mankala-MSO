using System.Diagnostics.Metrics;
using Mancala;

namespace MancalaTest
{
    [TestClass]
    public class MancalaTest
    {
        [TestMethod]
        public void TestCheckNotEmpty()
        {
            int playerAmount = 2;
            int size = 6;
            int stoneAmount = 4;

            Board board = new(playerAmount, size, stoneAmount);
            board.BoardArray[1, 1] = 0;
            bool first = board.CheckNotEmpty(0,1);
            bool second = board.CheckNotEmpty(1, 1);

            Assert.AreNotEqual(first, second);

        }

        [TestMethod]
        public void TestCheckIfRowEmpty()
        {
            int playerAmount = 2;
            int size = 6;
            int stoneAmount1 = 4;
            int stoneAmount2 = 0;

            Board board1 = new(playerAmount, size, stoneAmount1);
            Board board2 = new(playerAmount, size, stoneAmount2);
            bool first = board1.CheckIfRowEmpty(0);
            bool second = board2.CheckIfRowEmpty(0);

            Assert.AreNotEqual(first, second);
        }


        [TestMethod]
        public void TestCheckIfBoardEmpty()
        {
            int playerAmount = 2;
            int size = 6;
            int stoneAmount1 = 4;
            int stoneAmount2 = 0;

            Board board1 = new(playerAmount, size, stoneAmount1);
            Board board2 = new(playerAmount, size, stoneAmount2);
            bool first = board1.CheckIfBoardEmpty();
            bool second = board2.CheckIfBoardEmpty();

            Assert.AreNotEqual(first, second);

        }

        [TestMethod]
        public void TestMancalaNextIndex()
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Mancala.Mancala mancala = new(playerAmount, size, stoneAmount);

            playerAmount = 2;

            mancala.Board = new(playerAmount, size, stoneAmount);

            mancala.Board.BoardArray[2, 0] = 2;
            mancala.Board.BoardArray[3, 0] = 3;
            int first = mancala.Board.BoardArray[2, 0];

            (int, int) nextIndex = MancalaSowing.NextIndex(mancala, 2, 0);
            int second = mancala.Board.BoardArray[nextIndex.Item1,  nextIndex.Item2];


            Assert.AreEqual(first, second - 1);

        }

        [TestMethod]
        public void TestMancalaNextIndexBorder()
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Mancala.Mancala mancala = new(playerAmount, size, stoneAmount);

            playerAmount = 2;

            mancala.Board = new(playerAmount, size, stoneAmount);

            mancala.Board.BoardArray[size, 0] = 2;
            mancala.Board.BoardArray[0, 1] = 3;
            int first = mancala.Board.BoardArray[size, 0];

            (int, int) nextIndex = MancalaSowing.NextIndex(mancala, size, 0);
            int second = mancala.Board.BoardArray[nextIndex.Item1, nextIndex.Item2];


            Assert.AreEqual(first, second - 1);


        }

        [TestMethod]
        public void TestWariNextIndex()
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Wari wari = new(playerAmount, size, stoneAmount);

            playerAmount = 2;

            wari.Board = new(playerAmount, size, stoneAmount);

            wari.Board.BoardArray[2, 0] = 2;
            wari.Board.BoardArray[3, 0] = 3;
            int first = wari.Board.BoardArray[2, 0];

            (int, int) nextIndex = MancalaSowing.NextIndex(wari, 2, 0);
            int second = wari.Board.BoardArray[nextIndex.Item1, nextIndex.Item2];


            Assert.AreEqual(first, second - 1);

        }

        [TestMethod]
        public void TestWariNextIndexBorder()
        {
            int playerAmount = 0;
            int size = 6 - 1;// the (-1) is there because the WariSowing skips the homepit
            int stoneAmount = 4;

            Wari wari = new(playerAmount, size, stoneAmount);

            playerAmount = 2;

            wari.Board = new(playerAmount, size, stoneAmount);

            wari.Board.BoardArray[size, 0] = 2;
            wari.Board.BoardArray[0, 1] = 3;
            int first = wari.Board.BoardArray[size, 0];

            (int, int) nextIndex = MancalaSowing.NextIndex(wari, size, 0);
            int second = wari.Board.BoardArray[nextIndex.Item1, nextIndex.Item2];


            Assert.AreEqual(first, second - 1);

        }

        [TestMethod]
        public void TestOppositeHole()
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Wari wari = new(playerAmount,size, stoneAmount);

            playerAmount = 2;

            wari.Board = new(playerAmount, size, stoneAmount);

            for (int y = 0; y < playerAmount; y++)
            {
                for (int x = 0; x <= size; x++)
                {
                    (int, int) result = wari.Board.FindOppositeHole((x, y));

                    int first;
                    if (x == size)
                        first = size;
                    else
                        first = (size - 1) - x;

                    int second;
                    if (y == 0)
                        second = 1;
                    else
                        second = 0;

                    Assert.AreEqual((first, second), result);
                }
            }
        }

        [TestMethod]
        public void TestRuleSetMancala() // property based testing
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Mancala.Mancala mancala = new(playerAmount, size, stoneAmount);

            mancala.RuleSetInitialization();

            IRule sowing = new MancalaSowing();
            IRule winRule = new StdWinRule();
            IRule capturing = new Capturing();
            IRule lastInHomePit = new LastInHomePit();

            // usually you shouldn't put multiple Assert statements in a test method.
            // However, seeing as we are testing a whole RuleSet, we want to test every part of it
            Assert.AreSame(sowing.GetType(), mancala.RuleSet.Sowing.GetType());
            Assert.AreSame(winRule.GetType(), mancala.RuleSet.WinRule.GetType());
            Assert.AreSame(capturing.GetType(), mancala.RuleSet.Rules[0].GetType());
            Assert.AreSame(lastInHomePit.GetType(), mancala.RuleSet.Rules[1].GetType());

        }

        [TestMethod]
        public void TestRuleSetWari() // property based testing
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            Wari wari = new(playerAmount, size, stoneAmount);

            wari.RuleSetInitialization();

            IRule sowing = new WariSowing();
            IRule winRule = new StdWinRule();
            IRule wari2Or3 = new Wari2Or3();
            IRule stdNextPlayer = new StdNextPlayer();

            // usually you shouldn't put multiple Assert statements in a test method.
            // However, seeing as we are testing a whole RuleSet, we want to test every part of it
            Assert.AreSame(sowing.GetType(), wari.RuleSet.Sowing.GetType());
            Assert.AreSame(winRule.GetType(), wari.RuleSet.WinRule.GetType());
            Assert.AreSame(wari2Or3.GetType(), wari.RuleSet.Rules[0].GetType());
            Assert.AreSame(stdNextPlayer.GetType(), wari.RuleSet.Rules[1].GetType());

        }

        [TestMethod]
        public void TestRuleSetManWari() // property based testing
        {
            int playerAmount = 0;
            int size = 6;
            int stoneAmount = 4;

            ManWari manWari = new(playerAmount, size, stoneAmount);

            manWari.RuleSetInitialization();

            IRule sowing = new MancalaSowing();
            IRule winRule = new StdWinRule();
            IRule wari2Or3 = new Wari2Or3();
            IRule capturing = new Capturing();
            IRule stdNextPlayer = new StdNextPlayer();

            // usually you shouldn't put multiple Assert statements in a test method.
            // However, seeing as we are testing a whole RuleSet, we want to test every part of it
            Assert.AreSame(sowing.GetType(), manWari.RuleSet.Sowing.GetType());
            Assert.AreSame(winRule.GetType(), manWari.RuleSet.WinRule.GetType());
            Assert.AreSame(wari2Or3.GetType(), manWari.RuleSet.Rules[0].GetType());
            Assert.AreSame(capturing.GetType(), manWari.RuleSet.Rules[1].GetType());
            Assert.AreSame(stdNextPlayer.GetType(), manWari.RuleSet.Rules[2].GetType());

        }
    }
}