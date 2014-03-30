using System;
using System.Collections.Generic;
using System.Linq;
using AI2048.Game;

namespace AI2048.AI
{
    public class TwoTurnsAheadAgent: Agent
    {
        public TwoTurnsAheadAgent(Func<Grid, long> heurstk) : base(heurstk){}
        
        public override Move MakeDecision(Grid state)
        {
            var simulationResults = new Dictionary<Move, long>();
            foreach (var move in MOVES)
            {
                var newState = GameLogic.MakeMove(state, move);
                if (newState == state)
                    continue; // don't make unnecessary moves
                simulationResults.Add(move, makeDecision(newState).Value);
            }

            var decision = simulationResults.OrderByDescending(p => p.Value).First();
            Console.WriteLine(String.Join(" ", simulationResults.Select(p=>p.Value.ToString()).ToArray()) + ">" + decision.Value);

            return decision.Key;
        }

        public KeyValuePair<Move, long> makeDecision(Grid state)
        {
            var simulationResults = new Dictionary<Move, long>();
            foreach (var move in MOVES)
            {
                var newState = GameLogic.MakeMove(state, move);
                if (newState == state)
                    continue; // don't make unnecessary moves
                simulationResults.Add(move, _heuristic(newState));
            }

            var decision = simulationResults.OrderByDescending(p => p.Value).First();
            //Console.WriteLine(String.Join(" ", simulationResults.Select(p=>p.Value.ToString()).ToArray()) + ">" + decision.Value);

            return decision;
        }
    }
}
