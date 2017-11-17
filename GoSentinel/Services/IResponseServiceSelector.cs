using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IResponseServiceSelector
    {
        IActionResponseServiceBase GetService<T>(T actionResponseType) where T : IActionResponse;
    }
}
