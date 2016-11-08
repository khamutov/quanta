using System;
using System.Diagnostics;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Quanta.DataFeed;
using Xunit;

namespace Quanta.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            using (TextReader textReader = File.OpenText(@"C:\projects\MTLR_790101_161101.txt"))
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.RegisterClassMap<FinamCsvRecordMap>();

                var records = csv.GetRecords<FinamCsvRecord>().ToList();

                Assert.Equal(1964, records.Count);
                Assert.Equal(274.77, 
                    MathNet.Numerics.Statistics.Statistics.MeanStandardDeviation(records.Select(i => i.ClosePrice)).Item1,
                   1);
            }
        }

        [Fact]
        public void TestDownload()
        {
            WebDownloader downloader = new WebDownloader();
        }

        [Fact]
        public async Task TestMoexDownloader()
        {
            var tickers = await TickerMoexDownloader.DownloadTickers();
            Assert.Equal(292, tickers.Count);
        }

        [Fact]
        public async Task TestFinamMapping()
        {
            var tickerMapping = await FinamTickerHelper.DownloadMapping();
            Assert.Equal("81820", tickerMapping.Single(t => t.Ticker == "ALRS" && t.Market == "1").Id);
            Assert.Equal("21018", tickerMapping.Single(t => t.Ticker == "MTLR" && t.Market == "1").Id);
            Assert.Equal("16842", tickerMapping.Single(t => t.Ticker == "GAZP" && t.Market == "1").Id);
        }

        [Fact]
        public async Task QuoteDownloadTest()
        {
            var tickerId = new FinamTickerId()
            {
                Id = "21018",
                Market = "1",
                Ticker = "MTLR"
            };
            var quotes = await FinamQuoteDownloader.DownloadQuotes(tickerId);
            Assert.Equal(1966, quotes.Count);
        }

        [Fact]
        public void TestParseMoexTickers()
        {
            using (var file = new StreamReader(@"C:\projects\rates.csv", Encoding.GetEncoding(1251)))
            {
                var inMemoryFile = new StringWriter();

                // skip first 2 lines
                file.ReadLine();
                file.ReadLine();
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim().Length == 0)
                    {
                        break;
                    }
                    inMemoryFile.WriteLine(line);
                }

                var csv = new CsvReader(new StringReader(inMemoryFile.ToString()));
                csv.Configuration.RegisterClassMap<TickerMoexCsvMap>();
                csv.Configuration.Delimiter = ";";
                var records = csv.GetRecords<Ticker>().ToList();

                Assert.Equal(292, records.Count);

                using (var textWriter = new StreamWriter(@"C:\projects\tickers.csv"))
                {
                    var csvW = new CsvWriter(textWriter);
                    csvW.WriteRecords(records);
                }
            }
        }

        [Fact]
        public void TestMongoConnection()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<BsonDocument>("bar");
            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "type", "Database" },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };
            collection.InsertOne(document);
        }
    }
}
