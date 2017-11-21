using System.Threading.Tasks;
using GoSentinel.Models;
using GoSentinel.Services.ActionMappings;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionHandler
    {
        Task<IActionResponse> HandleAsync(PokemonFilterAction action);
        Task<IActionResponse> HandleAsync(NearestPokemonAction action);
    }
}
