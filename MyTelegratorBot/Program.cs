// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using MyTelegratorBot;
using MyTelegratorBot.Handlers;
using Telegram.Bot;
using Telegrator;
using Telegrator.Hosting;

Console.WriteLine("Hello, World!");

TelegratorClient bot = new TelegratorClient("8392580190:AAEKxTK2kyuG5LAnJpYCT1bwVPKi88oni1g");
ReportConstructorService reportConstructorService = new(new TimeSpan(hours: 0, minutes: 1, seconds: 0), bot);

bot.Handlers.CollectHandlersAssemblyWide();
bot.StartReceiving();
Console.ReadLine();