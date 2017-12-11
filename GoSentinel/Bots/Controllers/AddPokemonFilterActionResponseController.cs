using System;
using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers
{
    public class AddPokemonFilterActionResponseController : IActionResponseController<AddPokemonFilterActionResponse>
    {
        public void Handle(IBot bot, IActionResponse actionResponseBase)
        {
            if (!(actionResponseBase is AddPokemonFilterActionResponse actionResponse))
            {
                throw new ArgumentException("Action response incorrect type");
            }

            var action = actionResponse.Action;
            var statMsg = "";
            if (action.ValueMin != null || action.ValueMax != null)
            {
                statMsg = $"{action.Stat}";
                if (action.ValueMin != null)
                {
                    statMsg += $"min: {action.ValueMin}";
                }
                if (action.ValueMax != null)
                {
                    statMsg += $"max: {action.ValueMax}";
                }
            }
            
            var msg = $"{action.PokemonName} ({statMsg}) aggiunto alle notifiche";
            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}
