using System;
using ApiAiSDK;
using GoSentinel.Models;
using GoSentinel.Services;
using Telegram.Bot.Types;

namespace GoSentinel.Bots.Controllers
{
    public class BotMessageController :  IBotMessageController
    {
        private readonly ApiAi _apiAi;
        private readonly IActionVisitor _actionVisitor;

        public BotMessageController(ApiAi apiAi, IActionVisitor actionVisitor)
        {
            _apiAi = apiAi;
            _actionVisitor = actionVisitor;
        }

        public void OnMessage(IBot bot, Message message)
        {
            var response = _apiAi.TextRequest(message.Text);
            bot.SendTextMessageAsync(message.Chat.Id, response.Result.Fulfillment.Speech);
            var action = AiResponseToAction.Map(response);
            action.Accept(_actionVisitor);
        }
    }

    public interface IActionVisitor
    {
        void Visit(AddPokemonFilterAction action);
    }

    public class LogVisitVisitor : IActionVisitor
    {
        public void Visit(AddPokemonFilterAction action)
        {
            Console.WriteLine(action.PokemonName);
        }
    }
}
