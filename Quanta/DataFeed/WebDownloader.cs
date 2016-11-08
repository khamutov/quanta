using System;
using System.Net;

namespace Quanta.DataFeed
{
    public class WebDownloader : WebClient
    {
        public int Timeout { get; set; }

        public WebDownloader()
        {
            Timeout = 60000;
        }

        public WebDownloader(int timeout)
        {
            Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            request.Timeout = Timeout;
            return request;
        }
    }
}
