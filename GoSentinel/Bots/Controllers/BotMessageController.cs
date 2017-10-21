using System;
using System.Threading.Tasks;
using ApiAiSDK;
using GoSentinel.Models;
using GoSentinel.Services;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;
        private readonly IAiActionHandler _aiActionHandler;

        public BotMessageController(ApiAi apiAi, IAiActionHandler aiActionHandler)
        {
            _apiAi = apiAi;
            _aiActionHandler = aiActionHandler;
        }

        public async Task OnMessageAsync(IBot bot, Message message)
        {
            var response = _apiAi.TextRequest(message.Text);
            await bot.SendTextMessageAsync(message.Chat.Id, response.Result.Fulfillment.Speech);
            var action = AiResponseToAction.Map(response);
            action.Accept(_aiActionHandler);
        }
    }

    public class AiActionHandler : IAiActionHandler
    {
        public Task HandleAsync(AddPokemonFilterAction action)
        {
            Console.WriteLine(action.PokemonName);
            return null;
        }
    }
}
