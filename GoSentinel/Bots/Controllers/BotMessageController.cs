using System;
using System.Data;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services;
using GoSentinel.Services.ActionMappings;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
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
            AIResponse aiResponse = _apiAi.TextRequest(message.Text);
            IAction action = _aiResponseToActionService.Map(aiResponse);
            action.Message = message;
            Type actionControllerGenericType = typeof(IActionController<>).MakeGenericType(action.GetType());
            IActionController actionController = (IActionController)_serviceProvider.GetService(actionControllerGenericType);
            IActionResponse actionResponse = actionController?.Handle(action);

            if (actionResponse == null)
            {
                throw new ApplicationException("Action response should is null");
            }

            Type actionResponseControllerGenericType = typeof(IActionResponseController<>).MakeGenericType(actionResponse.GetType());
            IActionResponseController actionResponseController = (IActionResponseController)_serviceProvider.GetService(actionResponseControllerGenericType);
            actionResponseController?.Handle(bot, actionResponse);
        }
    }
}
