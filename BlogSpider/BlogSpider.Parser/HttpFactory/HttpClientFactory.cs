using System.Net.Http;

namespace BlogSpider.Parser.HttpFactory
{
    public static class HttpClientFactory
    {
        public static HttpClient GetClient()
        {
            return new HttpClient();
        }
    }
}
