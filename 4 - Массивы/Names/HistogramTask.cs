using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            return new HistogramData(
                $"Рождаемость людей с именем '{name}'", 
                GenerateLabels(), 
                GenerateValues(names, name));
        }
        
        private static double[] GenerateValues(NameData[] names, string currentName)
        {
            var array = new double[31];
            foreach (var name in names)
            {
                if (name.Name != currentName || name.BirthDate.Day == 1)
                    continue;
                array[name.BirthDate.Day - 1]++;
            }

            return array;
        }

        private static string[] GenerateLabels()
        {
            return Enumerable
                .Range(1, 31)
                .Select(e => e.ToString())
                .ToArray();
        }
    }
}