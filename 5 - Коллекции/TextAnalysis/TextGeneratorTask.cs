using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var phrase = phraseBeginning.Split().ToList();
            if (phrase.Count == 0)
                return "";
            var prevWord = phrase[phrase.Count - 1];
            var prevPrevWord = phrase.Count > 1 
                ? phrase[phrase.Count - 2] 
                : "";

            for (var i = 0; i < wordsCount; i++)
            {
                var nextWord = FindNextWord(nextWords, prevPrevWord, prevWord);
                if (nextWord == null) 
                    break;
                phrase.Add(nextWord);
                prevPrevWord = prevWord;
                prevWord = nextWord;
            }
            
            return string.Join(" ", phrase);
        }
        
        private static string FindNextWord(
            Dictionary<string, string> mostFrequentNextWords, 
            string previousWord, 
            string currentWord)
        {
            if (previousWord != "" && mostFrequentNextWords.ContainsKey(previousWord + " " + currentWord)) 
                return mostFrequentNextWords[previousWord + " " + currentWord];
            if (mostFrequentNextWords.ContainsKey(currentWord)) 
                return mostFrequentNextWords[currentWord];
            return null;
        }
    }
}