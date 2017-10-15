using System;
using GoSentinel.Bots.Controllers;
using GoSentinel.Models;
using Telegram.Bot.Types;

namespace GoSentinel.Services
{
    public interface IBotService
    {
        void Init(TelegramBotConfiguration telegramBotConfiguration);
        void RegisterMessageController(IBotMessageController messageHandler);
    }
}
