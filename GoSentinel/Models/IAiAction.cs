using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public interface IAiAction
    {
        string UserId { get; set; }
        Message Message { get; set; }
        Task<IAiActionResponse> Accept(IAiActionHandler handler);
    }

    public interface IAiActionResponse
    {
        IAiAction Action { get; set; }
    }
}