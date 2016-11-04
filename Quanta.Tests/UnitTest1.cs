using System.Diagnostics;
using System.IO;
using CsvHelper;
using System.Linq;
using System.Text;
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
    }
}
