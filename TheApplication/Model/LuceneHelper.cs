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
    public class LuceneHelper
    {
        Directory _LuceneIndexDirectory;
        Analyzer _Analyzer;
        IndexWriter _IndexWriter;
        IndexSearcher _IndexSearcher;
        MultiFieldQueryParser _MultiFieldQueryParser;
        Similarity _Similarity;
        float _BoostValue = 2;
        List<SEDocument> _SourceCollection;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string DOCUMENTID_FN = "DocumentId";
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string BIBLIOGRAPHIC_FN = "Bibliographic";
        const string ABSTRACT_FN = "Abstract";
        static List<SEDocument> documentColl = new List<SEDocument>();

        public LuceneHelper(List<SEDocument> SEDocuments, string IndexPath)
        {
            _SourceCollection = SEDocuments;
            //_LuceneIndexDirectory

            _Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            String[] fields = new String[] { TITLE_FN, ABSTRACT_FN };
            IDictionary<String, float> boosts = new Dictionary<String, float>();
            boosts.Add(TITLE_FN, _BoostValue);
            _MultiFieldQueryParser = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30,
                fields,
                _Analyzer
            //,
            //boosts
            );

            //parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] {TITLE_FN, ABSTRACT_FN}, analyzer);

            //newSimilarity = new NewSimilarity(); 
            CreateIndex(IndexPath);
            IndexDocuments();
            CleanUpIndexer();
            CreateSearcher();
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
            //searcher.Similarity = newSimilarity; 
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
            //writer.SetSimilarity(newSimilarity); 
        }

        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        private void IndexDocument(SEDocument document)
        {
            Field documentIdField = new Field(DOCUMENTID_FN, document.ID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Field titleField = new Field(TITLE_FN, document.Title, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            titleField.Boost = _BoostValue;
            Field authorField = new Field(AUTHOR_FN, document.Author, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Field bibliographicField = new Field(BIBLIOGRAPHIC_FN, document.Bibliographic, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Field abstractField = new Field(ABSTRACT_FN, document.Abstract, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            Document doc = new Document();
            doc.Add(documentIdField);
            doc.Add(titleField);
            doc.Add(authorField);
            doc.Add(bibliographicField);
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
        
        public List<RankedSEDocument> SearchText(string QueryString)
        {
            List<RankedSEDocument> RankedSEDocuments = new List<RankedSEDocument>();
            if (!string.IsNullOrEmpty(QueryString))
            {
                //Should we do ToLower in the QueryParser instead?
                QueryString = QueryString.ToLower();
                Query query = _MultiFieldQueryParser.Parse(QueryString);

                TopDocs results = _IndexSearcher.Search(query, 100);
                int rank = 1;
                foreach (ScoreDoc scoreDoc in results.ScoreDocs)
                {
                    Document CurrentDocument = _IndexSearcher.Doc(scoreDoc.Doc);
                    RankedSEDocuments.Add(new RankedSEDocument(CurrentDocument.Get(DOCUMENTID_FN).ToString(),
                        CurrentDocument.Get(TITLE_FN).ToString(),
                        CurrentDocument.Get(AUTHOR_FN).ToString(),
                        CurrentDocument.Get(BIBLIOGRAPHIC_FN).ToString(),
                        CurrentDocument.Get(ABSTRACT_FN).ToString(), 
                        rank,
                        0));

                    rank++;
                }
            }
            return RankedSEDocuments;
        }
    }
}
