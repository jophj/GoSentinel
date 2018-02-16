using System;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services.ActionMappings;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class UnrecognizableIntentException : Exception
    {}

    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;
        private readonly AiResponseToActionService _aiResponseToActionService;
        private readonly IServiceProvider _serviceProvider;

        public BotMessageController(
            ApiAi apiAi,
            AiResponseToActionService aiResponseToActionService,
            IServiceProvider serviceProvider
            )
        {
            _apiAi = apiAi;
            _aiResponseToActionService = aiResponseToActionService;
            _serviceProvider = serviceProvider;
        }

        public void OnMessage(IBot bot, Message message)
        {
            if (bot == null || message == null)
            {
                throw new ArgumentException();
            }

            AIResponse aiResponse = TextRequest(message.Text);
            if (aiResponse.Result.Action == Data.BotAction.InputUnknown)
            {
                throw new UnrecognizableIntentException();
            }

            IAction action = _aiResponseToActionService.Map(aiResponse);
            action.Message = message;

            Type actionControllerGenericType = typeof(IActionController<>).MakeGenericType(action.GetType());
            IActionController actionController = (IActionController)_serviceProvider.GetService(actionControllerGenericType);
            IActionResponse actionResponse = actionController?.Handle(action);

            Type actionResponseControllerGenericType = typeof(IActionResponseController<>).MakeGenericType(actionResponse.GetType());
            IActionResponseController actionResponseController = (IActionResponseController)_serviceProvider.GetService(actionResponseControllerGenericType);
            actionResponseController?.HandleAsync(bot, actionResponse);
        }

        protected virtual AIResponse TextRequest(string messageText)
        {
            return _apiAi.TextRequest(messageText);
        }
    }
}
