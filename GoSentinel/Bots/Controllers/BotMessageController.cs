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

            IAction action = MapAiResponse(aiResponse);
            action.Message = message;

            IActionController actionController = GetActionController(action);
            IActionResponse actionResponse = actionController?.Handle(action);

            IActionResponseController actionResponseController = GetActionResponseController(actionResponse);
            actionResponseController?.HandleAsync(bot, actionResponse);
        }

        protected virtual IActionResponseController GetActionResponseController(IActionResponse actionResponse)
        {
            Type actionResponseControllerGenericType = typeof(IActionResponseController<>).MakeGenericType(actionResponse.GetType());
            return (IActionResponseController)_serviceProvider.GetService(actionResponseControllerGenericType);
        }

        protected virtual IActionController GetActionController(IAction action)
        {
            Type actionControllerGenericType = typeof(IActionController<>).MakeGenericType(action.GetType());
            return (IActionController)_serviceProvider.GetService(actionControllerGenericType);
        }

        protected virtual IAction MapAiResponse(AIResponse aiResponse)
        {
            return _aiResponseToActionService.Map(aiResponse);
        }

        protected virtual AIResponse TextRequest(string messageText)
        {
            return _apiAi.TextRequest(messageText);
        }
    }
}
