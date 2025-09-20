using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegratorBot
{
    public class SharedHttpClient
    {
        public static HttpClient Client 
        { 
            get => new HttpClient() {BaseAddress = BaseAdress}; 
        }
        public static Uri BaseAdress { get; set; } = new Uri("https://andreises.pythonanywhere.com");

    }
}
