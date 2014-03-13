using System;
using NUnit.Framework;

namespace AI2048
{
    [TestFixture]
    public class Run
    {


        [Test]
        public void RunSimulation()
        {
            using (var game = new GamePage())
            {
                game.Turn(Move.Up);
                game.Turn(Move.Left);


                Console.WriteLine("Score is: " + game.Score);

                var grid = game.GridState;

                Console.WriteLine("Grid is");

                for (var y = 0; y < grid.GetLength(0); y++)
                {
                    for (var x = 0; x < grid.GetLength(0); x++)
                    {
                        Console.Write(grid[x,y] + " ");
                    }
                    Console.WriteLine();
                }
            }  
        }
    }
}
