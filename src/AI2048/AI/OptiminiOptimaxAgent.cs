using System;
using System.Collections.Generic;
using System.Linq;
using AI2048.Game;

namespace AI2048.AI
{
    public class OptiminiOptimaxAgent: Agent
    {
        public const int MAX_DEPTH = 3;

        public OptiminiOptimaxAgent(Func<Grid, long> heurstk) : base(heurstk) {}

        public override Move MakeDecision(Grid state)
        {
            var simulationResults = new Dictionary<Move, long>();
            foreach (var move in MOVES)
            {
                simulationResults.Add(move, evaluateMove(state, move));
            }

            var decision = simulationResults.OrderByDescending(p => p.Value).First();
            //Console.WriteLine(String.Join(" ", simulationResults.Select(p => p.Value.ToString()).ToArray()) + ">" + decision.Value);

            return decision.Key;
        }

        public long evaluateMove(Grid state, Move move)
        {
            var newState = GameLogic.MakeMove(state, move);
            if (newState == state)
                return long.MinValue;

            return maxNodeValue(newState, 0);
        }

        public long maxNodeValue(Grid state, int currDepth)
        {
            if (currDepth >= MAX_DEPTH)
                return _heuristic(state);

            var value = long.MinValue;
            foreach (var move in MOVES)
            {
                var newState = GameLogic.MakeMove(state, move);
                if (newState == state)
                    continue;

                long newVal;
                if (state.EmptyCellsNo > 3)
                    newVal = randomNodeValue(newState, currDepth+1);
                else
                    newVal = minNodeValue(newState, currDepth + 1);

                value = Math.Max(value, newVal);
            }

            return value;
        }

        public long randomNodeValue(Grid state, int currDepth)
        {
            var value = 0L;
            var nextStates = GameLogic.NextPossibleWorldStates(state);
            if (nextStates.Count == 0)
                return long.MinValue;

            foreach (var nextState in nextStates)
            {
                var newVal = maxNodeValue(nextState, currDepth + 1);
                value += newVal/nextStates.Count;
            }

            return value;
        }

        public long minNodeValue(Grid state, int currDepth)
        {
            var value = long.MaxValue;
            var nextStates = GameLogic.NextPossibleWorldStates(state);
            if (nextStates.Count == 0)
                return long.MinValue;

            foreach (var nextState in nextStates)
            {
                var newVal = maxNodeValue(nextState, currDepth + 1);
                value = Math.Min(value, newVal);
            }

            return value;
        }
    }
}