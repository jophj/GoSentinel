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
                throw new ArgumentNullException();
            }

            var action = actionResponse.Action;
            var msg = $"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat} - {action.ValueMin } - {action.ValueMax}";
            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}