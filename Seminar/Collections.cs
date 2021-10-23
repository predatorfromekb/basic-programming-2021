using System;
using System.Linq;

namespace Seminar
{
    public static class Collections
    {
        public static void Task1()
        {
            var original = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var preprocessed = new int[original.Length][];

            Preprocess();

            void Preprocess()
            {
                for (var i = 0; i < preprocessed.Length; i++)
                {
                    var length = preprocessed.Length - i;
                    preprocessed[i] = new int[length];
                    var sum = 0;
                    for (var j = 0; j < length; j++)
                    {
                        sum = sum + original[j+i];
                        preprocessed[i][j] = sum;
                    }
                }
            }

            int Process(int L, int R)
            {
                return preprocessed[L][R - L];
            }

            Console.WriteLine(Process(1,4));

        }
        
        public static void Task2()
        {
            var original = new int[10];
            var preprocessed = new int[original.Length];

            void Update(int L, int R, int X)
            {
                preprocessed[L] += X;
                if (R < original.Length - 1)
                    preprocessed[R+1] -= X;
            }
            
            void Process()
            {
                var counter = 0;
                for (var i = 0; i < preprocessed.Length; i++)
                {
                    counter += preprocessed[i];
                    original[i] = counter;
                }
                Console.WriteLine(String.Join(",", original));
            }

            Update(1,4,2);
            Update(3,6,3);
            Update(2,5,-1);
            Process();
        }
    }
}