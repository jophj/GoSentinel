using System.Threading.Tasks;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public class PokemonFilterAction : IAiAction
    {
        public string UserId { get; set; }
        public Message Message { get; set; }
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }

        public async Task<IAiActionResponse> Accept(IAiActionHandler handler)
        {
            return await handler.HandleAsync(this);
        }
    }
}
