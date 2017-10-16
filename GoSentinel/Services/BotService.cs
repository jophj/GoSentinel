﻿using System;
using System.Collections.Generic;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Models;
using Telegram.Bot.Types;

namespace GoSentinel.Services
{
    class BotService : IBotService
    {
        private ICollection<IBot> _bots;

        public BotService()
        {
            _bots = new List<IBot>();
        }

        public void Init(TelegramBotConfiguration telegramBotConfiguration)
        {
            IBot bot = new TelegramBot(telegramBotConfiguration);
            _bots.Add(bot);
        }

        public void RegisterMessageController(IBotMessageController botController)
        {
            foreach (var bot in _bots)
            {
                bot.AddMessageHandler(message => botController.OnMessage(bot, message));
            }
        }
    }
}
