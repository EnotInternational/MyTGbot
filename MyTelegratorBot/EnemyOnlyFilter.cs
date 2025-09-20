using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegrator.Attributes;
using Telegrator.Filters.Components;

namespace MyTelegratorBot
{
    public class EnemyOnlyFilter : FilterAnnotation<CallbackQuery>
    {
        public override bool CanPass(FilterExecutionContext<CallbackQuery> context)
        {
            return context.Input.Data.StartsWith("enemy_");
        }
    }
}
