using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Skip(1)
				.Select(ParseSlideRecord)
				.Where(e => e != null)
				.ToDictionary(e => e.SlideId, e => e);
		}

		private static SlideRecord ParseSlideRecord(string line)
		{
			if (string.IsNullOrEmpty(line))
				return null;
			var parsed = line.Split(';');
			if (parsed.Length != 3 
			    || !int.TryParse(parsed[0], out var id)
			    || !Enum.TryParse<SlideType>(parsed[1], true, out var slideType))
				return null;

			return new SlideRecord(id, slideType, parsed[2]);
		}

		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines.Skip(1)
				.Select(line => ParseVisitRecord(line, slides));
		}
		
		private static VisitRecord ParseVisitRecord(
			string line, IDictionary<int, SlideRecord> slides)
		{
			var record = line.Split(';');
			if (record.Length < 4 
			    || !int.TryParse(record[0], out var userId) 
			    || !int.TryParse(record[1], out var slideId)
			    || !DateTime.TryParse(record[2], out var date)
			    || !TimeSpan.TryParse(record[3], out var time)
			    || !slides.ContainsKey(slideId)
			    )
				throw new FormatException($"Wrong line [{line}]");

			return new VisitRecord(
				userId,
				slideId,
				date + time,
				slides[slideId].SlideType
			);
		}
	}
}