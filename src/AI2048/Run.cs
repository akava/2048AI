using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AI2048.Game;
using NUnit.Framework;

namespace AI2048
{
    [TestFixture]
    public class Run
    {
        private Move[] MOVES = { Move.Up, Move.Left, Move.Down, Move.Right };

        [Test]
        public void RunSimulation()
        {
            using (var game = new GamePage())
            {
                Console.WriteLine(game.GridState);
                game.Turn(Move.Up);
                Console.WriteLine("Up");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Left);
                Console.WriteLine("Left");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Down);
                Console.WriteLine("Down");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Right);
                Console.WriteLine("Right");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Up);
                Console.WriteLine("Up");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Left);
                Console.WriteLine("Left");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Down);
                Console.WriteLine("Down");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Right);
                Console.WriteLine("Right");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Up);
                Console.WriteLine("Up");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Left);
                Console.WriteLine("Left");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Down);
                Console.WriteLine("Down");
                Console.WriteLine(game.GridState);
                game.Turn(Move.Right);
                Console.WriteLine("Right");
                Console.WriteLine(game.GridState);
            }
        }

        [Test]
        public void RunGameLogicSelfCheck()
        {
            using (var game = new GamePage())
            {
                var moves = new []{
                    Move.Up, Move.Left, Move.Down, Move.Right,
                    Move.Up, Move.Left, Move.Down, Move.Right,
                    Move.Up, Move.Left, Move.Down, Move.Right,
                    Move.Up, Move.Left, Move.Down, Move.Right,
                    Move.Up, Move.Left, Move.Down, Move.Right,
                    Move.Up, Move.Left, Move.Down, Move.Right,
                };

                foreach (var move in moves)
                {
                    var prevState = game.GridState;
                    var expected = GameLogic.MakeMove(prevState, move);
                    game.Turn(move);

                    var actual = game.GridStateNoNew;

                    if (expected.ToString() != actual.ToString())
                    {
                        Console.WriteLine("Failed selfcheck");
                        Console.WriteLine(prevState);
                        Console.WriteLine(move);
                        Console.WriteLine(actual);
                    }
                }

            }  
        }

        [Test]
        public void OneTurnAheadAI()
        {
            using (var game = new GamePage())
            {
                while (game.CanMove)
                {
                    var move = makeDecision(game.GridState);
                    game.Turn(move);
                }
            }
        }

        private Move makeDecision(Grid state)
        {
            var simulationResults = new Dictionary<Move, int>();

            foreach (var move in MOVES)
            {
                var newState = GameLogic.MakeMove(state, move);
                var res = 0;
                if (newState.ToString() != state.ToString()) // don't make unnecesary moves
                    res = newState.EmptyCellsNo;
                simulationResults.Add(move, res);
            }
            
            var decision = simulationResults.OrderByDescending(p => p.Value).First();
            Console.WriteLine(String.Join(" ", simulationResults.Select(p=>p.Value.ToString()).ToArray()) + ">" + decision.Value);

            return decision.Key;
        }
    }
}
