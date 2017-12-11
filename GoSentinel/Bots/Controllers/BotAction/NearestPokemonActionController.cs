using System;
using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class NearestPokemonActionController : IActionController<NearestPokemonBotAction>
    {
        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is NearestPokemonBotAction action))
            {
                throw new ArgumentException();
            }

            Console.WriteLine($"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName}");
            return new NearestPokemonActionResponse()
            {
                Action = action
            };
        }
    }
}
