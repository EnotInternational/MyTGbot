using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Attributes;

namespace MyTelegratorBot.Handlers
{
    public enum ReportGettingState
    {
        None = SpecialState.NoState,
        GettingText,
        GettingAddress
    }
}
