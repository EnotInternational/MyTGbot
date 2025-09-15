using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotClassLibrary;

namespace MyTGbot.Commands
{
    public class SayMyNameCommand : MyBotCommand
    {
        private readonly TelegramBotClient _botClient;
        public SayMyNameCommand(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public override async Task Execute(Message message, UpdateType type)
        {
            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await MyHttpClient.sharedClient.GetAsync("api/UserInfo/id=" + message.Chat.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while postion UserInfo to API: " + ex);
                _ = _botClient.SendMessage(message.Chat.Id, $"Server is dead");
                return;
            }

            if (!responseMessage.IsSuccessStatusCode)
            {
                _ = _botClient.SendMessage(message.Chat.Id, $"I don`t know you");
                return;
            }

            UserInfo? userInfo = await responseMessage.Content.ReadFromJsonAsync<UserInfo>();

            if (userInfo is not { })
            {
                _ = _botClient.SendMessage(message.Chat.Id, $"I don`t know you");
                return;
            }

            _ = _botClient.SendMessage(message.Chat.Id, $"{userInfo.Name}");

            _botClient.SubscribeOnMessageSingleshot(AnswerWithGif,message.Chat.Id);
        }
        private async Task AnswerWithGif(Message message, UpdateType type)
        {
            if(message.Text.Equals("You`re god damn right", StringComparison.OrdinalIgnoreCase))
            {
                _ = _botClient.SendVideo(message.Chat.Id, "https://tenor.com/ru/view/damn-right-breaking-bad-gif-14029087");
            }
        }
    }
}
