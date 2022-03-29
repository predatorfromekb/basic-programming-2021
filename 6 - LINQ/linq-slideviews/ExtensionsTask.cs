using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			var orderedItems = items
				.OrderBy(e => e)
				.ToList();
			var count = orderedItems.Count;
			if (orderedItems.Count == 0)
				throw new InvalidOperationException();
			if (count % 2 == 1)
				return orderedItems[count / 2];
			return (orderedItems[count / 2] + orderedItems[count / 2 - 1]) / 2;
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var first = true;
			var prev = default(T);
			foreach (var item in items)
			{
				if (!first)
					yield return Tuple.Create(prev, item);
				first = false;
				prev = item;
			}
		}
	}
}