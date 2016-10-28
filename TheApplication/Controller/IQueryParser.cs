using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Search;

namespace TheApplication.Controller
{
    public interface IQueryParser
    {
        Query ProcessQuery(string Query, bool PreProcess);
    }
}
