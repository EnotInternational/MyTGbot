using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotClassLibrary;

namespace MyTGbot.Commands
{
    public class GetUserInfoCommand : MyBotCommand
    {
        private readonly TelegramBotClient _botClient;
        public GetUserInfoCommand(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public override async Task Execute(Message message, UpdateType type)
        {
            Message userName = await _botClient.SendMessage(message.Chat.Id, "Enter a name of the user you want to know: ");

            _botClient.SubscribeOnMessageSingleshot((message, type) => TrySendUserInfo(message, type, message.Chat.Id));
        }
        private async Task<bool> TrySendUserInfo(Message message, UpdateType type, ChatId chatId)
        {
            if (message is not { Text: { } name } )
                return false;

            UserInfo? userInfo = await GetUserInfo(name);

            if (userInfo is null)
            {
                _ = _botClient.SendMessage(message.Chat.Id, $"Can`t find user with name {name}");
                return false;
            }
            _ = _botClient.SendMessage(message.Chat.Id, $"Name: {userInfo.Name}, tg id: {userInfo.Id}, last seen: {userInfo.lastSeen}");
            return true;
        }
        private async Task<UserInfo?> GetUserInfo(String name)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = await MyHttpClient.sharedClient.GetAsync($"api/UserInfo/name={name}");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error while getting UserInfo from API: " + ex);
                return null;
            }

            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            UserInfo? userInfo = await responseMessage.Content.ReadFromJsonAsync<UserInfo>();

            if (userInfo is not{ })
            {
                return null;
            }
            return userInfo;

        }
    
    }
}
