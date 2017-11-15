using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IPokemonFilterService
    {
        Task<IAiActionResponse> Add(PokemonFilterAction action);
    }
}
