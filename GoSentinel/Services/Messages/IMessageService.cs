using GoSentinel.Data;

namespace GoSentinel.Services.Messages
{
    public interface IMessageService<T> where T : IActionResponse
    {
        string Generate(T actionResponse);
    }
}