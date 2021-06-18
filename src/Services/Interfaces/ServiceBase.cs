using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StatusInvestScraping.Services
{
    public class ServiceBase
    {
        public  HttpClient HttpClientBase;
        public ServiceBase()
        {
            HttpClientBase = new HttpClient();
        }
        public async Task<string> RetornaHtml(string url)
        {
            var response = await HttpClientBase.GetByteArrayAsync(url);
            return Encoding.GetEncoding("ISO-8859-1").GetString(response, 0, response.Length - 1);
        }
    }
}
