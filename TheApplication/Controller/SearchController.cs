using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApplication.Model;

namespace TheApplication.Controller
{
    public class SearchController
    {
        private LuceneHelper _LuceneHelper;
        private QueryParser _QueryParser = new QueryParser();
        private List<RankedSEDocument> _RankedSEDocuments;

        public SearchController(LuceneHelper LuceneHelper)
        {
            _LuceneHelper = LuceneHelper;
        }

        private string Preprocess(string QueryString)
        {
            return _QueryParser.InformationNeedParser(QueryString) + string.Join(" ", _QueryParser.FindPhrases(QueryString));
        }

        public List<RankedSEDocument> SearchIndex(string QueryString, bool Preprocess)
        {
            if(Preprocess)
            {
                QueryString = this.Preprocess(QueryString);
            }

            _RankedSEDocuments = _LuceneHelper.SearchText(QueryString);

            return _RankedSEDocuments;
        }

        public void SaveRankedDocuments()
        {

        }
    }
}
