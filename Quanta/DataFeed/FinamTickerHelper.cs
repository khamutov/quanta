using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quanta.DataFeed
{
    public class FinamTickerHelper
    {
        private static string[] ClearBrakes(string[] subject) // Просто функция для удаления лишней информации в строках JS-присваивания переменных
        {
            int brakeIndex = subject[0].IndexOf('[');
            if (brakeIndex > 0)
            {
                subject[0] = subject[0].Substring(brakeIndex + 1, subject[0].Length - brakeIndex - 1);
            }
            brakeIndex = subject[subject.Length - 1].IndexOf(']');
            if (brakeIndex > 0)
            {
                subject[subject.Length - 1] = subject[subject.Length - 1].Substring(0, brakeIndex);
            }
            return subject;
        }

        public static async Task<List<FinamTickerId>> DownloadMapping()
        {
            var downloader = new WebDownloader();

            var jsdata = await downloader.DownloadStringTaskAsync("http://www.finam.ru/cache/icharts/icharts.js");

            string[] responseVars = jsdata.Split('\n');
            string[] emitentIDs = { };
            string[] emitentCodes = { };
            string[] emitentMarkets = { };
            for (var i = 0; i < responseVars.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        emitentIDs = ClearBrakes(responseVars[i].Split(','));
                        break;
                    case 2:
                        emitentCodes = ClearBrakes(responseVars[i].Split(','));
                        break;
                    case 3:
                        emitentMarkets = ClearBrakes(responseVars[i].Split(','));
                        break;
                }
            }

            var tickerMapping = new List<FinamTickerId>();
            for (int i = 0; i < emitentIDs.Length; i++)
            {
                FinamTickerId emitent = new FinamTickerId()
                {
                    Id = emitentIDs[i],
                    Ticker = emitentCodes[i].Trim('\''),
                    Market = emitentMarkets[i]
                };
                tickerMapping.Add(emitent);
            }

            return tickerMapping;
        }
    }
}
