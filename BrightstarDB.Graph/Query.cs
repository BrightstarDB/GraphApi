using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightstarDB.Client;

namespace BrightstarDB.Graph
{
    public class Query
    {
        private List<QueryTerm> _terms;
        private QueryResult _queryResult;
        private Context _context;
        private readonly List<Uri> _startNodes;
        private Graph _graph;

        public Query(Graph graph)
        {
            _startNodes = new List<Uri>();
            _graph = graph;
            _terms = new List<QueryTerm>();
        }

        public Query(Graph graph, Uri startNode)
        {
            _startNodes = new List<Uri> {startNode};
            _graph = graph;
            _terms = new List<QueryTerm>();
        }

        public Query(IEnumerable<Uri> startNodes)
        {
            _startNodes = new List<Uri>();
            _startNodes.AddRange(startNodes);
            _terms = new List<QueryTerm>();
        }

        public Query Traverse(string property, TraversalOptions traversaloptions = null)
        {
            _terms.Add(new TraverseQueryTerm(property, false, traversaloptions));
            return this;
        }

        public Query TraverseInverse(string property, TraversalOptions traversaloptions = null)
        {
            _terms.Add(new TraverseQueryTerm(property, true, traversaloptions));
            return this;
        }

        public Query Filter(string propertyType, string value, Operator op)
        {
            _terms.Add(new FilterQueryTerm(propertyType, value, op));
            return this;
        }

        public QueryResult Execute()
        {
            // noop at the moment
            AnalyseQuery();

            var result = new QueryResult();
            _context = new Context();

            // add the start nodes to the context
            _context.AddSet(_startNodes);

            foreach (var queryTerm in _terms)
            {
                if (queryTerm is TraverseQueryTerm)
                {
                    var traverseQueryTerm = queryTerm as TraverseQueryTerm;
                    var inputSet = _context.Latest;
                    var output = new List<Uri>();
                    foreach (var uri in inputSet)
                    {
                        string query;
                        if (traverseQueryTerm.IsInverse)
                        {
                            query = "select ?id where { ?id  <" + traverseQueryTerm.Property + "> <" + uri + "> }";
                        }
                        else
                        {
                            query = "select ?id where { <" + uri + "> <" + traverseQueryTerm.Property + "> ?id }";
                        }

                        var sparqlResult = _graph.ExecuteQuery(query);
                        foreach (var row in sparqlResult.SparqlResultRows())
                        {
                            output.Add(new Uri(row.GetColumnValue("id").ToString()));
                        }
                        _context.AddSet(output);
                    }
                } else if (queryTerm is FilterQueryTerm)
                {
                    var filterQueryTerm = queryTerm as FilterQueryTerm;
                    var inputSet = _context.Latest;
                    var output = new List<Uri>();
                    if (filterQueryTerm.Operator == Operator.Eq)
                    {
                        var query = "select ?id where { ?id <" + filterQueryTerm.Property + "> \"" + filterQueryTerm.Operand + "\" }";
                        var sparqlResult = _graph.ExecuteQuery(query);
                        foreach (var row in sparqlResult.SparqlResultRows())
                        {
                            output.Add(new Uri(row.GetColumnValue("id").ToString()));
                        }

                        var intersectedOuput = output.Intersect<Uri>(_context.Latest);
                        _context.AddSet(intersectedOuput.ToList());
                    }
                } else if (queryTerm is SelectQueryTerm)
                {
                    
                }
            }

            result.ResourceSet = _context.Latest;
            return result;
        }

        /// <summary>
        /// Here we look at what we need to keep and what we can throw away
        /// as we process the data
        /// </summary>
        private void AnalyseQuery()
        {
            // for now we do everything niavely.
        }


        /*

         //ADDED BY VIDAR:
        //retain - Allow everything to pass except what is not in the supplied collection.
         public Query Retain(string nameOf)
        {
            // add goback query term
            return this;
        }

         //ADDED BY VIDAR:
        //except - Emit everything to pass except what is in the supplied collection.
         public Query Except(string nameOf)
        {
            // add goback query term
            return this;
        }

         //ADDED BY VIDAR:
        //aggregate - Emits input, but adds input in collection, where provided closure processes input prior to insertion (greedy). In being "greedy", 'aggregate' will exhaust all the items that come to it from previous steps before emitting the next element.
         public Query Aggregate(string nameOf)
        {
            // add goback query term
            return this;
        }

         public Query As(string name)
        {
            // add a named marked to this "position"
            return this;
        }


        //ADDED BY VIDAR:
        public Query Back(int numberOfHopsToGoBack)
        {
            // add goback query term
            return this;
        }

        //ADDED BY VIDAR:
        public Query Filter(string propertyType, Query subQuery, string op) {
            // add filter query term
            return this;
        }

        //ADDED BY VIDAR:(IF POSSIBLE)
        public Query Filter(Func<Query,string,string,string> funcFilter) {
            // add filter query term
            return this;
        }

        public Query Filter(IWhereClause clause)
        {
            // add filter query term
            return this;
        }

        /// <summary>
        /// returns a table of values 
        /// </summary>
        
        //VIDAR CHANGED SIGNATURE:
        public Query Select(IEnumerable<string> properties) {
            // add select query term
            return this;
        }

        //public void Select(IEnumerable<string> properties)
        //{
        //    // add select query term
        //}

        //VIDAR CHANGED SIGNATURE:
        public Query Select(string propertylist) {
            // add select query term
            return this;
        }

        //ADDED BY VIDAR:
        public Query OrderBy(string property, bool descending) {
            // add orderby query term
            return this;
        }
         * */


        //public QueryResult Execute(List<Aggregator> aggfunctions)
        //{
        //    return null;
        //}
    }
}
