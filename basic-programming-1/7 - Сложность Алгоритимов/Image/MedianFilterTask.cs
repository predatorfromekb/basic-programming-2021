using System;
using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		/* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
		public static double[,] MedianFilter(double[,] original)
		{
			var xLength = original.GetLength(0);
			var yLength = original.GetLength(1);
			var result = new double[xLength, yLength];
			for (var x = 0; x < xLength; x++)
			{
				for (var y = 0; y < yLength; y++)
				{
					result[x, y] = ApplyMedianFilter(original, x, y);
				}
			}

			return result;
		}
		
		private static double ApplyMedianFilter(double[,] image, int x, int y)
		{
			var width = image.GetLength(0);
			var height = image.GetLength(1);
			
			var left = Math.Max(0, x - 1);
			var right = Math.Min(width - 1, x + 1);
			var top = Math.Max(0, y - 1);
			var bottom = Math.Min(height - 1, y + 1);
			
			var values = new List<double>();
			for (var x1 = left; x1 <= right; x1++)
			{
				for (var y1 = top; y1 <= bottom; y1++)
				{
					values.Add(image[x1, y1]);
				}
			}
			
			values.Sort();
			return Median(values);
		}

		private static double Median(List<double> sortedValues)
		{
			var middle = sortedValues.Count / 2;
			if (sortedValues.Count % 2 == 0)
				return (sortedValues[middle - 1] + sortedValues[middle]) / 2;
			return sortedValues[middle];
		}
	}
}