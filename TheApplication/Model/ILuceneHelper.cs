using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using TheApplication.View;

namespace TheApplication.Model
{
    public interface ILuceneHelper
    {
        void CreateIndex(List<SEDocument> SEDocuments, string IndexPath);
    
        SearchCollectionResult SearchCollection(Query Query);
    }
}
