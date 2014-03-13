using System;
using System.Linq;
using System.Text;

namespace AI2048.Game
{
    public class Grid
    {
        private readonly int[,] _grid; 
        public Grid(int[,] grid)
        {
            _grid = grid;
        }

        public Grid(string[] rows)
        {
            var grid = new int[4, 4];
            for (var y = 0; y < rows.Length; y++)
            {
                var vals = rows[y].Split(' ').Select(int.Parse).ToArray();
                for (var x = 0; x < vals.Length; x++)
                {
                    grid[x, y] = vals[x];
                }
            }

            _grid = grid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < _grid.GetLength(0); y++)
            {
                for (var x = 0; x < _grid.GetLength(0); x++)
                {
                    sb.Append(_grid[x, y] + " ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public int[] GetRow(int y)
        {
            var res = new int[4];
            for (var i = 0; i < 4; i++)
            {
                res[i] = _grid[i, y];
            }
            return res;
        }

        public int[] GetColumn(int x)
        {
            var res = new int[4];
            for (var i = 0; i < 4; i++)
            {
                res[i] = _grid[x, i];
            }
            return res;
        }
    }
}