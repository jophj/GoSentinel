using System;
using GoSentinel.Data;
using GoSentinel.Services.Messages;

namespace GoSentinel.Bots.Controllers.BotActionResponse
{
    public class NearestPokemonActionResponseController : IActionResponseController<NearestPokemonActionResponse>
    {
        private readonly IMessageService<NearestPokemonActionResponse> _messageService;

        public NearestPokemonActionResponseController(IMessageService<NearestPokemonActionResponse> messageService)
        {
            _messageService = messageService;
        }

        public void Handle(IBot bot, IActionResponse actionResponseBase)
        {
            if (actionResponseBase == null)
            {
                throw new ArgumentNullException();
            }

            if (!(actionResponseBase is NearestPokemonActionResponse actionResponse))
            {
                throw new ArgumentException();
            }

            var msg = _messageService.Generate(actionResponse);
            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}
