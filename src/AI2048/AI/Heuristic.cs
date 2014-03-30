using System;
using System.Linq;
using AI2048.Game;

namespace AI2048.AI
{
    public static class Heuristic
    {
        public static readonly Func<Grid, long> EmptyCellsNr = state =>
        {
            var nums = state.Flatten();
            return nums.Count(n=>n==0);
        };

        public static readonly Func<Grid, long> MakeSplits = state =>
        {
            var nums = state.Flatten();
            return nums.Select(n => n*n).Sum();
        };

        public static readonly Func<Grid, long> MakeSplitsWithStickyBigNum = state =>
        {
            var res = MakeSplits(state);

            var nums = state.Flatten();
            if (nums[0] == nums.Max())
                res += 100000000;
            
            return res;
        };

        public static readonly Func<Grid, long> CornerVave = state =>
        {
            var h = 0;
            for (var x = 0; x < 4; x++)
                for (var y = 0; y < 4; y++)
                {
                    h += (x+ y + 1) * state[x, y] * state[x, y];
                }
            return h;
        };

        public static Func<Grid, long> AllRotatiions(Func<Grid, long> heu)
        {
            return state =>
            {
                var max = heu(state);
                for (var r = 0; r < 3; r++)
                {
                    state = GameLogic.RotateCW(state);
                    max = Math.Max(max, heu(state));
                }

                return max;
            };
        }
    }
}