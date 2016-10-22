using LAIR.ResourceAPIs.WordNet;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using java.io;
//using java.util;
//using edu.stanford.nlp.ling;
//using edu.stanford.nlp.tagger.maxent;
using java.io;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;
using Console = System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Xml;
using TheApplication.Controller;

namespace TheApplication
{
    public class QueryParser : IQueryParser
    {
        PorterStemmerAlgorithm.PorterStemmer myStemmer; 
        System.Collections.Generic.Dictionary<string, int> tokenCount; 
        public string[] stopWords = {"a", "an", "and", "are", "as", "at", "be", "but", "by","for", "if", "in", "into", "is", "it","no", "not", "of", "on", "or", "such","that", "the", "their", "then", "there", "these","they", "this", "to", "was", "will", "with"};
        private static Dictionary<string, WordNetEngine.POS> POSList;

        public QueryParser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            //tokenCount = new Dictionary<string,int>();
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
        /// Find phrases in a sentence
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Phrase List</returns>
        public List<string> FindPhrase(string userQuery)
        {
            List<string> phraseList = new List<string>();
            foreach (Match match in Regex.Matches(userQuery, "\"([^\"]*)\""))
                phraseList.Add(match.ToString().Trim('"'));
            return phraseList;
        }

        /// <summary>
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        public string[] StemTokens(string[] tokens){
            int numTokens = tokens.Count();
            string[] stems = new string[numTokens];
            for(int i =0; i< numTokens; i++)
            {
                stems[i] = myStemmer.stemTerm(tokens[i]);
            }
            return stems;
        }
        
        /// <summary>
        /// Counts the occurrences of a given set of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        public void CountOccurrences(string[] tokens)
        {
            foreach (string s in tokens)
            {
                int count=0;
                tokenCount.TryGetValue(s, out count);
                count = count + 1;
                tokenCount[s] = count;
            }
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


        public string GetSynonyms(string word)
        {
            //string url = string.Format("http://words.bighugelabs.com/api/2/7d767a7c110f12ce6521e0e6e3a46e55/{0}/json", word);
            string url = string.Format("http://www.dictionaryapi.com/api/v1/references/thesaurus/xml/{0}?key=f8ebaf16-367e-415b-899e-a6126a040fa3", word);
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
            XmlDocument xmlResult = new XmlDocument();
            xmlResult.LoadXml(result);
            string syns = string.Empty;
            XmlNodeList xmlNode = xmlResult.SelectNodes("/entry_list/entry/sens/syn");
            if (xmlNode != null && xmlNode.Count > 0)
            {
                foreach (XmlNode xNode in xmlNode)
                {
                    syns += xNode.FirstChild.Value;
                    //"adhere (to), comply (with), conform (to), follow, goose-step (to), mind, observe"	string

                }
            }
            webresponse.Close();
            return syns;
        }


        public string InformationNeedParser(string userQuery)
        {
            string[] basicquery = ProcessText(userQuery);
            //string taggedQuery = POSTagger(userQuery);
            //string[] query = ProcessText(taggedQuery);
            Dictionary<string, WordNetEngine.POS> POSList = BindPOSDictionary();
            
        //http://words.bighugelabs.com/api/2/7d767a7c110f12ce6521e0e6e3a46e55/word/xml
        //Your API key is 7d767a7c110f12ce6521e0e6e3a46e55

            //http://www.dictionaryapi.com/api/v1/references/thesaurus/xml/umpire?key=[YOUR KEY GOES HERE]
            //Key (Thesaurus): f8ebaf16-367e-415b-899e-a6126a040fa3
            //Key (Dictionary): ecc5d2c5-f4a4-41db-b471-7cbc5127a7fd

            List<SynSet> synonyms = new List<SynSet>();
            string syns = string.Empty;
            foreach (string token in basicquery)
            {
                //string[] t = token.Split('/');
                //if (t!= null && t.Count() > 1 && POSList.ContainsKey(t[1].ToUpper()))
                //{
                    //syns = syns + " " + GetSynonyms(token);
                //synonyms = WordNet(t[0], POSList[t[1].ToUpper()]);

                synonyms = WordNet(token, POSList["NN"]);
                if (synonyms != null && synonyms.Count > 0)
                {
                    foreach (SynSet synSet in synonyms)
                    {
                        if (synSet.Words != null && synSet.Words.Any())
                            foreach (string s in synSet.Words)
                                if (!s.ToUpper().Equals(token.ToUpper()))
                                    syns = syns + " " + s;
                    }
                }
                //}
            }
            //string[] expandedQuery = ProcessText(syns);
            return string.Join(" ", basicquery) + " " + syns;
        }

        public static Dictionary<string, WordNetEngine.POS> BindPOSDictionary()
        {
            POSList = new Dictionary<string, WordNetEngine.POS>();
            POSList.Add("NN", WordNetEngine.POS.Noun);
            POSList.Add("NNZ", WordNetEngine.POS.Noun);
            POSList.Add("NNS", WordNetEngine.POS.Noun);
            POSList.Add("NNP", WordNetEngine.POS.Noun);
            POSList.Add("NNPS ", WordNetEngine.POS.Noun);
            POSList.Add("VBZ", WordNetEngine.POS.Verb);
            POSList.Add("VB", WordNetEngine.POS.Verb);
            POSList.Add("VBG", WordNetEngine.POS.Verb);
            POSList.Add("VBN", WordNetEngine.POS.Verb);
            POSList.Add("VBD", WordNetEngine.POS.Verb);
            POSList.Add("VBP", WordNetEngine.POS.Verb);
            POSList.Add("JJ", WordNetEngine.POS.Adjective);
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

        private WordNetEngine _wordNetEngine;

        public List<SynSet> WordNet(string word, WordNetEngine.POS pos)
        {
            string root = Directory.GetDirectoryRoot(".");
            _wordNetEngine = new WordNetEngine(root + @"\dev\wordnetapi\resources\", false);
            List<SynSet> synSetsToShow = null;

            SynSet synset = _wordNetEngine.GetMostCommonSynSet(word, WordNetEngine.POS.Noun);

            if (synset != null)
                synSetsToShow = new List<SynSet>(new SynSet[] { synset });
            return synSetsToShow;

        }

        MaxentTagger tagger = null;

        public void LoadPOSTagger()
        {
            var jarRoot = @"C:\Users\nahide\Documents\DataScience\IR\TheApplication\TheApplication\TheApplication\stanford-english-corenlp-2016-01-10-models\edu\stanford\nlp";
            var modelsDirectory = jarRoot + @"\models\pos-tagger";

            // Loading POS Tagger
            tagger = new MaxentTagger(modelsDirectory + @"\wsj-0-18-bidirectional-nodistsim.tagger");
        }

        private string POSTagger(string text)
        {
            //// Text for tagging
            //var text = "A Part-Of-Speech Tagger (POS Tagger) is a piece of software that reads text"
            //           + "in some language and assigns parts of speech to each word (and other token),"
            //           + " such as noun, verb, adjective, etc., although generally computational "
            //           + "applications use more fine-grained POS tags like 'noun-plural'.";
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
