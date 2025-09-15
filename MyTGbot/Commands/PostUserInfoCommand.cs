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
    public class PostUserInfoCommand : MyBotCommand
    {
        private readonly TelegramBotClient _botClient;
        public PostUserInfoCommand(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public override async Task Execute(Message message, UpdateType type)
        {
            Message userName = await _botClient.SendMessage(message.Chat.Id, "Enter your name: ");

            _botClient.SubscribeOnMessageSingleshot((message, type) => TryRecieveUserInfo(message, type, message.Chat.Id));
        }

        private async Task<bool> TryRecieveUserInfo(Message message, UpdateType type, ChatId chatId)
        {
            if (message is not { Text: { } name })
                return false;

            if (type is not UpdateType.Message)
                return false;

            UserInfo user = new UserInfo() {Name = name, lastSeen = DateTime.Now, Id = chatId.Identifier.Value};

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await MyHttpClient.sharedClient.PostAsync($"api/UserInfo", JsonContent.Create<UserInfo>(user));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while postion UserInfo to API: " + ex);
                return false;
            }

            if (!responseMessage.IsSuccessStatusCode)
            {
                _ = _botClient.SendMessage(message.Chat.Id, $"Registrations is impossible now");
                return false;
            }
            _ = _botClient.SendMessage(message.Chat.Id, $"You have been registered");
            return true;
        }
    }
}
