using System;
using GoSentinel.Models;
using GoSentinel.Services.Actions;

namespace GoSentinel.Bots.Controllers
{
    public class AddPokemonFilterActionController : IActionController<AddPokemonFilterAction>
    {
        private readonly IPokemonFilterActionService _pokemonFilterActionService;

        public AddPokemonFilterActionController(IPokemonFilterActionService pokemonFilterActionService)
        {
            _pokemonFilterActionService = pokemonFilterActionService;
        }

        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is AddPokemonFilterAction action))
            {
                throw new ArgumentException();
            }

            return _pokemonFilterActionService.Add(action).Result;
        }
    }
}
