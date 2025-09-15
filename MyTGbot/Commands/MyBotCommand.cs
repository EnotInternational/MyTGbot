using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyTGbot
{
    public abstract class MyBotCommand : BotCommand
    {
        public abstract Task Execute(Message message, UpdateType type);
    }
}
