using System.Threading.Tasks;
using GoSentinel.Models;
using GoSentinel.Services;

namespace GoSentinel.Bots.Controllers
{
    public class ActionHandler : IActionHandler
    {
        private readonly IPokemonFilterActionService _pokemonFilterActionService;

        public ActionHandler(IPokemonFilterActionService pokemonFilterActionService)
        {
            _pokemonFilterActionService = pokemonFilterActionService;
        }

        public async Task<IActionResponse> HandleAsync(PokemonFilterAction action)
        {
            return await _pokemonFilterActionService.Add(action);
        }
    }

    public interface IPokemonFilterActionService
    {
        Task<IActionResponse> Add(PokemonFilterAction action);
    }
}