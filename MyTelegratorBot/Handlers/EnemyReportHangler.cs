using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Attributes;
using Telegrator.Handlers;
using Telegrator.StateKeeping;

namespace MyTelegratorBot.Handlers
{
    [CallbackQueryHandler]
    [CallbackData("report_enemy")]
    public class ReportHandler : CallbackQueryHandler
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
    [CallbackQueryHandler]
    [EnemyOnlyFilter]
    public class EnemyReportHandler : CallbackQueryHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            string data = container.ActualUpdate.Data;
            data = data.Remove(0, 6);

            if(!ReportConstructorService.Instance.TryGetConstructingReport(container.ActualUpdate.From.Id, out Report report).Positive)
            {
                return Result.Fault();
            }
            else
            {
                Console.Write("Rgo");
            }

            foreach(var  enemy in EnemyCategories.List)
            {
                if(enemy.GetHashCode() == int.Parse(data))
                {
                    report.DangerType = enemy;
                }
            }
            Message message = await Responce("Опишите угрозу");
            container.ForwardEnumState<ReportGettingState>();
            //var replyMarkup = new ReplyKeyboardMarkup(new KeyboardButton("Отправить локацию") { RequestLocation = true });
            //Message locationMessage = await Responce("Укажите вашу локацию", replyMarkup: replyMarkup);
            //locationMessage.Location.
            return Result.Ok();
        }
    }

    [MessageHandler]
    [EnumState<ReportGettingState>(ReportGettingState.GettingText)]
    public class DangerDescriptionHandler : MessageHandler
    {

        public override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
    public enum ReportGettingState
    {
        None = SpecialState.NoState,
        GettingText,
        GettingAddress
    }
}
