using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public interface IAiActionHandler
    {
        Task HandleAsync(AddPokemonFilterAction action);
    }
}