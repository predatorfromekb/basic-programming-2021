using System.Collections.Generic;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("aa ", new[] { "aa" })]
        [TestCase("bs sd", new[] { "bs", "sd" })]
        [TestCase(" d ", new[] { "d" })]
        [TestCase(" 'sd' ", new[] { "sd" })]
        [TestCase("'a'b", new[] { "a", "b" })]
        [TestCase("'b ", new[] { "b " })]
        [TestCase("a   \"b", new[] { "a", "b" })]
        [TestCase("'b ", new[] { "b " })]
        [TestCase("' '", new[] { " " })]
        [TestCase("'\"'", new[] { "\"" })]
        [TestCase(@"'\\\\\\'", new[] { @"\\\" })]
        [TestCase(@"'a\'\\'", new[] { @"a'\" })]
        [TestCase(@"", new string[0])]
        [TestCase(@"ab'a'", new[] { "ab", "a" })]
        [TestCase(@"""""", new[] { "" })]
        [TestCase(@"""a'""", new[] { "a'" })]
        [TestCase(@"""a\""""", new[] { "a\"" })]
        public static void RunTests(string input, string[] expectedResult)
        {
            Test(input, expectedResult);
        }
    }

    public class FieldsParserTask
    {
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        public static List<Token> ParseLine(string line)
        {
            var fields = new List<Token>();
            var i = 0;
            while (i < line.Length)
            {
                var spaces = ReadSpacesToken(line, i);
                if (spaces.GetIndexNextToToken() >= line.Length)
                    break;
                var field = ReadField(line, spaces.GetIndexNextToToken());
                fields.Add(field);
                i = field.GetIndexNextToToken();
            }
            return fields;
        }
        
        private static Token ReadSpacesToken(string line, int startIndex)
        {
            var index = startIndex;
            while (index < line.Length && line[index] == ' ')
                index++;
            return new Token("", startIndex, index - startIndex);
        }
        
        private static Token ReadField(string line, int startIndex)
        {
            var ch = line[startIndex];
            if (ch == '"' || ch == '\'') return QuotedFieldTask.ReadQuotedField(line, startIndex);
            return ReadSimpleField(line, startIndex);
        }

        public static Token ReadSimpleField(string line, int startIndex)
        {
            var index = startIndex;
            while (index < line.Length && line[index] != ' ' && line[index] != '"' && line[index] != '\'')
                index++;
            var length = index - startIndex;
            return new Token(line.Substring(startIndex, length), startIndex, length);
        }
    }
}