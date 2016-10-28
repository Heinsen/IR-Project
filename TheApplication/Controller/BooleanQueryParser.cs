using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Support; // for snowball analyser
using TheApplication.Controller;
using TheApplication.Model;
using System.Text.RegularExpressions;

namespace TheApplication.Controller
{
    public class BooleanQueryParser : IQueryParser
    {
        MultiFieldQueryParser _MultiFieldQueryParser;
        Analyzer _Analyzer;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string DOCUMENTID_FN = "DocumentId";
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string BIBLIOGRAPHIC_FN = "Bibliographic";
        const string ABSTRACT_FN = "Abstract";
        static List<SEDocument> documentColl = new List<SEDocument>();
        
        public BooleanQueryParser()
        {
            _Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
        }

        public void InitializeMultiFieldQueryParser(bool PreProcess)
        {
            String[] fields = new String[] { TITLE_FN, ABSTRACT_FN };
            if (PreProcess)
            {
                HashMap<string, float> boosts = new HashMap<string, float>();
                boosts.Add(TITLE_FN, (float)10);
                boosts.Add(ABSTRACT_FN, (float)5);

                _MultiFieldQueryParser = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30, fields,
                _Analyzer
                , boosts
                );

            }
            else
            {
                _MultiFieldQueryParser = new MultiFieldQueryParser(
                       Lucene.Net.Util.Version.LUCENE_30, fields,
                       _Analyzer);
            }

            _MultiFieldQueryParser.DefaultOperator = MultiFieldQueryParser.OR_OPERATOR;
        }

        /// <summary>
        /// Find phrases in a sentence
        /// </summary>
        /// <param name="UserQuery">The user query</param>
        /// <returns>Phrase List</returns>
        private List<string> FindPhrases(string UserQuery)
        {
            List<string> PhraseList = new List<string>();
            foreach (Match match in Regex.Matches(UserQuery, "\"([^\"]*)\""))
                PhraseList.Add(match.ToString().Trim('"'));
            return PhraseList;
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

        public Query ProcessQuery(string QueryString, bool PreProcess)
        {
            BooleanQuery FinalQuery = new BooleanQuery();

            InitializeMultiFieldQueryParser(PreProcess);

            BooleanQuery BooleanQuery = new BooleanQuery();
            if (PreProcess)
            {
                //Extract all phrases
                List<string> PhraseList = FindPhrases(QueryString);

                QueryParser myQueryParser = new QueryParser();
                foreach (string phrase in PhraseList)
                {
                    PhraseQuery abstractPhraseQuery = new PhraseQuery();
                    PhraseQuery titlePhraseQuery = new PhraseQuery();

                    abstractPhraseQuery.Add(new Term(ABSTRACT_FN, phrase));
                    titlePhraseQuery.Add(new Term(TITLE_FN, phrase));

                    abstractPhraseQuery.Boost = 1.2F;
                    abstractPhraseQuery.Slop = 3;
                    FinalQuery.Add(abstractPhraseQuery, Occur.SHOULD);
                    titlePhraseQuery.Boost = 4.0F;
                    titlePhraseQuery.Slop = 3;
                    FinalQuery.Add(titlePhraseQuery, Occur.SHOULD);
                }
            }
            else
            {
                FinalQuery.Add(_MultiFieldQueryParser.Parse(QueryString), Occur.SHOULD);
            }

            string[] tokens = TokeniseString(QueryString.Replace('\"', ' ').Replace('[', ' ').Replace(']', ' '));
            foreach (string term in tokens)
            {
                FinalQuery.Add(_MultiFieldQueryParser.Parse(term.Replace("~", "") + "~"), Occur.SHOULD);
            }

            FinalQuery.MinimumNumberShouldMatch = 2;

            return FinalQuery;
        }

    }
}
