using System;
using System.Linq;
using AI2048.Game;
using NUnit.Framework;

namespace AI2048.Tests
{
    [TestFixture]
    public class GridOperationsTests
    {
        [Test]
        public void RotateCWTest()
        {
            Test(@"
0 0 0 0 
4 0 0 0 
8 0 0 0 
2 0 2 2 

2 8 4 0 
0 0 0 0 
2 0 0 0 
2 0 0 0");
        }

        [Test]
        public void RotateCWFullTest()
        {
            Test(@"
01 02 03 04 
05 06 07 08 
09 10 11 12 
13 14 15 16 

13 09 05 01 
14 10 06 02 
15 11 07 03 
16 12 08 04 ");
        }

        private void Test(string @case)
        {
            var testCase = parse(@case);

            var grid = testCase.Item1;
            var expected = testCase.Item2;

            var resGrid = GameLogic.RotateCW(grid);
            Assert.That(resGrid.ToString(), Is.EqualTo(expected.ToString()));
        }

        public Tuple<Grid, Grid> parse(string testCase)
        {
            var lines = testCase.Split('\n').Select(l => l.Trim()).ToArray();
            var init = new Grid(lines.Skip(1).Take(4).ToArray());
            var exp = new Grid(lines.Skip(6).Take(4).ToArray());

            return Tuple.Create(init, exp);
        }         
    }
}