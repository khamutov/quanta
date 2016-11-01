using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvHelper;
using System.Linq;
using System.Diagnostics;

namespace Quanta.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (TextReader textReader = File.OpenText(@"C:\projects\MTLR_790101_161101.txt"))
            {
                var csv = new CsvReader(textReader);
                var records = csv.GetRecords<Object>();

                Trace.WriteLine(records.First());
                Assert.AreEqual(1964, records.Count());
            }
        }
    }
}
