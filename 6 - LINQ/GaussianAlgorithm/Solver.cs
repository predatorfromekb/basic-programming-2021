using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        private const double EPS = 1e-9;
        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            
            var mtrx = GetMatrix(matrix, freeMembers);
            var length = mtrx.RowsCount();
            var usedRow = new bool[length];
            var result = new double[length];

            for (var x = 0; x < mtrx.ColumnsCount() - 1; x++) // столбцов на 1 больше чем строк
            {
                int? foundUnusedRow = null;
                for (var y = 0; y < length; y++)
                {
                    if (mtrx[x, y] == 0)
                    {
                        usedRow[y] = true;
                        continue;
                    }
                        
                    if (!usedRow[y])
                    {
                        foundUnusedRow = y;
                        usedRow[y] = true;
                        break;
                    }
                }

                if (foundUnusedRow == null)
                {
                    continue;
                }
                else
                {
                    var currentRowNumber = foundUnusedRow.Value;
                    for (var y = 0; y < length; y++)
                    {
                        if (usedRow[y])
                            continue;
                        var yToCurrentRowNumberMultiplier = mtrx[x, currentRowNumber];
                        var currentRowNumberToYMultiplier = mtrx[x, y];
                        mtrx.Multiply(y, yToCurrentRowNumberMultiplier);
                        mtrx.Multiply(currentRowNumber, currentRowNumberToYMultiplier);
                        mtrx.Substract(y, currentRowNumber);
                    }
                }
            }
            return result;
        }
        
        private static double[,] GetMatrix(double[][] matrix, double[] freeMembers)
        {
            if (matrix.Length != freeMembers.Length)
                throw new ArgumentException();
            
            var length = freeMembers.Length;
            var arr = new double[length + 1, length];
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    arr[j, i] = matrix[i][j];
                }
                arr[length, i] = freeMembers[i];
            }

            return arr;
        }
    }

    public static class TwoDimensionalArrayExtensions
    {
        public static int ColumnsCount(this double[,] arr) => arr.GetLength(0);
        public static int RowsCount(this double[,] arr) => arr.GetLength(1);
        public static void Multiply(this double[,] arr, int y, double multiplier)
        {
            for (var x = 0; x < arr.ColumnsCount(); x++)
            {
                arr[x, y] *= multiplier;
            }
        }
        
        public static void Substract(this double[,] arr, int y, int yAnother)
        {
            for (var x = 0; x < arr.ColumnsCount(); x++)
            {
                arr[x, y] -= arr[x, yAnother];
            }
        }
    }
}
