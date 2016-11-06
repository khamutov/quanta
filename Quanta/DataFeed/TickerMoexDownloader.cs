using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Quanta.DataFeed
{
    public class TickerMoexDownloader
    {
        private const string Url =
            @"http://www.moex.com/iss/apps/rms/rates.csv?iss.only=object&limit=unlimited&sort_column=SECID&sort_order=ASC&security_types=common_share,preferred_share&board_groups=stock_tplus,stock_ndm_tplus&index=&listname=1,2,3&collateral=0&currencyid=&lang=ru";

        public static async Task<List<Ticker>> DownloadTickers()
        {
            var downloader = new WebDownloader {Encoding = Encoding.GetEncoding(1251)};

            var donwloadedContent = await downloader.DownloadStringTaskAsync(Url);

            using (var file = new StringReader(donwloadedContent))
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

                return records;
            }
        }
    }
}
