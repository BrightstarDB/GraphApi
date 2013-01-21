using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrightstarDB.Graph;

namespace BrightstarDB.GraphTests
{
    /// <summary>
    /// Summary description for GraphTests
    /// </summary>
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TestTraversal()
        {
            var g = new BrightstarDB.Graph.Graph("type=http;endpoint=http://localhost:8090/brightstar", "movielens");
            var query = new Query(g, new Uri("http://brightstardb.com/samples/movielens/user/" + 500));
            query.TraverseInverse("http://brightstardb.com/samples/movielens/model/user"); // traverse to ratings
            var result = query.Execute();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResourceSet);
            Assert.IsTrue(result.ResourceSet.Count > 0);
        }

        [TestMethod]
        public void TestFilter()
        {
            var g = new BrightstarDB.Graph.Graph("type=http;endpoint=http://localhost:8090/brightstar", "movielens");
            var query = new Query(g, new Uri("http://brightstardb.com/samples/movielens/user/" + 500));
            query.TraverseInverse("http://brightstardb.com/samples/movielens/model/user"); // traverse to ratings
            query.Filter("http://brightstardb.com/samples/movielens/model/value", "4", Operator.Eq);
            var result = query.Execute();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResourceSet);
            Assert.IsTrue(result.ResourceSet.Count > 0);
        }
    }
}
