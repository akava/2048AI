using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using AI2048.AI;
using AI2048.Game;
using NUnit.Framework;

namespace AI2048
{
    [TestFixture]
    public class Run
    {
        [Test]
        public void RunSimulation()
        {
            var agent = new OptiminiOptimaxAgent(Heuristic.AllRotatiions(Heuristic.CornerVave));

            for (int i = 0; i < 10; i++)
            using (var game = new GamePage())
            {
                while (game.CanMove)
                {
                    var move = agent.MakeDecision(game.GridState);
                    game.Turn(move);
                }

                game.TakeScreenshot().SaveAsFile("game_" + game.Score + ".png", ImageFormat.Png);
                Console.WriteLine(game.Score);
            }
        }
    }
}
