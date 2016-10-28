using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Lucene.Net.Documents;
using TheApplication.Model;
using TheApplication.View;


namespace TheApplication.Controller
{
    public class SearchController
    {
        private ILuceneHelper _ILuceneHelper;
        private IQueryParser _IQueryParser;

        private List<RankedSEDocument> _RankedSEDocuments;
        private List<SEDocument> _SourceCollection;
        
        public SearchController(ILuceneHelper LuceneHelper, IQueryParser IQueryParser, List<SEDocument> _Source)
        {
            _ILuceneHelper = LuceneHelper;
            _IQueryParser = IQueryParser;
            _SourceCollection = _Source;
        }

        public SearchCollectionResult SearchIndex(string QueryString, bool PreProcess)
        {
            List<string> phraseList = new List<string>();

            Query Query = _IQueryParser.ProcessQuery(QueryString, PreProcess);
            
            SearchCollectionResult SearchCollectionResult = _ILuceneHelper.SearchCollection(Query);
            SearchCollectionResult.ProcessedQuery = Query.ToString();
            _RankedSEDocuments = SearchCollectionResult.RankedResults;

            return SearchCollectionResult;
        }
        
        public void SaveRankedDocuments(string QueryString)
        {


            SaveController _SaveController = new SaveController(GetTopicId(QueryString), _RankedSEDocuments);
            SaveView _SaveView = new SaveView();

            _SaveView.SetSaveController(_SaveController);

            _SaveView.Show();
        }

        private string GetTopicId(string QueryString)
        {
            if ((QueryString.Contains("what \"similarity laws\" must be obeyed when constructing aeroelastic models of heated high speed aircraft")))
            {
                return "1";
            }
            else if (string.Equals(QueryString, ("what are the structural and aeroelastic problems associated with flight of high speed aircraft")))
            {
                return "2";
            }
            else if (string.Equals(QueryString, ("how can the aerodynamic performance of channel flow ground effect machines be calculated")))
            {
                return "3";
            }
            else if (string.Equals(QueryString, ("in summarizing theoretical and experimental work on the behaviour of a typical aircraft structure in a noise environment is it possible to develop a design procedure")))
            {
                return "4";
            }
            else if (string.Equals(QueryString, ("has anyone developed an analysis which accurately establishes the large deflection behaviour of \"conical shells\"")))
            {
                return "5";
            }

            return "0";
        }
    }
}
