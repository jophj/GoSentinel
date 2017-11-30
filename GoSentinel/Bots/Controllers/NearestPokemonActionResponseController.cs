using System;
using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers
{
    public class NearestPokemonActionResponseController : IActionResponseController<NearestPokemonActionResponse>
    {
        public void Handle(IBot bot, IActionResponse actionResponseBase)
        {
            if (actionResponseBase == null)
            {
                throw new ArgumentNullException();
            }

            if (!(actionResponseBase is NearestPokemonActionResponse actionResponse))
            {
                throw new ArgumentException();
            }

            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, $"{actionResponse.Action.GetType().Name} - {actionResponse.Action.Message.From.Username} - {actionResponse.Action.PokemonName}");
        }
    }
}