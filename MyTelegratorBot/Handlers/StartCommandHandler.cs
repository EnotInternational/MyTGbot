using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Annotations;
using Telegrator.Handlers;
using Telegrator;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("start")]
    [ChatType(ChatType.Private)]
    public class StartCommandHandler : CommandHandler
    {
        public async override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Сообщить об угрозе", "report"),
            });
            await Responce("**EnemyTraker**\r\t\nЕсли вы заметили космическую тарелку, загадочного монстра или природное явление - нажмите кнопку ниже и расскажите нам об этом. Ваше сообщение поможет администратору быстро оценить, является ли объект угрозой и принять необходимые меры. Ваша бдительность очень важна!", replyMarkup: keyboard, parseMode:ParseMode.Markdown);
            return Result.Ok();
        }
    }
}
