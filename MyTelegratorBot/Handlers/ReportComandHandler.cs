using Telegram.Bot.Types.Enums;
using Telegrator.Annotations;
using Telegrator.Handlers;
using Telegrator;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("report")]
    [ChatType(ChatType.Private)]
    public class ReportComandHandler : CommandHandler
    {
        public async override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            ReportComandHandler reportCommandHandler = new ReportComandHandler();
            var keyboard = new InlineKeyboardMarkup();
            foreach (var enemy in EnemyCategories.List)
            {
                keyboard.AddButton(enemy, "enemy_" + enemy.GetHashCode());
            }
            ReportConstructorService.Instance.NewReport(container.ActualUpdate.From.Id);
            await Client.SendMessage(chatId: container.ActualUpdate.Chat.Id, text: "Укажите тип угрозы:", replyMarkup: keyboard);
            return Result.Ok();
        }
    }
}
