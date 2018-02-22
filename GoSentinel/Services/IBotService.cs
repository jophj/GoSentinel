using System;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using Telegram.Bot.Types;

namespace GoSentinel.Services
{
    public interface IBotService
    {
        void Init(TelegramBotConfiguration telegramBotConfiguration);
        void RegisterMessageController(IBotMessageController messageHandler);
    }
}
