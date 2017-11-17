using GoSentinel.Bots.Controllers;

namespace GoSentinel.Models
{
    public interface IActionResponse
    {
        IAction Action { get; set; }
    }
}