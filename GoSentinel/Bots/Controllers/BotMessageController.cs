using System;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Models;
using GoSentinel.Services;
using GoSentinel.Services.ActionMappings;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;
        private readonly AiResponseToActionService _aiResponseToActionService;
        private readonly IActionHandler _actionHandler;
        private readonly IResponseServiceSelector _responseServiceSelector;
        private readonly IServiceProvider _serviceProvider;

        public BotMessageController(
            ApiAi apiAi,
            AiResponseToActionService aiResponseToActionService,
            IActionHandler actionHandler,
            IResponseServiceSelector responseServiceSelector,
            IServiceProvider serviceProvider
            )
        {
            _apiAi = apiAi;
            _aiResponseToActionService = aiResponseToActionService;
            _actionHandler = actionHandler;
            _responseServiceSelector = responseServiceSelector;
            _serviceProvider = serviceProvider;
        }

        public async Task OnMessageAsync(IBot bot, Message message)
        {
            AIResponse aiResponse = _apiAi.TextRequest(message.Text);
            IAction action = _aiResponseToActionService.Map(aiResponse);
            action.Message = message;
            Type actionControllerGenericType = typeof(IActionController<>).MakeGenericType(action.GetType());
            IActionController actionController = (IActionController)_serviceProvider.GetService(actionControllerGenericType);
            actionController?.Handle(action);
            IActionResponse actionResponse = await action.Accept(_actionHandler);
            var actionService = _responseServiceSelector.GetService(actionResponse);
            string textResponse = actionService.Handle(actionResponse);
            await bot.SendTextMessageAsync(message.Chat.Id, textResponse);
        }
    }
}
