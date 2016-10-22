using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheApplication.Model
{
    public interface ILuceneHelper
    {
        void CreateIndex(List<SEDocument> SEDocuments, string IndexPath);

        List<RankedSEDocument> SearchText(string QueryString, List<string> phraseList, List<SEDocument> _SourceCollection, bool asis, int page);

    }
}
