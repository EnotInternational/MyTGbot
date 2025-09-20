using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegrator;

namespace MyTelegratorBot
{
    public class ReportConstructorService
    {
        public static ReportConstructorService Instance { get; private set; }
        private List<Report> reports = new();
        public ReportConstructorService() 
        {
            Instance = this;
        }
        public Report NewReport(long userId)
        { 
            Report report = new Report() { UserId = userId};
            reports.Add(report);
            return report;
        }
        public Result TryGetConstructingReport(long userId, out Report report)
        {
            report = reports.Where(rep => rep.UserId == userId && rep.Status == Report.ReportStatus.Constructing).First();
            if (report == null)
            {
                return Result.Fault();
            }
            return Result.Ok();
        }
    }
}
