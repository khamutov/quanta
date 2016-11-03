using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvHelper;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;

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
                csv.Configuration.RegisterClassMap<FinamCsvRecordMap>();

                var records = csv.GetRecords<FinamCsvRecord>().ToList();

                Assert.AreEqual(1964, records.Count());
                Assert.AreEqual(274.77, 
                    MathNet.Numerics.Statistics.Statistics.MeanStandardDeviation(records.Select(i => i.ClosePrice)).Item1,
                    0.1);
            }
        }
    }
}
