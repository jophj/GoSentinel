using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionResponseController
    {
        void Handle(IBot bot, IActionResponse actionResponse);
    }

    public interface IActionResponseController<T> : IActionResponseController where T : IActionResponse
    { }
}
