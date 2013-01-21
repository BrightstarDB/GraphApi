using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BrightstarDB.Client;

namespace BrightstarDB.Graph
{
    /// <summary>
    /// A read only graph view of a triple store
    /// </summary>
    public class Graph
    {
        private string _connectionString;
        private readonly IBrightstarService _client;
        private string _storeName;

        public Graph(string connectionString, string storeName)
        {
            _client = BrightstarService.GetClient(connectionString);
        }

        public IBrightstarService Client { get { return _client; } }

        public XDocument ExecuteQuery(string expression)
        {
            var dataStream = _client.ExecuteQuery(_storeName, expression);
            return XDocument.Load(dataStream);
        }
    }
}
