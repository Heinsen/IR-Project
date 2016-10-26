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

namespace TheApplication.Model
{
    public class LuceneHelper : ILuceneHelper
    {
        Directory _LuceneIndexDirectory;
        Analyzer _Analyzer;
        IndexWriter _IndexWriter;
        IndexSearcher _IndexSearcher;
        MultiFieldQueryParser _MultiFieldQueryParser;
        Similarity _Similarity;
        float _BoostValue = 2.0F;
        List<SEDocument> _SourceCollection;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string DOCUMENTID_FN = "DocumentId";
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string BIBLIOGRAPHIC_FN = "Bibliographic";
        const string ABSTRACT_FN = "Abstract";
        static List<SEDocument> documentColl = new List<SEDocument>();

        public LuceneHelper()
        {
            _LuceneIndexDirectory = null;
            _IndexWriter = null;
            //_Analyzer = new Lucene.Net.Analysis.Snowball.SnowballAnalyzer(VERSION, "English");
            _Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(VERSION);
            _Similarity = new NewSimilarity();
        }

        public void CreateIndex(List<SEDocument> SEDocuments, string IndexPath)
        {
            _SourceCollection = SEDocuments;
            CreateIndex(IndexPath);
            IndexDocuments();
            CleanUpIndexer();
            CreateSearcher();
        }

        public void InitializeMultiFieldQueryParser(bool asis)
        {
            String[] fields = new String[] { TITLE_FN, ABSTRACT_FN };
            if (asis)
            {
                _MultiFieldQueryParser = new MultiFieldQueryParser(
                        Lucene.Net.Util.Version.LUCENE_30, fields,
                        _Analyzer);
            }
            else
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
            _MultiFieldQueryParser.DefaultOperator = MultiFieldQueryParser.OR_OPERATOR;
        }

        private void IndexDocuments()
        {
            foreach (SEDocument SEDocument in _SourceCollection)
            {
                IndexDocument(SEDocument);
            }
        }

