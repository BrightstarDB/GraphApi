using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightstarDB.Graph
{
    public abstract class QueryTerm
    {
    }

    public class TraverseQueryTerm : QueryTerm
    {
        public string Property { get; set; }
        public TraversalOptions Options { get; set; }
        public bool IsInverse { get; set; }

        public TraverseQueryTerm(string property, bool isInverse, TraversalOptions options)
        {
            Property = property;
            Options = options;
            IsInverse = isInverse;
        }

        public List<Uri> Execute(List<Uri> input)
        {
            return input;
        }        
    }

    public class FilterQueryTerm : QueryTerm
    {
        public string Property { get; set; }
        public object Operand { get; set; }
        public Operator Operator { get; set; }

        public FilterQueryTerm(string property, object operand, Operator op)
        {
            Property = property;
            Operand = operand;
            Operator = op;
        }

        public List<Uri> Execute(List<Uri> input)
        {
            return input;
        } 
    }

    public class SelectQueryTerm : QueryTerm
    {
        public List<string> Properties { get; set; }

        public SelectQueryTerm(List<string> properties)
        {
            Properties = properties;
        } 
    }

}
