using System;
using System.Threading.Tasks;
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

            if (actionResponse.PokemonSpawn == null)
            {
                throw new ArgumentException("PokemonSpawn cannot be null");
            }

            var msg = _messageService.Generate(actionResponse);
            try
            {
                var message = bot.SendTextMessageAsync(actionResponse.Action.Message.Chat.Id, msg).Result;
                bot.SendLocationAsync(
                    actionResponse.Action.Message.Chat.Id,
                    actionResponse.PokemonSpawn.Latitude,
                    actionResponse.PokemonSpawn.Longitude
                );
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}
