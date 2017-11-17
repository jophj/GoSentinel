using System;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Models;
using GoSentinel.Services;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;
        private readonly IActionHandler _actionHandler;
        private readonly IResponseServiceSelector _responseServiceSelector;

        public BotMessageController(
            ApiAi apiAi,
            IActionHandler actionHandler,
            IResponseServiceSelector responseServiceSelector
            )
        {
            _apiAi = apiAi;
            _actionHandler = actionHandler;
            _responseServiceSelector = responseServiceSelector;
        }

        public async Task OnMessageAsync(IBot bot, Message message)
        {
            AIResponse aiResponse = _apiAi.TextRequest(message.Text);
            IAction action = AiResponseToAction.Map(aiResponse);
            action.Message = message;
            IActionResponse actionResponse = await action.Accept(_actionHandler);
            var actionService = _responseServiceSelector.GetService(actionResponse);
            string textResponse = actionService.Handle(actionResponse);
            await bot.SendTextMessageAsync(message.Chat.Id, textResponse);
        }
    }
}
