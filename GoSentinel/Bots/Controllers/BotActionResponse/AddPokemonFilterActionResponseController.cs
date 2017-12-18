using System;
using System.Threading.Tasks;
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

        public async Task HandleAsync(IBot bot, IActionResponse actionResponseBase)
        {
            if (!(actionResponseBase is AddPokemonFilterActionResponse actionResponse))
            {
                throw new ArgumentException("Action response incorrect type");
            }

            var msg = _messageService.Generate(actionResponse);
            await bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}
