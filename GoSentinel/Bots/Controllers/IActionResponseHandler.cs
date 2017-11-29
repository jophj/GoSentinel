using GoSentinel.Models;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionResponseController
    {
        IActionResponse Handle(IAction action);
    }

    public interface IActionResponseController<T> : IActionResponseController where T : IActionResponse
    { }

    public interface IActionResponseHandler
    {
        string Handle(PokemonFilterActionResponse actionResponse);
    }
}
