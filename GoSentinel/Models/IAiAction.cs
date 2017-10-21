using GoSentinel.Bots.Controllers;

namespace GoSentinel.Models
{
    public interface IAiAction
    {
        void Accept(IActionVisitor visitor);
    }
}