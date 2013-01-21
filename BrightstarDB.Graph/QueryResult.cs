using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightstarDB.Graph
{
    public class QueryResult
    {
        public DataTable ResultTable { get; set; }
        public List<Uri> ResourceSet { get; set; }  
    }

    public enum QueryResultType
    {

    }
}
