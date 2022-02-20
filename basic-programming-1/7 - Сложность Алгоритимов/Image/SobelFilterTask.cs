using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] image, double[,] sx)
        {
            var width = image.GetLength(0);
            var height = image.GetLength(1);
            var result = new double[width, height];
            var sy = Transpose(sx);
            var offset = sx.GetLength(0) / 2;
            for (var x = offset; x < width - offset; x++)
            {
                for (var y = offset; y < height - offset; y++)
                {
                    // Вместо этого кода должно быть
                    // поэлементное умножение матриц sx и полученной транспонированием из неё sy на окрестность точки (x, y)
                    // Такая операция ещё называется свёрткой (Сonvolution)
                    var gx = Convolute(image, sx, x, y);
                    var gy = Convolute(image, sy, x, y);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }
                
            return result;
        }
        
        private static double Convolute(double[,] image, double[,] matrix, int imageX, int imageY)
        {
            var size = matrix.GetLength(0);

            var left = imageX - size / 2;
            var top = imageY - size / 2;

            double convolution = 0;
            for (var mx = 0; mx < size; mx++)
            {
                for (var my = 0; my < size; my++)
                {
                    convolution += matrix[mx, my] * image[left + mx, top + my];
                }
            }

            return convolution;
        }
        
        private static double[,] Transpose(double[,] matrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);
            var result = new double[height, width];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    result[x, y] = matrix[y, x];   
                }
            }

            return result;
        }
    }
}