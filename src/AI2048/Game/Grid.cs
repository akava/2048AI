using System;
using System.Collections.Generic;
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

        public static bool operator ==(Grid first, Grid second)
        {
            if (ReferenceEquals(null, first)) return false;
            if (ReferenceEquals(null, second)) return false;

            return first.ToString() == second.ToString(); // yeh, this is not the slowest operation here))
        }

        public static bool operator !=(Grid first, Grid second)
        {
            if (ReferenceEquals(null, first)) return false;
            if (ReferenceEquals(null, second)) return false;

            return !(first == second);
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

        public int SummAll()
        {
            var sum = 0;
            for (var x = 0; x < _grid.GetLength(0); x++)
                for (var y = 0; y < _grid.GetLength(0); y++)
                    sum+=_grid[x, y];
            return sum;
        }

        public int EmptyCellsNo
        {
            get
            {
                var n = 0;
                for (var x = 0; x < _grid.GetLength(0); x++)
                    for (var y = 0; y < _grid.GetLength(0); y++)
                        if(_grid[x, y] == 0)
                        n++;
                return n;
            }
        }

        public int[] Flatten()
        {
            var res = new List<int>();
            for (var x = 0; x < _grid.GetLength(0); x++)
                for (var y = 0; y < _grid.GetLength(0); y++)
                    res.Add(_grid[x, y]);
            return res.ToArray();
        }
    }
}