using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public Report NewReport(long userId)
        { 
            Report report = new Report() { UserId = userId, DateTime = DateTime.Now};
            _reports.Add(report);
            return report;
        }
        public Result TryGetConstructingReport(long userId, out Report report)
        {
            report = _reports.Where(rep => rep.UserId == userId).First();
            if (report == null)
            {
                return Result.Fault();
            }
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

            //TODO send report

            return Result.Ok();
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
