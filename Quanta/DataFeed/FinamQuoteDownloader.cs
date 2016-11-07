using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Quanta.DataFeed
{
    public class FinamQuoteDownloader
    {
        private const string Url =
            "http://export.finam.ru/MTLR_790101_161101.txt?market=1&em=21018&code=MTLR&apply=0&df=1&mf=0&yf=1979&from=01.01.1979&dt=1&mt=10&yt=2016&to=01.11.2016&p=8&f=MTLR_790101_161101&e=.txt&cn=MTLR&dtf=1&tmf=1&MSOR=1&mstime=on&mstimever=1&sep=1&sep2=1&datf=1&at=1";


        public static async Task<List<FinamCsvRecord>>  DownloadQuotes(FinamTickerId ticker)
        {
            var dateFrom = new DateTime(1970, 1, 1);
            var dateTo = DateTime.Now.AddDays(-1);

            var url = string.Format(
                "http://export.finam.ru/quote.txt?" +
                "market={5}&em={4}&code={3}&apply=0&" +
                "df={0}&mf={1}&yf={2}&from={9:dd.mm.yy}&" +
                "dt={6}&mt={7}&yt={8}&to={10:dd.mm.yy}&" +
                "p=8&f=quote&e=.txt&cn={3}&dtf=1&tmf=1&" +
                "MSOR=1&mstime=on&mstimever=1&sep=1&sep2=1&datf=1&at=1",
                dateFrom.Day,
                dateFrom.Month - 1,
                dateFrom.Year,
                ticker.Ticker,
                ticker.Id,
                ticker.Market,
                dateTo.Day,
                dateTo.Month - 1,
                dateTo.Year, 
                dateFrom,
                dateTo);

            var downloader = new WebDownloader();
            var content = await downloader.DownloadStringTaskAsync(url);

            var csv = new CsvReader(new StringReader(content));
            csv.Configuration.RegisterClassMap<FinamCsvRecordMap>();

            var quotes = csv.GetRecords<FinamCsvRecord>().ToList();

            return quotes;
        }
    }
}
