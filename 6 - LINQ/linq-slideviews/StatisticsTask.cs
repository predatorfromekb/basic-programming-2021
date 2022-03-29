using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			var minutes = visits
				.GroupBy(e => e.UserId)
				.SelectMany(e =>
				{
					return e
						.OrderBy(x => x.DateTime)
						.Bigrams()
						.Where(r => r.Item1.SlideType == slideType)
						.Select(r => r.Item2.DateTime - r.Item1.DateTime)
						.Where(time => time >= TimeSpan.FromMinutes(1) && time <= TimeSpan.FromHours(2));
				})
				.Select(e => e.TotalMinutes)
				.ToList();

			return minutes.Any() ? minutes.Median() : 0;
		}
	}
}