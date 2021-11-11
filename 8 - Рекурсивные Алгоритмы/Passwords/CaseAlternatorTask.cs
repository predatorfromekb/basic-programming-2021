using System.Collections.Generic;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        //Тесты будут вызывать этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        private static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                result.Add(new string (word));
                return;
            }

            var currentChar = word[startIndex];

            var upperChar = char.ToUpper(currentChar);
            var lowerChar = char.ToLower(currentChar);

            if (!char.IsLetter(currentChar) || upperChar == lowerChar)
            {
                AlternateCharCases(word, startIndex + 1, result);
            }
            else
            {
                word[startIndex] = lowerChar;
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = upperChar;
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}