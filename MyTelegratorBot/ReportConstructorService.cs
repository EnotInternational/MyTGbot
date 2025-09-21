using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegrator;

namespace MyTelegratorBot
{
    public class ReportConstructorService
    {
        public static ReportConstructorService Instance { get; private set; }
        private List<Report> _reports = new();
        private TimeSpan _reportLifetime;
        private TimeSpan CleanupPeriod { get; set; } = new TimeSpan(hours: 0, minutes: 0, seconds: 30);
        private TelegratorClient _bot;
        private Timer _periodicReportsCleanupTimer;
        public ReportConstructorService(TimeSpan reportLifetime, TelegratorClient bot) 
        {
            _reportLifetime = reportLifetime;
            _bot = bot;
            Instance = this;

            _periodicReportsCleanupTimer = new Timer(RemoveOutdatedReports, null, CleanupPeriod, CleanupPeriod);

            //_ = SendReport(new Report { 
            //    DangerType = "ЕГор2",
            //    DateTime = DateTime.Now,
            //    Longitude = 66.66,
            //    Latitude = 55.33
            //});
        }

        public Report NewReport(long userId)
        { 
            Report report = new Report() { UserId = userId, DateTime = DateTime.Now};
            _reports.Add(report);
            return report;
        }
        public Result TryGet(long userId, out Report report)
        {
            IEnumerable<Report> filtered = _reports.Where(rep => rep.UserId == userId);
            if(filtered.Count() == 0)
            {
                report = null;
                return Result.Fault();
            }
            report = filtered.First();
            return Result.Ok();
        }
        public Result CompleteAndSendReport(long userId)
        {
            var report = _reports.Where(rep => rep.UserId == userId).First();
            if (report == null)
            {
                return Result.Fault();
            }

            _reports.Remove(report);

            _ = SendReport(report);
            //TODO send report

            return Result.Ok();
        }
        private async Task SendReport(Report report)
        {
            
            var parameters = new
            {
                telegram_id = "4353534536456456"
            };

            var options = new RestClientOptions("https://andreises.pythonanywhere.com");

            var tokenRequest = new RestRequest("api/telegram/refresh/")
                .AddJsonBody(parameters);

            var client = new RestClient(options);
            try
            {
                RestResponse response = await client.PostAsync(tokenRequest);
                var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
                String access = obj.access;

                var postParameters = new
                {
                    name = report.DangerType.ToString(),
                    address = "Не придумал",
                    time = report.DateTime.ToString(),
                    latitude = report.Latitude,
                    longitude = report.Longitude,
                };

                var postReport = new RestRequest("api/positions/")
                    .AddJsonBody(postParameters);

                postReport.AddHeader("Authorization", $"Bearer {access}");
                RestResponse postions = await client.PostAsync(postReport);
                //var poss = JsonConvert.DeserializeObject<dynamic>(postions.Content);
                //String access = obj.access;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ex");
            }
        }
        public void RemoveOutdatedReports(object? state)
        {
            for (int i = _reports.Count-1; i >= 0; i--)
            {
                Report report = _reports[i];

                TimeSpan span = DateTime.Now.Subtract(report.DateTime);
                if (span < _reportLifetime)
                    continue;

                _reports.Remove(report);
                _bot.SendMessage(report.UserId, "Ваш отчёт был сброшен из-за истечения выделенного на заполнение времени");
            }
        }
    }
}
