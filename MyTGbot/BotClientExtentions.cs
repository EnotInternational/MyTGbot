using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static Telegram.Bot.TelegramBotClient;

namespace MyTGbot
{
    public static class BotClientExtentions
    {
        public static void SubscribeOnMessageSingleshot(this TelegramBotClient botClient, OnMessageHandler onMessageHandler)
        {
            botClient.OnMessage += ActionWrapper;

            async Task ActionWrapper(Message message, UpdateType type)
            {
                await onMessageHandler(message, type);
                botClient.OnMessage -= ActionWrapper;
            }
        }
    }
}
