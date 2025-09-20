using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Handlers;

namespace MyTelegratorBot.Handlers
{
    [CallbackQueryHandler]
    [CallbackData("report_enemy")]
    public class ReportCommandHandler : CallbackQueryHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            ReportComandHandler reportCommandHandler = new ReportComandHandler();
            var keyboard = new InlineKeyboardMarkup();
            foreach (var enemy in EnemyCategories.List)
            {
                keyboard.AddButton(enemy, "enemy_" + enemy.GetHashCode());
            }
            ReportConstructorService.Instance.NewReport(container.ActualUpdate.From.Id);
            await Client.SendMessage(chatId: container.ActualUpdate.Message.Chat.Id, text: "Укажите тип угрозы:", replyMarkup: keyboard);
            return Result.Ok();
        }
    }
}
