using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var frequencies = BuildFrequencies(text);
            foreach (var bigram in frequencies)
                result.Add(bigram.Key, GetMostFrequent(bigram.Value));
            return result;
        }
        
        private static Dictionary<string, Dictionary<string, int>> BuildFrequencies(List<List<string>> text)
        {
            var frequencies = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (var i = 0; i < sentence.Count - 1; i++)
                {
                    IncrementFrequencySafely(frequencies, sentence[i], sentence[i + 1]);
                    if (i < sentence.Count - 2)
                        IncrementFrequencySafely(frequencies, sentence[i] + " " + sentence[i + 1], sentence[i+2]);
                }
            }
            return frequencies;
        }
        
        private static void IncrementFrequencySafely(Dictionary<string, Dictionary<string, int>> frequencies,
            string beginning, string nextWord)
        {
            if (!frequencies.ContainsKey(beginning))
                frequencies[beginning] = new Dictionary<string, int>();
            if (!frequencies[beginning].ContainsKey(nextWord))
                frequencies[beginning][nextWord] = 0;
            frequencies[beginning][nextWord]++;
        }
        
        private static string GetMostFrequent(Dictionary<string, int> wordFrequencies)
        {
            var result = "";
            var maxFrequency = int.MinValue;
            foreach (var word in wordFrequencies)
            {
                if (word.Value > maxFrequency ||
                    word.Value == maxFrequency && string.CompareOrdinal(word.Key, result) < 0)
                {
                    result = word.Key;
                    maxFrequency = word.Value;
                }
            }
            return result;
        }
   }
}