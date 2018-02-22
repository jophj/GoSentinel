using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public interface IBotMessageController
    {
        void OnMessage(IBot bot, Message message);
    }
}
