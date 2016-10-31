using System;
using System.Collections.Generic;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Search; // for IndexSearcher
using Lucene.Net.QueryParsers;  // for QueryParser
using Lucene.Net.Support; // for snowball analyser

namespace TheApplication.Controller
{
    public class BooleanQueryParser : IQueryParser
    {
        MultiFieldQueryParser _MultiFieldQueryParserPreProcess;
        MultiFieldQueryParser _MultiFieldQueryParserNoPreProccess;
        Analyzer _Analyzer;
        LexicalHelper _LexicalParser;

        static List<SEDocument> documentColl = new List<SEDocument>();
        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        public BooleanQueryParser()
        {
            _Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            _LexicalParser = new LexicalHelper();
            InitializeMultiFieldQueryParser();
        }

        private void InitializeMultiFieldQueryParser()
        {

            String[] fields = new String[] { SEDocument.TITLE_FN, SEDocument.ABSTRACT_FN };

            //Initialization of Preprocess MultiFieldQueryParser
            HashMap<string, float> boosts = new HashMap<string, float>();
            boosts.Add(SEDocument.TITLE_FN, (float)10);
            boosts.Add(SEDocument.ABSTRACT_FN, (float)5);

            _MultiFieldQueryParserPreProcess = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30,
                fields,
                _Analyzer,
                boosts
                );
            _MultiFieldQueryParserPreProcess.DefaultOperator = MultiFieldQueryParser.OR_OPERATOR;

            //Initialization of No Preprocess MultiFieldQueryParser
            _MultiFieldQueryParserNoPreProccess = new MultiFieldQueryParser(
                       Lucene.Net.Util.Version.LUCENE_30,
                       fields,
                       _Analyzer);
            
            _MultiFieldQueryParserNoPreProccess.DefaultOperator = MultiFieldQueryParser.OR_OPERATOR;
        }
        
        public Query ProcessQuery(string QueryString, bool PreProcess)
        {
            BooleanQuery FinalQuery = new BooleanQuery();
            
            if (PreProcess)
            {
                //Extract all phrases
                List<string> PhraseList = _LexicalParser.FindPhrases(QueryString);

                foreach (string phrase in PhraseList)
                {
                    PhraseQuery abstractPhraseQuery = new PhraseQuery();
                    PhraseQuery titlePhraseQuery = new PhraseQuery();

                    abstractPhraseQuery.Add(new Term(SEDocument.ABSTRACT_FN, phrase));
                    titlePhraseQuery.Add(new Term(SEDocument.TITLE_FN, phrase));

                    abstractPhraseQuery.Boost = 3.0F;
                    abstractPhraseQuery.Slop = 2;
                    FinalQuery.Add(abstractPhraseQuery, Occur.SHOULD);
                    titlePhraseQuery.Boost = 4.0F;
                    titlePhraseQuery.Slop = 2;
                    FinalQuery.Add(titlePhraseQuery, Occur.SHOULD);
                }

                string[] tokens = _LexicalParser.ProcessText(QueryString);
                foreach (string term in tokens)
                {
                    FinalQuery.Add(_MultiFieldQueryParserPreProcess.Parse(term.Replace("~", "") + "~0.3"), Occur.SHOULD);
                }

            }
            else
            {
                FinalQuery.Add(_MultiFieldQueryParserNoPreProccess.Parse(QueryString), Occur.SHOULD);

                string[] tokens = _LexicalParser.ProcessText(QueryString);
                foreach (string term in tokens)
                {
                    FinalQuery.Add(_MultiFieldQueryParserNoPreProccess.Parse(term.Replace("~", "") + "~0.3"), Occur.SHOULD);
                }

            }

            FinalQuery.MinimumNumberShouldMatch = 2;
            return FinalQuery;
        }

    }
}
