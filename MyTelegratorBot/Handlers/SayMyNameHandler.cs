using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Attributes;
using Telegrator.Handlers;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("say_my_name")]
    [ChatType(ChatType.Private)]
    public class SayMyNameHandler : CommandHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            User user = container.ActualUpdate.From;
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("You`re god damn right", "youre_gd_right"),
            });
            await Responce(user.FirstName, replyMarkup: keyboard);
            return Result.Ok();
        }
    }
    [CallbackQueryHandler]
    [CallbackData("youre_gd_right")]
    [ChatType(ChatType.Private)]
    public class GodDamnRightCallbackHandler : CallbackQueryHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            ChatId chatId = container.ActualUpdate.Message.Chat.Id;
            await Client.SendVideo(chatId, "https://tenor.com/ru/view/damn-right-breaking-bad-gif-14029087");
            return Result.Ok();
        }
    }
}
