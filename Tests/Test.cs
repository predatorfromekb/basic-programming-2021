using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        [TestCase(1, -6, 9, 1, 3, TestName = "OneRoot")]
        [TestCase(1, -3, 2, 2, 2, 1, TestName = "TwoRoots")]
        public void Solve(int a, int b, int c, params double[] roots)
        {
            CollectionAssert.AreEqual(roots, roots);
        }
    }
}