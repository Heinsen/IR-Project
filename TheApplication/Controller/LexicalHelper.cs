using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using java.util;
using System.Text.RegularExpressions;

namespace TheApplication.Controller
{
    public class LexicalHelper
    {
        private static Dictionary<string, string> POSList;
        public string[] stopWords = { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with", "what", "must", "when", "has", "anyone", "which", "how", "can" };

        public string GetSynonyms(string word)
        {
            string url = string.Format("http://words.bighugelabs.com/api/2/7d767a7c110f12ce6521e0e6e3a46e55/{0}/txt", word);
            return CallRestMethod(url);
        }

        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            string[] splittedResult = result.Split('|');
            int synCount = splittedResult.Count() / 3;
            string syns = string.Empty;
            for (int i = 1; i <= 3; i++)
            {
                if (synCount >= i * 2)
                {
                    syns += splittedResult[i * 2].Split('\n')[0] + " ";
                }
            }
            webresponse.Close();
            return syns;
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
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        public string[] StemTokens(string[] tokens)
        {
            PorterStemmerAlgorithm.PorterStemmer myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            int numTokens = tokens.Count();
            string[] stems = new string[numTokens];
            for (int i = 0; i < numTokens; i++)
            {
                stems[i] = myStemmer.stemTerm(tokens[i]);
            }
            return stems;
        }

        /// <summary>
        /// Removes stopwords from an array of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        /// <returns>The array of tokens without any stopwords</returns>
        public string[] StopWordFilter(string[] tokens)
        {

            int numTokens = tokens.Count();
            List<string> filteredTokens = new List<string>();
            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                if (!stopWords.Contains(token) && (token.Length > 2)) filteredTokens.Add(token);
            }
            return filteredTokens.ToArray<string>();
        }

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

        public string Parse(string userQuery)
        {
            string[] basicquery = ProcessText(userQuery);
            string taggedQuery = POSTagger(userQuery);
            string[] query = TokeniseString(taggedQuery);

            string syns = string.Empty;
            foreach (string token in query)
            {
                string[] t = token.Split('/');
                if (t != null && t.Count() > 1 && POSList.ContainsKey(t[1].ToUpper()))
                {
                    if (t[1].ToUpper().Equals("JJ"))
                        t[0] = StemTokens(new string[] { t[0] }).FirstOrDefault();

                    try
                    {
                        syns = syns + " " + GetSynonyms(t[0]);
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            string[] expandedQuery = ProcessText(syns);
            return string.Join(" ", basicquery) + " " + string.Join(" ", expandedQuery.Distinct());
        }

        public Dictionary<string, string> BindPOSDictionary()
        {
            POSList = new Dictionary<string, string>();
            POSList.Add("NN", "Noun");
            POSList.Add("NNZ", "Noun");
            POSList.Add("NNS", "Noun");
            POSList.Add("NNP", "Noun");
            POSList.Add("NNPS ", "Noun");
            POSList.Add("JJ", "Adjective");
            return POSList;
        }

        /// <summary>
        /// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
        /// </summary>
        /// <param name="str">the information need</param>
        public string[] ProcessText(string str)
        {
            string[] tokens = TokeniseString(str);
            string[] tokensNoStop = StopWordFilter(tokens);
            //string[] stems = StemTokens(tokensNoStop);

            return tokensNoStop;
        }

        public MaxentTagger Tagger
        {
            get
            {
                if (tagger == null)
                {
                    LoadPOSTagger();
                }
                return tagger;
            }
        }

        private static MaxentTagger tagger;

        public void LoadPOSTagger()
        {
            var jarRoot = @"C:\Users\nahide\Documents\DataScience\IR\TheApplication\TheApplication\TheApplication\stanford-english-corenlp-2016-01-10-models\edu\stanford\nlp";
            var modelsDirectory = jarRoot + @"\models\pos-tagger";

            // Loading POS Tagger
            tagger = new MaxentTagger(modelsDirectory + @"\wsj-0-18-bidirectional-nodistsim.tagger");
        }

        private string POSTagger(string text)
        {
            string posSentence = string.Empty;
            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
            foreach (ArrayList sentence in sentences)
            {
                var taggedSentence = tagger.tagSentence(sentence);
                posSentence += Sentence.listToString(taggedSentence, false);
            }
            return posSentence;
        }
    }
}
