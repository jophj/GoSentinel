using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IActionResponseService<T> : IActionResponseServiceBase where T : IActionResponse
    {}

    public interface IActionResponseServiceBase
    {
        string Handle(IActionResponse actionResponse);
    }
}
