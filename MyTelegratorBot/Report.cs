using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegratorBot
{
    public class Report
    {
        public long UserId { get; set; }
        public string DangerType { get; set; }
        public string Text { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Constructing;

        public enum ReportStatus {Constructing, Ready};
    }
}
