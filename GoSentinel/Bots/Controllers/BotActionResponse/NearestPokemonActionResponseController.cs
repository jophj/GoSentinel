using System;
using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers.BotActionResponse
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

            bot.SendTextMessageAsync(actionResponse.BotAction.Message.Chat.Id, $"{actionResponse.BotAction.GetType().Name} - {actionResponse.BotAction.Message.From.Username} - {actionResponse.BotAction.PokemonName}");
        }
    }
}
