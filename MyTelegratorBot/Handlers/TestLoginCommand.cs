using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using Telegram.Bot;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Handlers;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("test_login")]
    public class TestLoginCommand : CommandHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = SharedHttpClient.CookieContainer,
                AllowAutoRedirect = true
            };
            var parameters = new
            {
                email = "sobakasutulaya@egor.ru",
                password = "123"
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(parameters),
                Encoding.UTF8,
                "application/json"
            );

            var client = new HttpClient(handler) { BaseAddress = SharedHttpClient.BaseAdress };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "Your-App");

            HttpResponseMessage response = await client.PostAsync("api/login/", content);
            return Result.Ok();
        }
    }
    [CommandHandler]
    [CommandAllias("test_reg")]
    public class TestRegistrationCommand : CommandHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            var parameters = new
            {
                id = "4353534536456456",
                username = "Superegor231",
                email = "sobakasutulayrererea@egor.ru",
                password = "1234"
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(parameters),
                Encoding.UTF8,
                "application/json"
            );
            var options = new RestClientOptions("https://andreises.pythonanywhere.com")
            {
              
            };
            var tokenRequest = new RestRequest("api/telegram/auth/")
                .AddJsonBody(parameters);

            var getPositions = new RestRequest("api/posts/")
                .AddJsonBody(parameters);

            var client = new RestClient(options);
            try
            {
                RestResponse response = await client.PostAsync(tokenRequest);
                var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
                String access = obj.access;
                CookieCollection coll = response.Cookies;

                options = new RestClientOptions("https://andreises.pythonanywhere.com")
                {
                    
                };
                
                client = new RestClient(options);
                getPositions.AddHeader("Authorization", $"Bearer {access}");
                RestResponse postions = await client.GetAsync(getPositions);
                var poss = JsonConvert.DeserializeObject<dynamic>(postions.Content);
                //String access = obj.access;
            }
            catch(Exception e)
            {
                Console.WriteLine("Ex");
            }

            return Result.Ok();
        }
        private class Content
        {
            public string id { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string password { get; set; }

        }
    }
}
