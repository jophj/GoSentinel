using System;
using GoSentinel.Data;
using GoSentinel.Services.Actions;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class NearestPokemonActionController : IActionController<NearestPokemonAction>
    {
        private readonly INearestPokemonService _nearestPokemonService;

        public NearestPokemonActionController(INearestPokemonService nearestPokemonService)
        {
            _nearestPokemonService = nearestPokemonService;
        }

        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is NearestPokemonAction action) || string.IsNullOrEmpty(action.PokemonName))
            {
                throw new ArgumentException();
            }

            var pokemon = _nearestPokemonService.GetNearest(action.PokemonName, 0, 0);

            return new NearestPokemonActionResponse()
            {
                PokemonSpawn = pokemon,
                Action = action
            };
        }
    }
}
