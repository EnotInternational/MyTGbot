using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyTGbot;
using MyTGbot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

const string BOT_TOKEN = "7605212970:AAEZVjg-CFwlpVaLlkf4bH08p6dl3vx8Pdc";

using var cts = new CancellationTokenSource();
TelegramBotClient bot = new TelegramBotClient(BOT_TOKEN, cancellationToken:cts.Token);
Telegram.Bot.Types.User me = await bot.GetMe();

IServiceCollection serviceDescriptors = new ServiceCollection()
    .AddSingleton<TelegramBotClient>(bot)
    .AddSingleton<CommandMapper>()
    ;

ServiceProvider provider = serviceDescriptors.BuildServiceProvider();

CommandMapper commandMapper = provider.GetService<CommandMapper>();

commandMapper.RegisterCommand(new GetUserInfoCommand(bot) 
{ Command = "/get_user_info", Description = "gets given user info" });

commandMapper.RegisterCommand(new PostUserInfoCommand(bot)
{ Command = "/register_me", Description = "registers you in the satan`s hell database" });
//commandMapper.RegisterCommand(new GetUserInfoCommand(commandMapper, bot) { Command = "/getUserInfo", Description = "gets given user info" });

commandMapper.CompleteRegistration();

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();

cts.Cancel();

