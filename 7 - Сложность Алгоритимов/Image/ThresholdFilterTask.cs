using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var result = new double[width, height];
			var whitePixelsCount = (int) (width * height * whitePixelsFraction);
			var threshold = GetThresholdValue(original, whitePixelsCount);
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					result[x, y] = original[x, y] >= threshold ? 1 : 0;
				}
			}

			return result;
		}
		
		private static double GetThresholdValue(double[,] original, int whitePixelsCount)
		{
			if (whitePixelsCount == 0) 
				return double.PositiveInfinity;
			
			var xLength = original.GetLength(0);
			var yLength = original.GetLength(1);

			var pixels = new List<double>(original.Length);
			for (var x = 0; x < xLength; x++)
			{
				for (var y = 0; y < yLength; y++)
				{
					pixels.Add(original[x,y]);
				}
			}
			// можно так
			// var pixels = original.Cast<double>().ToList();

			pixels.Sort();
			return pixels[pixels.Count - whitePixelsCount];
		}
	}
}