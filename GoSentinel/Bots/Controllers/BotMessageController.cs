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
            var action = AiResponseToAction.Map(response);
            action.Message = message;
            action.Accept(_aiActionHandler);
            await bot.SendTextMessageAsync(message.Chat.Id, response.Result.Fulfillment.Speech);
        }
    }

    public class AiActionHandler : IAiActionHandler
    {
        private readonly IPokemonFilterService _pokemonFilterService;

        public AiActionHandler(IPokemonFilterService pokemonFilterService)
        {
            _pokemonFilterService = pokemonFilterService;
        }

        public async Task<IAiActionResponse> HandleAsync(PokemonFilter action)
        {
            return await _pokemonFilterService.Add(action);
        }
    }
}
