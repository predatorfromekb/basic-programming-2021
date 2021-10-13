using System;
using System.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            return new HeatmapData(
                "Пример карты интенсивностей",
                GenerateData(names), 
                GenerateLabelsX(), 
                GenerateLabelsY());
        }

        private static double[,] GenerateData(NameData[] names)
        {
            var array = new double[30, 12];
            foreach (var name in names)
            {
                if (name.BirthDate.Day == 1)
                    continue;
                array[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            }

            return array;
        }
        
        private static string[] GenerateLabelsX()
        {
            return Enumerable
                .Range(2, 30)
                .Select(e => e.ToString())
                .ToArray();
        }
        
        private static string[] GenerateLabelsY()
        {
            return Enumerable
                .Range(1, 12)
                .Select(e => e.ToString())
                .ToArray();
        }
    }
}