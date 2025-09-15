using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTGbot
{
    public class MyHttpClient
    {
        public static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:5000"),
        };

    }
}
