using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegrator;
using Telegrator.Annotations;
using Telegrator.Annotations.StateKeeping;
using Telegrator.Handlers;
using Telegrator.StateKeeping;

namespace MyTelegratorBot.Handlers
{
    [CommandHandler]
    [CommandAllias("register me")]
    [ChatType(ChatType.Private)]
    [EnumState<RegistrationState>(RegistrationState.Start)]
    public class RegisterMeHandler : CommandHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {
            container.ForwardEnumState<RegistrationState>();
            return Result.Ok();
        }
    }
    [MessageHandler]
    [ChatType(ChatType.Private)]
    [EnumState<RegistrationState>(RegistrationState.GettingName)]
    public class GetNameHandler : CommandHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<Message> container, CancellationToken cancellation)
        {

            return Result.Ok();
        }
    }
    public enum RegistrationState
    {
        Start = SpecialState.NoState,
        GettingName
    }
}
