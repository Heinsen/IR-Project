using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using java.util;

namespace TheApplication.Controller
{
    class PosTaggerLexicalHelper : LexicalHelper
    {
        private static Dictionary<string, string> _POSList = new Dictionary<string, string>()
         {
           { "NN", "Noun" },
           { "NNZ", "Noun" },
           { "NNS", "Noun" },
           { "NNP", "Noun" },
           { "NNPS ", "Noun" },
           { "JJ", "Adjective" }
         };

        private static string CallRestMethod(string url)
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

        private string GetSynonyms(string word)
        {
            string url = string.Format("http://words.bighugelabs.com/api/2/7d767a7c110f12ce6521e0e6e3a46e55/{0}/txt", word);
            return CallRestMethod(url);
        }

        /// <summary>
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        private string[] StemTokens(string[] tokens)
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

        public string Parse(string userQuery)
        {
            string[] basicquery = ProcessText(userQuery);
            string taggedQuery = POSTagger(userQuery);
            string[] query = TokeniseString(taggedQuery);

            string syns = string.Empty;
            foreach (string token in query)
            {
                string[] t = token.Split('/');
                if (t != null && t.Count() > 1 && _POSList.ContainsKey(t[1].ToUpper()))
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
