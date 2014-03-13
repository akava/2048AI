using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

namespace AI2048.Game
{

//0 0 0 0 
//2 0 0 0 
//8 8 0 2 
//2 4 4 0 
//
//Right
//0 0 0 2 
//0 0 0 2 
//0 0 16 2 
//0 0 2 8 

    public class GameLogic
    {
        public static Grid MakeMove(Grid grid, Move move)
        {
            var newGrid = new int[4, 4];
            switch (move)
            {
                case Move.Left:
                    for (var y = 0; y < 4; y++)
                    {
                        var nonZeroes = merge(grid.GetRow(y).Where(n => n != 0).ToArray());
                        var l = nonZeroes.Length;
                        for (var i = 0; i < l; i++)
                        {
                            newGrid[i, y] = nonZeroes[i];
                        }
                    }
                    break;
                case Move.Right:
                    for (var y = 0; y < 4; y++)
                    {
                        var nonZeroes = merge(grid.GetRow(y).Where(n => n != 0).ToArray().Reverse().ToArray()).Reverse().ToArray();
                        var l = nonZeroes.Length;
                        for (var i = 0; i < l; i++)
                        {
                            newGrid[i + (4-l), y] = nonZeroes[i];
                        }
                    }
                    break;
                case Move.Up:
                    for (var x = 0; x < 4; x++)
                    {
                        var nonZeroes = merge(grid.GetColumn(x).Where(n => n != 0).ToArray());
                        var l = nonZeroes.Length;
                        for (var i = 0; i < l; i++)
                        {
                            newGrid[x, i] = nonZeroes[i];
                        }
                    }
                    break;
                case Move.Down:
                    for (var x = 0; x < 4; x++)
                    {
                        var nonZeroes = merge(grid.GetColumn(x).Where(n => n != 0).ToArray().Reverse().ToArray()).Reverse().ToArray();
                        var l = nonZeroes.Length;
                        for (var i = 0; i < l; i++)
                        {
                            newGrid[x, i + (4-l)] = nonZeroes[i];
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("move");
            }
            return new Grid(newGrid);
        }

        private static int[] merge(int[] line)
        {
            if (line.Length <= 1)
                return line;
            if (line.Length == 2)
            {
                if (line[0] == line[1])
                    return new[] {line[0]*2};
                return line;
            }
            if (line.Length == 3)
            {
                if (line[0] == line[1])
                    return new[] { line[0] * 2, line[2] };
                if (line[1] == line[2])
                    return new[] { line[0], line[1]* 2 };
                return line;
            }
            if (line.Length == 4)
            {
                if (line[0] == line[1])
                {
                    if (line[2] == line[3])
                        return new[] {line[0]*2, line[2]*2};
                    return new[] { line[0] * 2, line[2], line[3] };
                }
                if (line[1] == line[2])
                    return new[] { line[0], line[1] * 2, line[3] };
                if (line[2] == line[3])
                    return new[] { line[0], line[1], line[2] * 2 };
                return line;
            }

            return line;
        }
    }
}