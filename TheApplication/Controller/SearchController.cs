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
        private ILuceneHelper _ILuceneHelper;
        private IQueryParser _IQueryParser;
        private List<RankedSEDocument> _RankedSEDocuments;

        public SearchController(ILuceneHelper LuceneHelper, IQueryParser IQueryParser)
        {
            _ILuceneHelper = LuceneHelper;
            _IQueryParser = IQueryParser;
        }

        public List<RankedSEDocument> SearchIndex(string QueryString, bool Preprocess)
        {
            if(Preprocess)
            {
                QueryString = _IQueryParser.InformationNeedParser(QueryString);
            }

            _RankedSEDocuments = _ILuceneHelper.SearchText(QueryString);

            return _RankedSEDocuments;
        }

        public void SaveRankedDocuments()
        {

        }
    }
}
