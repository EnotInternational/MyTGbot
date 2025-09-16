// See https://aka.ms/new-console-template for more information
using MyTelegratorBot.Handlers;
using Telegrator;

Console.WriteLine("Hello, World!");

const string BOT_TOKEN = "7605212970:AAEZVjg-CFwlpVaLlkf4bH08p6dl3vx8Pdc";

var bot = new TelegratorClient(BOT_TOKEN);

bot.Handlers.CollectHandlersDomainWide();
bot.StartReceiving();

Console.ReadLine();