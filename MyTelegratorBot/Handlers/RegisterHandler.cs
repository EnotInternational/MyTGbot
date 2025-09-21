using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Handlers;

namespace MyTelegratorBot.Handlers
{

    [CallbackQueryHandler]
    [CallbackData("regiser")]
    [EnumState<RegisterState>(RegisterState.None)]
    public class RegisterCallbackHandler : CallbackQueryHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            await Responce("Укажите вашу электронную почту: ");
            return Result.Ok();
        }
    }
    [MessageHandler]
    [EnumState<RegisterState>(RegisterState.GettingName)]
    public class RegisterNameMessageHandler : MessageHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            await Responce("Укажите ваш пароль почту: ");
            return Result.Ok();
        }
    }
    public enum RegisterState
    {
        None = SpecialState.NoState,
        GettingName,
        GettingPassword
    }
}
