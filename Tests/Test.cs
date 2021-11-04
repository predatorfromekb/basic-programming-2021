﻿using NUnit.Framework;

namespace Seminar
{
    [TestFixture]
    public class Test
    {
        [TestCase(1, -6, 9, 1, 3, TestName = "OneRoot")]
        [TestCase(1, -3, 2, 2, 2, 1, TestName = "TwoRoots")]
        public void Solve(int a, int b, int c, params double[] roots)
        {
            var result = QuadraticEquationSolver.Solve(a, b, c);
            CollectionAssert.AreEqual(roots, result);
        }
    }
}