        /// <summary>
        /// Creates the searcher object
        /// </summary>
        private void CreateSearcher()
        {
            _IndexSearcher = new IndexSearcher(_LuceneIndexDirectory);
            _IndexSearcher.Similarity = _Similarity; 
        }


        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        private void CreateIndex(string indexPath)
        {
            _LuceneIndexDirectory = FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            _IndexWriter = new IndexWriter(_LuceneIndexDirectory, _Analyzer, true, mfl);
            _IndexWriter.SetSimilarity(_Similarity); 
        }

        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        private void IndexDocument(SEDocument document)
        {
            Lucene.Net.Documents.Field documentIdField = new Field(DOCUMENTID_FN, document.ID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Lucene.Net.Documents.Field titleField = new Field(TITLE_FN, document.Title, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            titleField.Boost = _BoostValue;
            Lucene.Net.Documents.Field abstractField = new Field(ABSTRACT_FN, document.Abstract, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(documentIdField);
            doc.Add(titleField);
            doc.Add(abstractField);
            _IndexWriter.AddDocument(doc);
        }

        /// <summary>
        /// Flushes the buffer and closes the index
        /// </summary>
        private void CleanUpIndexer()
        {
            _IndexWriter.Optimize();
            _IndexWriter.Flush(true, true, true);
            _IndexWriter.Dispose();
        }



        public List<RankedSEDocument> SearchText(string QueryString, List<string> phraseList, List<SEDocument> _SourceCollection, bool asis, int page)
        {
            List<RankedSEDocument> RankedSEDocuments = new List<RankedSEDocument>();
            if (!string.IsNullOrEmpty(QueryString))
            {
                TopDocs results = Search(QueryString, phraseList, asis, page);
                LoadMatchedDocument(_SourceCollection, page, RankedSEDocuments, results);

            }
            return RankedSEDocuments;
        }

        public TopDocs Search(string QueryString, List<string> phraseList, bool asis, int page)
        {
            InitializeMultiFieldQueryParser(asis);

            BooleanQuery finalQuery = new BooleanQuery();
            if (asis)
            {
                finalQuery.Add(_MultiFieldQueryParser.Parse(QueryString), Occur.SHOULD);
            }
            else
            {
                //querytext = querytext.ToLower();
                QueryParser myQueryParser = new QueryParser();
                foreach (string phrase in phraseList)
                {
                    //finalQuery.Add(_MultiFieldQueryParser.Parse("\"" + phrase + "\"" + "^20"), Occur.SHOULD);
                    //_MultiFieldQueryParser.PhraseSlop = 2;
                    PhraseQuery abstractPhraseQuery = new PhraseQuery();
                    PhraseQuery titlePhraseQuery = new PhraseQuery();

                    abstractPhraseQuery.Add(new Term(ABSTRACT_FN, phrase));
                    titlePhraseQuery.Add(new Term(TITLE_FN, phrase));

                    abstractPhraseQuery.Boost = 1.2F;
                    abstractPhraseQuery.Slop = 3;
                    finalQuery.Add(abstractPhraseQuery, Occur.SHOULD);
                    titlePhraseQuery.Boost = 4.0F;
                    titlePhraseQuery.Slop = 3;
                    finalQuery.Add(titlePhraseQuery, Occur.SHOULD);

                    //string[] phraseParts = phrase.Split(' ');
                    //BooleanQuery innerquery = new BooleanQuery();
                    //foreach (string p in phraseParts)
                    //{
                    //    innerquery.Add(_MultiFieldQueryParser.Parse(p.Replace("~", "") + "~"), Occur.MUST);
                    //}
                    //finalQuery.Add(innerquery, Occur.SHOULD);
                }

                string[] tokens = myQueryParser.TokeniseString(QueryString.Replace('\"', ' ').Replace('[', ' ').Replace(']', ' '));
                foreach (string term in tokens)
                {
                        finalQuery.Add(_MultiFieldQueryParser.Parse(term.Replace("~", "") + "~"), Occur.SHOULD);
                }
                finalQuery.MinimumNumberShouldMatch = 2;


            }
            TopDocs results = _IndexSearcher.Search(finalQuery, (page == -1) ? 1400 : (page + 1) * 10);
            return results;
        }

        private void LoadMatchedDocument(List<SEDocument> _SourceCollection, int page, List<RankedSEDocument> RankedSEDocuments, TopDocs results)
        {
            int lowerBand = page * 10;
            int upperBand = (page + 1) * 10;
            ScoreDoc[] scoreDocs = results.ScoreDocs;
            int rank = 1;

            if (page == -1)
            {
                lowerBand = 0;
                upperBand = results.ScoreDocs.Length;
            }
            for (int i = lowerBand; i < upperBand && i < results.ScoreDocs.Length; i++)
            {
                //Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                Document doc = _IndexSearcher.Doc(scoreDocs[i].Doc);

                string documentId = doc.Get(DOCUMENTID_FN).ToString();
                SEDocument currentDoc = _SourceCollection.Where(d => d.ID == documentId).FirstOrDefault();
                if (currentDoc != null)
                {
                    if (page != -1)
                    {
                        RankedSEDocuments.Add(new RankedSEDocument
                        (
                            documentId,
                            !string.IsNullOrEmpty(currentDoc.Title) ? currentDoc.Title : string.Empty,
                            !string.IsNullOrEmpty(currentDoc.Author) ? currentDoc.Author : string.Empty,
                            !string.IsNullOrEmpty(currentDoc.Bibliographic) ? currentDoc.Bibliographic : string.Empty,
                            !string.IsNullOrEmpty(currentDoc.Abstract) ? currentDoc.Abstract : string.Empty,
                            (page * 10) + i,
                            0
                        )
                        );
                    }
                    else
                    {
                        RankedSEDocuments.Add(new RankedSEDocument(

                                                documentId,
                                                rank++,
                                                scoreDocs[i].Score

                                            ));
                    }
                }
            }
        }

    }
}
