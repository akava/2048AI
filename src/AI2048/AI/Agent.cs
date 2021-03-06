﻿using System;
using AI2048.Game;

namespace AI2048.AI
{
    public abstract class Agent
    {
        protected Move[] MOVES = { Move.Up, Move.Left, Move.Down, Move.Right };

        protected readonly Func<Grid, long> _heuristic;
        protected Agent(Func<Grid, long> heurstk)
        {
            _heuristic = heurstk;
        }

        public abstract Move MakeDecision(Grid state);
    }
}