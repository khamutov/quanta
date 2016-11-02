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

                List<double> closePrices = new List<double>();
                while (csv.Read())
                {
                    var ticker = csv.GetField<string>("<TICKER>");
                    var period = csv.GetField<string>("<PER>");
                    var boolField = csv.GetField<string>("<DATE>");
                    var time = csv.GetField<string>("<TIME>");
                    var openPrice = Double.Parse(csv.GetField<string>("<OPEN>"), CultureInfo.InvariantCulture);
                    var highPrice = Double.Parse(csv.GetField<string>("<HIGH>"), CultureInfo.InvariantCulture);
                    var lowPrice = Double.Parse(csv.GetField<string>("<LOW>"), CultureInfo.InvariantCulture);
                    var closePrice = Double.Parse(csv.GetField<string>("<CLOSE>"), CultureInfo.InvariantCulture);
                    var volume = Double.Parse(csv.GetField<string>("<VOL>"), CultureInfo.InvariantCulture);

                    closePrices.Add(closePrice);
                }

                Assert.AreEqual(1964, closePrices.Count());

                Assert.AreEqual(274.77, 
                    MathNet.Numerics.Statistics.Statistics.MeanStandardDeviation(closePrices).Item1,
                    0.1);
            }
        }
    }
}
