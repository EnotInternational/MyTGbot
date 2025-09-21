using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegratorBot
{
    public class SharedHttpClient
    {
        public static HttpClient Client 
        {
            get => client;
        }
        public static readonly CookieContainer CookieContainer = new CookieContainer();
        private static HttpClient client;
        static SharedHttpClient()
        {
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true
            };
            client = new HttpClient(handler) { BaseAddress = BaseAdress };
        }
        public static Uri BaseAdress { get; set; } = new Uri("https://andreises.pythonanywhere.com");

    }
}
