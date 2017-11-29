using System;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public class NearestPokemonActionController : IActionController<NearestPokemonAction>
    {
        public IActionResponse Handle(IAction baseAction)
        {
            if (!(baseAction is NearestPokemonAction action))
            {
                throw new NullReferenceException();
            }

            Console.WriteLine($"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName}");
            return new NearestPokemonActionResponse()
            {
                Action = action
            };
        }
    }

    public class NearestPokemonActionResponseController : IActionResponseController<NearestPokemonActionResponse>
    {
        public void Handle(IBot bot, IActionResponse actionResponse)
        {
            throw new NotImplementedException();
        }
    }
}