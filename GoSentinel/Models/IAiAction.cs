using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;

namespace GoSentinel.Models
{
    public interface IAiAction
    {
        void Accept(IAiActionHandler handler);
    }
}