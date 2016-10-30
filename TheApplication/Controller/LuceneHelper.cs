using System;
using System.Collections.Generic;
using Lucene.Net.Analysis; // for Analyser
using Lucene.Net.Documents; // for Document and Field
using Lucene.Net.Index; //for Index Writer
using Lucene.Net.Store; //for Directory
using Lucene.Net.Search; // for IndexSearcher
using TheApplication.View;

namespace TheApplication.Model
{
    public class LuceneHelper : ILuceneHelper
    {
        Directory _LuceneIndexDirectory;
        Analyzer _Analyzer;
        IndexWriter _IndexWriter;
        IndexSearcher _IndexSearcher;
        Similarity _Similarity;
        float _TitleBoostValue = 2.0F;
        List<SEDocument> _SourceCollection;

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;

        public LuceneHelper()
        {
            _LuceneIndexDirectory = null;
            _IndexWriter = null;
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
        /// Indexes a given document into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        private void IndexDocument(SEDocument document)
        {
            Field DocumentIdField = new Field(SEDocument.DOCUMENTID_FN, document.ID, Field.Store.YES, Field.Index.NO, Field.TermVector.NO);
            Field TitleField = new Field(SEDocument.TITLE_FN, document.Title, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            TitleField.Boost = _TitleBoostValue;
            Field AbstractField = new Field(SEDocument.ABSTRACT_FN, document.Abstract, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            Document doc = new Document();

            doc.Add(DocumentIdField);
            doc.Add(TitleField);
            doc.Add(AbstractField);

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

        public SearchCollectionResult SearchCollection(Query Query)
        {
            SearchCollectionResult SearchCollectionResult = new SearchCollectionResult();
            TopDocs TopDocs = _IndexSearcher.Search(Query, 1400);
            SearchCollectionResult.SearchEndDateTime = DateTime.Now;
            SearchCollectionResult.RankedResults = new List<RankedSEDocument>();

            int Rank = 1;

            foreach (ScoreDoc ScoreDoc in TopDocs.ScoreDocs)
            {
                Document Document = _IndexSearcher.Doc(ScoreDoc.Doc);
                string DocumentID = Document.Get(SEDocument.DOCUMENTID_FN).ToString();

                SEDocument CurrentDoc = _SourceCollection.Find(d => d.ID == DocumentID);
                SearchCollectionResult.RankedResults.Add(LoadRankedSEDocument(CurrentDoc, Rank, ScoreDoc.Score));

                Rank++;
            }


            return SearchCollectionResult;
        }

        private RankedSEDocument LoadRankedSEDocument(SEDocument SEDocument, int Rank, float RelevanceScore)
        {
            return new RankedSEDocument(SEDocument.ID, SEDocument.Title, SEDocument.Author, SEDocument.Bibliographic, SEDocument.Abstract, Rank, RelevanceScore);
        }
    }
}