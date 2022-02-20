using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        private static readonly char[] sentenceSeparators = new[] {'.', '!', '?', ';', ':', '(', ')'};
        public static List<List<string>> ParseSentences(string text)
        {
            var sentences = text.Split(sentenceSeparators);
            var sentencesList = new List<List<string>>();
            
            foreach (var sentence in sentences)
            {
                var words = GetSentenceWords(sentence);
                if (words.Count > 0)
                    sentencesList.Add(words);
            }

            return sentencesList;
        }

        private static List<string> GetSentenceWords(string sentence)
        {
            var words = new List<string>();
            var builder = new StringBuilder();
            foreach (var symbol in sentence + ".")
            {
                if (char.IsLetter(symbol) || symbol == '\'')
                    builder.Append(char.ToLower(symbol));
                else if (builder.Length > 0)
                {
                    words.Add(builder.ToString());
                    builder.Clear();
                }
            }

            return words;
        }
    }
}