using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<RankedSEDocument> SearchIndex(string QueryString, bool asis, int pageNumber)
        {
            List<string> phraseList = new List<string>();
            if (!asis)
            {
                phraseList = _IQueryParser.FindPhrase(QueryString);
                QueryString = _IQueryParser.InformationNeedParser(QueryString);
            }

            _RankedSEDocuments = _ILuceneHelper.SearchText(QueryString, phraseList,_SourceCollection, asis, pageNumber);

            return _RankedSEDocuments;
        }

        public void SaveRankedDocuments(string QueryString, bool asis)
        {
            List<RankedSEDocument> _RankedDocuments;
            List<string> phraseList = new List<string>();
            if (!asis)
            {
                phraseList = _IQueryParser.FindPhrase(QueryString);
                QueryString = _IQueryParser.InformationNeedParser(QueryString);
                
            }

            _RankedDocuments = _ILuceneHelper.SearchText(QueryString, phraseList, _SourceCollection, asis, -1);

            SaveController _SaveController = new SaveController("023", _RankedDocuments);
            SaveView _SaveView = new SaveView();

            _SaveView.SetSaveController(_SaveController);

            _SaveView.Show();
        }
    }
}
