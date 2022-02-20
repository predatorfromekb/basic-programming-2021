using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2, TestName = "Empty")]
        [TestCase("'a'", 0, "a", 3, TestName = "SingleQuotes")]
        [TestCase("\"a\"", 0, "a", 3, TestName = "DoubleQuotes")]
        [TestCase("a'a'", 1, "a", 3, TestName = "StartWithCustomPosition")]
        [TestCase("\"'b'\"", 0, "'b'", 5, TestName = "NestedQuotes")]
        [TestCase("'a' 'b'", 0, "a", 3, TestName = "TwoFields")]
        [TestCase("'a", 0, "a", 2, TestName = "NoClosingQuote")]
        [TestCase("'", 0, "", 1, TestName = "TokenWithOnlyOneQuote")]
        [TestCase("'\\'a\\''", 0, "'a'", 7)]
        [TestCase("'\\\\'", 0, "\\", 4)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var index = startIndex + 1;
            var builder = new StringBuilder();
            var quotesCount = 1;
            var slashesCount = 0;
            while (index < line.Length)
            {
                if (line[index] == line[startIndex])
                {
                    quotesCount++;
                    break;
                }
                if (line[index] == '\\')
                {
                    slashesCount++;
                    index++;
                }
                builder.Append(line[index]);
                index++;
            }

            var result = builder.ToString();
            return new Token(result, startIndex, result.Length + quotesCount + slashesCount);
        }
    }
}
