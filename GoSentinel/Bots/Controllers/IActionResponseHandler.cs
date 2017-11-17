using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionResponseHandler
    {
        string Handle(PokemonFilterActionResponse actionResponse);
    }
}