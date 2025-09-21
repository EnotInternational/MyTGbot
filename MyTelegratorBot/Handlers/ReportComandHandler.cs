using Telegram.Bot.Types.Enums;
using Telegrator.Annotations;
using Telegrator.Handlers;
using Telegrator;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Newtonsoft.Json;
using RestSharp;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("report")]
    [ChatType(ChatType.Private)]
    public class ReportComandHandler : CommandHandler
    {
        public async override Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            return await Execute(container.ActualUpdate.From.Id, Client, cancellation);
        }
        public static async Task<Result> Execute(long id, ITelegramBotClient Client ,CancellationToken cancellation)
        {
            ReportComandHandler reportCommandHandler = new ReportComandHandler();
            var parameters = new
            {
                telegram_id = "4353534536456456"
            };

            var options = new RestClientOptions("https://andreises.pythonanywhere.com");

            var tokenRequest = new RestRequest("api/telegram/refresh/")
                .AddJsonBody(parameters);

            var client = new RestClient(options);

            RestResponse response;
            try
            {
                response = await client.PostAsync(tokenRequest);
            }
            catch (Exception ex)
            {
                await Client.SendMessage(chatId: id, text: "Сначала зарегистрируйтесь");
                return Result.Fault();
            }

            var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
            String access = obj.access;

            var getDangersReport = new RestRequest("api/targets/");

            getDangersReport.AddHeader("Authorization", $"Bearer {access}");

            RestResponse dangers;
            try
            {
                dangers = await client.GetAsync(getDangersReport);
                var dangerlist = JsonConvert.DeserializeObject<dynamic>(dangers.Content);
                foreach (var danger in dangerlist)
                {
                    string f = danger.name;
                    //var parsedDanger = JsonConvert.DeserializeObject<dynamic>(danger.name);
                    EnemyCategories.List.Add(f);
                }
            }
            catch (Exception ex)
            {
                //await Client.SendMessage(chatId: container.ActualUpdate.Chat.Id, text: "Сначала зарегистрируйтесь");
                return Result.Fault();
            }
            var keyboard = new InlineKeyboardMarkup();
            foreach (var enemy in EnemyCategories.List)
            {
                keyboard.AddButton(enemy, "enemy_" + enemy.GetHashCode());
            }

            ReportConstructorService.Instance.NewReport(id);
            await Client.SendMessage(chatId: id, text: "Укажите тип угрозы:", replyMarkup: keyboard);
            
            
            return Result.Ok();
        }
    }

    [CallbackQueryHandler]
    [CallbackData("report")]
    public class ReportCallbackHandler : CallbackQueryHandler
    {
        public override Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            return ReportComandHandler.Execute(container.ActualUpdate.From.Id, Client, cancellation);
        }
    }
}
