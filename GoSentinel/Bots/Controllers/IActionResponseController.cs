using System.Threading.Tasks;
using GoSentinel.Data;

namespace GoSentinel.Bots.Controllers
{
    public interface IActionResponseController
    {
        Task HandleAsync(IBot bot, IActionResponse actionResponse);
    }

    public interface IActionResponseController<T> : IActionResponseController where T : IActionResponse
    { }
}
