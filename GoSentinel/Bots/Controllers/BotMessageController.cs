using System;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services;
using GoSentinel.Services.ActionMappings;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class UnrecognizableIntentException : Exception
    {}

    public class BotMessageController :  IBotMessageController
    {
        private readonly IAiService _aiService;
        private readonly IAiResponseToActionService _aiResponseToActionService;
        private readonly IServiceProvider _serviceProvider;

        public BotMessageController(
            IAiService aiService,
            IAiResponseToActionService aiResponseToActionService,
            IServiceProvider serviceProvider
            )
        {
            _aiService = aiService;
            _aiResponseToActionService = aiResponseToActionService;
            _serviceProvider = serviceProvider;
        }

        public void OnMessage(IBot bot, Message message)
        {
            if (bot == null || message == null)
            {
                throw new ArgumentException();
            }

            AIResponse aiResponse = _aiService.TextRequest(message.Text);
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

        public virtual IActionResponseController GetActionResponseController(IActionResponse actionResponse)
        {
            Type actionResponseControllerGenericType = typeof(IActionResponseController<>).MakeGenericType(actionResponse.GetType());
            return (IActionResponseController)_serviceProvider.GetService(actionResponseControllerGenericType);
        }

        public virtual IActionController GetActionController(IAction action)
        {
            Type actionControllerGenericType = typeof(IActionController<>).MakeGenericType(action.GetType());
            return (IActionController)_serviceProvider.GetService(actionControllerGenericType);
        }

        public virtual IAction MapAiResponse(AIResponse aiResponse)
        {
            return _aiResponseToActionService.Map(aiResponse);
        }
    }
}
