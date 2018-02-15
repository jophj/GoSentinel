using System;
using System.Threading.Tasks;
using GoSentinel.Data;
using GoSentinel.Services.Messages;

namespace GoSentinel.Bots.Controllers.BotActionResponse
{
    public class GymStateActionResponseController : IActionResponseController<GymStateActionResponse>
    {
        private readonly IMessageService<GymStateActionResponse> _messageService;

        public GymStateActionResponseController(IMessageService<GymStateActionResponse> messageService)
        {
            _messageService = messageService;
        }

        public async Task HandleAsync(IBot bot, IActionResponse actionResponseBase)
        {
            if (!(actionResponseBase is GymStateActionResponse actionResponse))
            {
                throw new ArgumentException("Action response incorrect type");
            }

            var msg = _messageService.Generate(actionResponse);
            await bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg);
        }
    }
}