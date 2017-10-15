using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GoSentinel.Bots
{
    public interface IBot
    {
        void AddMessageHandler(Action<Message> messageHandler);
        Task<Message> SendTextMessageAsync(long chatId, string text);
    }

    public interface ITelegramBot : IBot { }
}