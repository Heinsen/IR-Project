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

namespace TheApplication
{
    class LuceneAdvancedSearch
    {
        Lucene.Net.Store.Directory luceneIndexDirectory;
        Lucene.Net.Analysis.Analyzer analyzer;
        Lucene.Net.Index.IndexWriter writer;
        IndexSearcher searcher;
        MultiFieldQueryParser parser;
        Similarity newSimilarity;
        float BoostValue = 2; 

        const Lucene.Net.Util.Version VERSION = Lucene.Net.Util.Version.LUCENE_30;
        const string DOCUMENTID_FN = "DocumentId";
        const string TITLE_FN = "Title";
        const string AUTHOR_FN = "Author";
        const string BIBLIOGRAPHIC_FN = "Bibliographic";
        const string ABSTRACT_FN = "Abstract";
        static List<Doc> documentColl = new List<Doc>();


        public LuceneAdvancedSearch()
        {
            luceneIndexDirectory = null;
            writer = null;
            analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            String[] fields = new String[] { TITLE_FN, ABSTRACT_FN };
            IDictionary<String, float> boosts = new Dictionary<String, float>();
            boosts.Add(TITLE_FN, BoostValue);
            parser = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30,
                fields,
                analyzer
                //,
                //boosts
            );
            
            //parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[] {TITLE_FN, ABSTRACT_FN}, analyzer);
            
            //newSimilarity = new NewSimilarity(); 
        }

        /// <summary>
        /// Creates the index at a given path
        /// </summary>
        /// <param name="indexPath">The pathname to create the index</param>
        public void CreateIndex(string indexPath)
        {
            luceneIndexDirectory = Lucene.Net.Store.FSDirectory.Open(indexPath);
            IndexWriter.MaxFieldLength mfl = new IndexWriter.MaxFieldLength(IndexWriter.DEFAULT_MAX_FIELD_LENGTH);
            writer = new Lucene.Net.Index.IndexWriter(luceneIndexDirectory, analyzer, true, mfl);
            //writer.SetSimilarity(newSimilarity); 
        }

        /// <summary>
        /// Indexes a given string into the index
        /// </summary>
        /// <param name="text">The text to index</param>
        public void IndexDocument(Doc document)
        {
            Lucene.Net.Documents.Field documentIdField = new Field(DOCUMENTID_FN, document.DocId, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Lucene.Net.Documents.Field titleField = new Field(TITLE_FN, document.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
            titleField.Boost = BoostValue;
            Lucene.Net.Documents.Field authorField = new Field(AUTHOR_FN, document.Author, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
            Lucene.Net.Documents.Field bibliographicField = new Field(BIBLIOGRAPHIC_FN, document.Bibliographic, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
            Lucene.Net.Documents.Field abstractField = new Field(ABSTRACT_FN, document.Abstract, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES);
            Lucene.Net.Documents.Document doc = new Document();
            doc.Add(documentIdField);
            doc.Add(titleField);
            doc.Add(authorField);
            doc.Add(bibliographicField);
            doc.Add(abstractField);
            writer.AddDocument(doc);
        }

        /// <summary>
        /// Flushes the buffer and closes the index
        /// </summary>
        public void CleanUpIndexer()
        {
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }


        /// <summary>
        /// Creates the searcher object
        /// </summary>
        public void CreateSearcher()
        {
            searcher = new IndexSearcher(luceneIndexDirectory);
            //searcher.Similarity = newSimilarity; 
        }

        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public List<Doc> SearchText(string querytext)
        {
            List<Doc> resultDocs = new List<Doc>();
            if (!string.IsNullOrEmpty(querytext))
            {
                querytext = querytext.ToLower();
                Query query = parser.Parse(querytext);

                TopDocs results = searcher.Search(query, 100);
                int rank = 0;
                foreach (ScoreDoc scoreDoc in results.ScoreDocs)
                {
                    rank++;
                    Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                    resultDocs.Add(new Doc
                    {
                        DocId = doc.Get(DOCUMENTID_FN).ToString(),
                        Title = doc.Get(TITLE_FN).ToString(),
                        Author = doc.Get(AUTHOR_FN).ToString(),
                        Bibliographic = doc.Get(BIBLIOGRAPHIC_FN).ToString(),
                        Abstract = doc.Get(ABSTRACT_FN).ToString()
                    }
                                           );
                }
            }
            return resultDocs;
        }

        /// <summary>
        /// Closes the index after searching
        /// </summary>
        public void CleanUpSearcher()
        {
            searcher.Dispose();
        }

        public static List<Doc> GetAllDocuments(String path)
        {
            WalkDirectoryTree(path);
            return documentColl;
        }

        #region File I/O
        public static void WalkDirectoryTree(String path)
        {
            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder 
            try
            {
                files = root.GetFiles("*.*");
            }

            catch (UnauthorizedAccessException e)
            {
                System.Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    string name = fi.FullName;
                    BuildDocumetCollection(name);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    string name = dirInfo.FullName;
                    WalkDirectoryTree(name);
                }
            }
        }

        static void BuildDocumetCollection(string name)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(name);
            string text = reader.ReadToEnd().Replace("\n", " ");
            int docIdIndex = text.IndexOf(".I");
            int titleIndex = text.IndexOf(".T");
            int authorIndex = text.IndexOf(".A");
            int bibIndex = text.IndexOf(".B");
            int absIndex = text.IndexOf(".W");

            Doc document = new Doc();
            document.DocId = text.Substring(docIdIndex + 2, titleIndex - docIdIndex - 2).Trim();
            document.Title = text.Substring(titleIndex + 2, authorIndex - titleIndex - 2).Trim();
            document.Author = text.Substring(authorIndex + 2, bibIndex - authorIndex - 2).Trim();
            document.Bibliographic = text.Substring(bibIndex + 2, absIndex - bibIndex - 2).Trim();
            document.Abstract = text.Substring(absIndex + 2, text.Length - absIndex - 2).Trim();

            //Remove Repeated Title from Abstract
            document.Abstract = document.Abstract.Remove(0, document.Title.Count()).Trim();

            documentColl.Add(document);
        }

        public List<string> BindInformationNeedDropDown()
        {
            List<string> infList = new List<string>();
            infList.Add("what \"similarity laws\" must be obeyed when constructing aeroelastic models of heated high speed aircraft .");
            infList.Add("what are the structural and aeroelastic problems associated with flight of high speed aircraft .");
            infList.Add("how can the aerodynamic performance of channel flow ground effect machines be calculated .");
            infList.Add("in summarizing theoretical and experimental work on the behaviour of a typical aircraft structure in a noise environment is it possible to develop a design procedure .");
            infList.Add("has anyone developed an analysis which accurately establishes the large deflection behaviour of \"conical shells\" .");

            return infList;
        }
        #endregion 
    }
}
