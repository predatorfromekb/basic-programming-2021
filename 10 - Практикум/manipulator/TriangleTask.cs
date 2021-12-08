using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        private static bool IsTriangle(double a, double b, double c)
        {
            return a >= 0 && b >= 0 && c >= 0 && a + b >= c && b + c >= a && c + a >= b;
        }

        public static double GetABAngle(double a, double b, double c)
        {
            if (!IsTriangle(a, b, c))
                return double.NaN;
            var smallAngle = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            return smallAngle;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(0, 1, 1, double.NaN)]
        [TestCase(1, 0, 1, double.NaN)]
        [TestCase(1, 1, 0, 0)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var actual = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, actual, 1e-7);
        }
    }
}