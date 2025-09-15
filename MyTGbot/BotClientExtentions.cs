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
                if (type != UpdateType.Message)
                    return;

                if (message is not { Text: { } })
                    return;

                await onMessageHandler(message, type);
                botClient.OnMessage -= ActionWrapper;
            }
        }
        public static void SubscribeOnMessageSingleshot(this TelegramBotClient botClient, OnMessageHandler onMessageHandler, ChatId chatId)
        {
            botClient.OnMessage += ActionWrapper;

            async Task ActionWrapper(Message message, UpdateType type)
            {
                if (type != UpdateType.Message)
                    return;

                if (message is not { Text: { } })
                    return;

                if (message.Chat.Id != chatId)
                    return;

                await onMessageHandler(message, type);
                botClient.OnMessage -= ActionWrapper;
            }
        }
    }
}
