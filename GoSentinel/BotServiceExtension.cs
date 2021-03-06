﻿using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using GoSentinel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GoSentinel
{
    public static class BotServiceExtension
    {
        public static void UseBotService(this IApplicationBuilder app)
        {
            IBotService botService = app.ApplicationServices.GetRequiredService<IBotService>();

            var telegramBotConfiguration =
                (TelegramBotConfiguration)app.ApplicationServices.GetService(typeof(TelegramBotConfiguration));

            botService.Init(telegramBotConfiguration);

            var messageControllers = app.ApplicationServices.GetServices<IBotMessageController>();
            foreach (var messageController in messageControllers)
            {
                botService.RegisterMessageController(messageController);
            }
        }
    }
}
