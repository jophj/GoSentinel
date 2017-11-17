using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IResponseService
    {
        string Handle(IActionResponse actionResponse);
    }
}