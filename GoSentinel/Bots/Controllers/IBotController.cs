using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public interface IBotMessageController
    {
        Task OnMessageAsync(IBot bot, Message message);
    }
}
