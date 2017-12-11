using System;
using GoSentinel.Data;
using GoSentinel.Services.Messages;

namespace GoSentinel.Bots.Controllers.BotActionResponse
{
    public class AddPokemonFilterActionResponseController : IActionResponseController<AddPokemonFilterActionResponse>
    {
        private readonly IMessageService<AddPokemonFilterActionResponse> _messageService;

        public AddPokemonFilterActionResponseController(IMessageService<AddPokemonFilterActionResponse> messageService)
        {
            _messageService = messageService;
        }

        public void Handle(IBot bot, IActionResponse actionResponseBase)
        {
            if (!(actionResponseBase is AddPokemonFilterActionResponse actionResponse))
            {
                throw new ArgumentException("Action response incorrect type");
            }

            var msg = _messageService.Generate(actionResponse);
            bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}
