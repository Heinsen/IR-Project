using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApplication.Model
{
    public class SearchCollectionResult
    {
        public List<RankedSEDocument> RankedResults;
        public DateTime SearchEndDateTime;
        public string ProcessedQuery;
    }
}
