using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			var sum = double.NaN;
			foreach (var point in data)
			{
				if (double.IsNaN(sum))
				{
					sum = point.OriginalY;
				}
				else
				{
					sum = sum * (1 - alpha) + alpha * point.OriginalY;
				}
				
				yield return point.WithExpSmoothedY(sum);
			}
		}
	}
}