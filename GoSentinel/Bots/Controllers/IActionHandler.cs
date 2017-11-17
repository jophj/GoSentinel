using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionHandler
    {
        Task<IActionResponse> HandleAsync(PokemonFilterAction action);
    }
}
