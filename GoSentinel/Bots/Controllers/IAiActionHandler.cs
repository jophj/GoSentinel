using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public interface IAiActionHandler
    {
        Task<IAiActionResponse> HandleAsync(PokemonFilterAction action);
    }
}