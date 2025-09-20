using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Handlers;
using Telegrator.StateKeeping;

namespace MyTelegratorBot.Handlers
{
    [MessageHandler]
    [EnumState<ReportGettingState>(ReportGettingState.GettingAddress)]
    public class LocationMessageHandler : MessageHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            if (!ReportConstructorService.Instance.TryGetConstructingReport(container.ActualUpdate.From.Id, out Report report).Positive)
            {
                return Result.Fault();
            }
            Location location = container.ActualUpdate.Location;
            if (location == null)
            {
                return Result.Fault();
            }
            report.Latitude = location.Latitude;
            report.Longitude = location.Longitude;
               
            container.SetEnumState<ReportGettingState>(ReportGettingState.GettingText);

            ReportConstructorService.Instance.CompleteAndSendReport(container.ActualUpdate.From.Id);
            await Responce("Ваше сообщение доставлено. Спасибо за сотрудничество", replyMarkup: new ReplyKeyboardRemove());
            return Result.Ok();
        }
    }
}
