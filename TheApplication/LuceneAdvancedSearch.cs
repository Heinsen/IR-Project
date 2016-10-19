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
        MultiFieldQueryParser mfqp;
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
            Lucene.Net.Documents.Field titleField = new Field(TITLE_FN, document.Title, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
            titleField.Boost = BoostValue;
            Lucene.Net.Documents.Field authorField = new Field(AUTHOR_FN, document.Author, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Lucene.Net.Documents.Field bibliographicField = new Field(BIBLIOGRAPHIC_FN, document.Bibliographic, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO);
            Lucene.Net.Documents.Field abstractField = new Field(ABSTRACT_FN, document.Abstract, Field.Store.NO, Field.Index.ANALYZED, Field.TermVector.YES);
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

        public void InitializeMultiFieldQueryParser(bool asis)
        {
            String[] fields = new String[] { TITLE_FN, ABSTRACT_FN };
            if (asis)
            {
                mfqp = new MultiFieldQueryParser(
                        Lucene.Net.Util.Version.LUCENE_30, fields,
                        analyzer);
            }
            else
            {
                HashMap<string, float> boosts = new HashMap<string, float>();
                boosts.Add(TITLE_FN, (float)10);
                boosts.Add(ABSTRACT_FN, (float)5);

                mfqp = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30, fields,
                analyzer
                ,boosts);

                //newSimilarity = new NewSimilarity();
            }
            mfqp.DefaultOperator = MultiFieldQueryParser.OR_OPERATOR;
        }

        /// <summary>
        /// Searches the index for the querytext
        /// </summary>
        /// <param name="querytext">The text to search the index</param>
        public List<Doc> SearchText(string querytext, List<string> phraseList, bool asis, int page)
        {

            List<Doc> resultDocs = new List<Doc>();
            if (!string.IsNullOrEmpty(querytext))
            {
                InitializeMultiFieldQueryParser(asis);

                //Query query = mfqp.Parse(querytext);

                BooleanQuery finalQuery = new BooleanQuery();
                if (asis)
                {
                    finalQuery.Add(mfqp.Parse(querytext), Occur.SHOULD);
                }
                else
                {
                    //querytext = querytext.ToLower();
                    QueryParser myQueryParser = new QueryParser();
                    finalQuery.Add(mfqp.Parse(querytext.Replace('\"', ' ')), Occur.SHOULD);
                    string[] tokens = myQueryParser.TokeniseString(querytext.Replace('\"', ' ').Replace('[', ' ').Replace(']', ' '));
                    //foreach (string term in tokens)
                    //{
                    //    finalQuery.Add(mfqp.Parse(term), Occur.SHOULD);
                    //}
                    //finalQuery.MinimumNumberShouldMatch = (int)(tokens.Count() / 4);

                    foreach (string phrase in phraseList)
                    {
                        finalQuery.Add(mfqp.Parse("\"" + phrase + "\""), Occur.MUST);
                        mfqp.PhraseSlop = 2;
                        //string[] phraseParts = phrase.Split(' ');
                        //phraseQuery = new PhraseQuery();
                        //foreach (string p in phraseParts)
                        //{
                        //    phraseQuery.Add(new Term(ABSTRACT_FN, p));
                        //}
                        //phraseQuery.Boost = BoostValue;
                        //phraseQuery.Slop = 3;
                        //finalQuery.Add(phraseQuery, Occur.SHOULD);
                    }
                }
                TopDocs results = searcher.Search(finalQuery, (page + 1) * 10);
                int rank = 0;
                for (int i = page * 10; i < (page + 1) * 10 && i < results.ScoreDocs.Length; i++)
                {
                    rank++;
                    //Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
                    Document doc = searcher.Doc(i);
                    string documentId = doc.Get(DOCUMENTID_FN).ToString();
                    Doc currentDoc = documentColl.Where(d => d.DocId == documentId).FirstOrDefault();
                    if (currentDoc != null)
                    {
                        resultDocs.Add(new Doc
                        {
                            DocId = documentId,
                            Title = !string.IsNullOrEmpty(currentDoc.Title) ? currentDoc.Title : string.Empty,
                            Author = !string.IsNullOrEmpty(currentDoc.Author) ? currentDoc.Author : string.Empty,
                            Bibliographic = !string.IsNullOrEmpty(currentDoc.Bibliographic) ? currentDoc.Bibliographic : string.Empty,
                            Abstract = !string.IsNullOrEmpty(currentDoc.Abstract) ? currentDoc.Abstract : string.Empty,

                        }
                                               );
                    }
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
