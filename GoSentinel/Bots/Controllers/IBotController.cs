using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public interface IBotMessageController
    {
        void OnMessage(IBot bot, Message message);
    }
}
