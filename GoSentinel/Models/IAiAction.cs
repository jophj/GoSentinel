using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public interface IAiAction
    {
        Message Message { get; set; }
        void Accept(IAiActionHandler handler);
    }
}