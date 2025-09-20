using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Handlers;
using Telegrator.StateKeeping;

namespace MyTelegratorBot.Handlers
{
    [MessageHandler]
    [EnumState<ReportGettingState>(ReportGettingState.GettingText)]
    public class DangerDescriptionMessageHandler : MessageHandler
    {

        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            if (!ReportConstructorService.Instance.TryGetConstructingReport(container.ActualUpdate.From.Id, out Report report).Positive)
            {
                return Result.Fault();
            }
            report.Text = container.ActualUpdate.Text;
            container.ForwardEnumState<ReportGettingState>();
            var replyMarkup = new ReplyKeyboardMarkup(new KeyboardButton("Отправить локацию") { RequestLocation = true });
            
            await Reply("Сообщите ваше местоположение", replyMarkup: replyMarkup);
            return Result.Ok();
        }
    }
}
