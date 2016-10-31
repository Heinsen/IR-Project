using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TheApplication.Controller
{
    public class LexicalHelper
    {
        private string[] _StopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with", "what", "must", "when", "has", "anyone", "which", "how", "can" };

        /// <summary>
        /// Find phrases in a sentence
        /// </summary>
        /// <param name="UserQuery">The user query</param>
        /// <returns>Phrase List</returns>
        public List<string> FindPhrases(string UserQuery)
        {
            List<string> PhraseList = new List<string>();
            foreach (Match match in Regex.Matches(UserQuery, "\"([^\"]*)\""))
                PhraseList.Add(match.ToString().Trim('"'));
            return PhraseList;
        }

        /// <summary>
        /// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
        /// </summary>
        /// <param name="str">the information need</param>
        public string[] ProcessText(string str)
        {
            string[] tokens = TokeniseString(str);
            string[] tokensNoStop = StopWordFilter(tokens);

            return tokensNoStop;
        }

        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            char[] splitters = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };
            return text.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Removes stopwords from an array of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        /// <returns>The array of tokens without any stopwords</returns>
        protected string[] StopWordFilter(string[] tokens)
        {

            int numTokens = tokens.Count();
            List<string> filteredTokens = new List<string>();
            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                if (!_StopWords.Contains(token) && (token.Length > 2)) filteredTokens.Add(token);
            }
            return filteredTokens.ToArray<string>();
        }
    }
}
