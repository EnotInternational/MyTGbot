using Telegram.Bot.Types;
using Telegrator;
using Telegrator.Handlers;
using Telegrator.StateKeeping;

namespace MyTelegratorBot.Handlers
{
    [CallbackQueryHandler]
    [EnemyOnlyFilter]
    public class EnemyTypeCallbackHandler : CallbackQueryHandler
    {
        public override async Task<Result> Execute(IAbstractHandlerContainer<CallbackQuery> container, CancellationToken cancellation)
        {
            string data = container.ActualUpdate.Data;
            data = data.Remove(0, 6);

            if(!ReportConstructorService.Instance.TryGetConstructingReport(container.ActualUpdate.From.Id, out Report report).Positive)
            {
                return Result.Fault();
            }
            foreach(var  enemy in EnemyCategories.List)
            {
                if(enemy.GetHashCode() == int.Parse(data))
                {
                    report.DangerType = enemy;
                    break;
                }
            }
            await Responce("Опишите угрозу");
            container.ForwardEnumState<ReportGettingState>();
            
            return Result.Ok();
        }
    }
}
