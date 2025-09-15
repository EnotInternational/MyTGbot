using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTGbot
{
    public class CommandMapper
    {
        private TelegramBotClient _bot;
        private Dictionary<String, MyBotCommand> _commands = new();
        public CommandMapper(TelegramBotClient bot)
        {
            _bot = bot;
            bot.OnMessage += HandleMessage;
            bot.OnUpdate += OnUpdate;
        }
        private async Task HandleMessage(Telegram.Bot.Types.Message message, Telegram.Bot.Types.Enums.UpdateType type)
        {
            if (message is not { Text: { } })
                return;

            if (!message.Text.StartsWith("/"))
                return;

            if (!_commands.TryGetValue(message.Text, out var command))
                return;

            await command.Execute(message, type);
        }
        public void RegisterCommand(MyBotCommand command)
        {
            _commands.Add(command.Command, command);
        }

        public async void CompleteRegistration()
        {
            await _bot.SetMyCommands(_commands.Values);
        }
        async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
            }
        }
    }
}
