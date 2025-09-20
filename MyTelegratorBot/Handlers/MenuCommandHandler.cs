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
using Telegram.Bot;
using System.ComponentModel;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("start")]
    [ChatType(ChatType.Private)]
    public class MenuCommandHandler : CommandHandler
    {
        public async override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Сообщить об угрозе", "report_enemy"),
            });
            await Responce("**EnemyTraker**\r\t\nЕсли вы заметили космическую тарелку, загадочного монстра или природное явление - нажмите кнопку ниже и расскажите нам об этом. Ваше сообщение поможет администратору быстро оценить, является ли объект угрозой и принять необходимые меры. Ваша бдительность очень важна!", replyMarkup: keyboard, parseMode:ParseMode.Markdown);
            return Result.Ok();
        }
    }
    [CommandHandler]
    [CommandAllias("report")]
    [ChatType(ChatType.Private)]
    public class ReportComandHandler : CommandHandler
    {
        public async override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            var keyboard = new InlineKeyboardMarkup();
            foreach (var enemy in EnemyCategories.List)
            {
                keyboard.AddButton(enemy, "enemy_" + enemy.GetHashCode());
            }
            await Client.SendMessage(chatId: container.ActualUpdate.Chat.Id, text: "Укажите тип угрозы:", replyMarkup: keyboard);
            return Result.Ok();
        }
    }
}
