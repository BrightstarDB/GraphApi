using System;
using BrightstarDB.Graph.DataBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrightstarDB.GraphTests
{
    [TestClass]
    public class DataGenerationTests
    {
        [TestMethod]
        public void TestGenerateData()
        {
            var db = new FilmReviewDataGenerator("c:\\data\\ml-100k");
            db.CreateData();
        }
    }
}
