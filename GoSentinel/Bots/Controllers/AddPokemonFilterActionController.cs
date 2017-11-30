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

        public IActionResponse Handle(IAction action)
        {
            return _pokemonFilterActionService.Add(action as AddPokemonFilterAction).Result;
        }
    }
}
