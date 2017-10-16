using System;
using ApiAiSDK;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;

        public BotMessageController(ApiAi apiAi)
        {
            _apiAi = apiAi;
        }

        public void OnMessage(IBot bot, Message message)
        {
            Console.WriteLine(message);
            var response = _apiAi.TextRequest(message.Text);
            bot.SendTextMessageAsync(message.Chat.Id, response.Result.Fulfillment.Speech);
        }
    }
}
