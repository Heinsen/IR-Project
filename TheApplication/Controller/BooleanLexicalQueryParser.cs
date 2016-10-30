using System;
using System.Collections.Generic;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using Lucene.Net.Support; // for snowball analyser

namespace TheApplication.Controller
{
    public class BooleanLexicalQueryParser : IQueryParser
    {
        MultiFieldQueryParser _MultiFieldQueryParser;
        Analyzer _Analyzer;
        PosTaggerLexicalHelper _PosTaggerLexicalParser;

        static List<SEDocument> documentColl = new List<SEDocument>();
        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        public BooleanLexicalQueryParser()
        {
            _Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            _PosTaggerLexicalParser = new PosTaggerLexicalHelper();
            _PosTaggerLexicalParser.LoadPOSTagger();
        }

        private void InitializeMultiFieldQueryParser(bool PreProcess)
        {
            String[] fields = new String[] { SEDocument.TITLE_FN, SEDocument.ABSTRACT_FN };
            if (PreProcess)
            {
                HashMap<string, float> boosts = new HashMap<string, float>();
                boosts.Add(SEDocument.TITLE_FN, (float)10);
                boosts.Add(SEDocument.ABSTRACT_FN, (float)5);

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


        public Query ProcessQuery(string QueryString, bool PreProcess)
        {
            BooleanQuery FinalQuery = new BooleanQuery();

            InitializeMultiFieldQueryParser(PreProcess);

            if (PreProcess)
            {
                //Extract all phrases
                List<string> PhraseList = _PosTaggerLexicalParser.FindPhrases(QueryString);
                QueryString = _PosTaggerLexicalParser.Parse(QueryString);

                foreach (string phrase in PhraseList)
                {
                    PhraseQuery abstractPhraseQuery = new PhraseQuery();
                    PhraseQuery titlePhraseQuery = new PhraseQuery();

                    abstractPhraseQuery.Add(new Term(SEDocument.ABSTRACT_FN, phrase));
                    titlePhraseQuery.Add(new Term(SEDocument.TITLE_FN, phrase));

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

            string[] tokens = _PosTaggerLexicalParser.TokeniseString(QueryString.Replace('\"', ' ').Replace('[', ' ').Replace(']', ' '));
            foreach (string term in tokens)
            {
                FinalQuery.Add(_MultiFieldQueryParser.Parse(term.Replace("~", "") + "~"), Occur.SHOULD);
            }

            FinalQuery.MinimumNumberShouldMatch = 2;

            return FinalQuery;
        }
    }
}
