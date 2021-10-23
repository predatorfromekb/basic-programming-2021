using System;
using System.Collections.Generic;

namespace Seminar
{
    public class Arrays
    {
        public static void Task2()
        {
            var first = new[] {1, 2, 3, 5};
            var second = new[] {2, 3, 4, 5};

            // Пересечение: 2,3,5
            // Обьединение: 1,2,3,4,5
            // Разность: 1,4
            
            var first1 = new[] {1, 2, 3, 4};
            var second1 = new[] {5, 6, 7, 8};

            // Пересечение: 
            // Обьединение: 1,2,3,4,5,6,7,8
            // Разность: 1,2,3,4,5,6,7,8
            
            var first2 = new[] {1, 1, 3, 3};
            var second2 = new[] {1, 1, 3, 3};

            // Пересечение: 
            // Обьединение: 1,3
            // Разность:

            Console.WriteLine(string.Join(",", MainLogic(first, second)));
            Console.WriteLine(string.Join(",", MainLogic(first1, second1)));
            Console.WriteLine(string.Join(",", MainLogic(first2, second2)));
        }
        

        private static int[] MainLogic(int[] first, int[] second)
        {
            const int Nodata = -1;
            var firstIndex = Nodata;
            var secondIndex = Nodata;
            var firstBound = first.Length - 1;
            var secondBound = second.Length - 1;

            var minValue = Nodata;
            var result = new List<int>();
            
            for (var i = 0; i < first.Length + second.Length; i++)
            {
                if (firstIndex == firstBound)
                {
                    AddIfMoreThanMin(second[secondIndex + 1]);
                    secondIndex++;
                }
                else if (secondIndex == secondBound)
                {
                    AddIfMoreThanMin(first[firstIndex + 1]);
                    firstIndex++;
                }
                else if (first[firstIndex + 1] > second[secondIndex + 1])
                {
                    AddIfMoreThanMin(second[secondIndex + 1]);
                    secondIndex++;
                } 
                else if (first[firstIndex + 1] < second[secondIndex + 1])
                {
                    AddIfMoreThanMin(first[firstIndex + 1]);
                    firstIndex++;
                }
                else // first[firstIndex] == second[secondIndex]
                {
                    //AddIfMoreThanMin(second[secondIndex + 1]);
                    minValue = second[secondIndex + 1];
                    firstIndex++;
                }
                //Console.WriteLine($"{(firstIndex == Nodata ? "*" : firstIndex)}-{(secondIndex == Nodata ? "*" : secondIndex)}");
                
                 void AddIfMoreThanMin(int element)
                 {
                     if (minValue < element)
                     {
                         result.Add(element);
                         minValue = element;
                     }
                 }
            }

            return result.ToArray();
        }
    }
}