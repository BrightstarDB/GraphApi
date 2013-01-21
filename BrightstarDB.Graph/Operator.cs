using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightstarDB.Graph
{
    public enum Operator
    {
        Eq, // Equals
        Gt, // GREATER THAN
        Lt, // LESS THAN
        In, // IN COLLECTION
        Contains, 
        StartsWith
    }
}
