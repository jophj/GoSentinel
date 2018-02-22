using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionController
    {
        IActionResponse Handle(IAction baseAction);
    }

    public interface IActionController<T> : IActionController where T : IAction
    { }
}
