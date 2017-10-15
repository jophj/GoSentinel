using System;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        public void OnMessage(IBot bot, Message message)
        {
            Console.WriteLine(message);
            bot.SendTextMessageAsync(message.Chat.Id, message.Text);
        }
    }
}