using System;
using System.Linq;
using AI2048.Game;

namespace AI2048.AI
{
    public static class Heuristic
    {
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
    }
}