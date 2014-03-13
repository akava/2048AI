using System;
using System.Linq;
using AI2048.Game;
using NUnit.Framework;

namespace AI2048.Tests
{
    [TestFixture]
    public class GameLogicTests
    {
        [Test]
        public void SimpleUpTest()
        {
            Test(@"
0 0 0 0 
0 0 0 0 
0 2 0 0 
0 0 0 0 

Up
0 2 0 0 
0 0 0 0 
0 0 0 0 
0 0 0 0 ");
        }

        [Test]
        public void SimpleUpMergeTest()
        {
            Test(@"
0 0 0 0 
0 2 0 0 
0 2 0 0 
0 0 0 0 

Up
0 4 0 0 
0 0 0 0 
0 0 0 0 
0 0 0 0 ");
        }

        [Test]
        public void ComplexUpTest()
        {
            Test(@"
0 0 0 0 
0 0 0 2 
0 2 0 2 
0 4 8 4 

Up
0 2 8 4 
0 4 0 4 
0 0 0 0 
0 0 0 0 ");
        }

        [Test]
        public void ComplexDownTest()
        {
            Test(@"
2 8 4 0 
8 0 0 0 
0 0 0 0 
2 4 0 0 

Down
0 0 0 0 
2 0 0 0 
8 8 0 0 
2 4 4 0 ");
        }

        [Test]
        public void ComplexLeftTest()
        {
            Test(@"
0 2 2 4 
0 0 0 8 
2 0 0 4 
0 0 0 0 

Left
4 4 0 0 
8 0 0 0 
2 4 0 0 
0 0 0 0 ");
        }

        [Test]
        public void ComplexRightTest()
        {
            Test(@"
0 0 0 0 
4 0 0 0 
8 0 0 0 
2 0 2 2 

Right
0 0 0 0 
0 0 0 4 
0 0 0 8 
0 0 2 4");
        }

        private void Test(string @case)
        {
            var testCase = parse(@case);

            var grid = testCase.Item1;
            var move = testCase.Item2;
            var expected = testCase.Item3;

            var resGrid = GameLogic.MakeMove(grid, move);
            Assert.That(resGrid.ToString(), Is.EqualTo(expected.ToString()));
        }

        public Tuple<Grid, Move, Grid> parse(string testCase)
        {
            var lines = testCase.Split('\n').Select(l=>l.Trim()).ToArray();
            var init = new Grid(lines.Skip(1).Take(4).ToArray());
            var exp = new Grid(lines.Skip(7).Take(4).ToArray());
            var move = (Move) Enum.Parse(typeof (Move), lines[6]);

            return Tuple.Create(init, move, exp);
        }
    }
}