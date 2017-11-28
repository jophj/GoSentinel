using System.Threading.Tasks;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public class NearestPokemonAction : IAction
    {
        public NearestPokemonAction()
        {
            Name = Action.NearestPokemon;
        }

        public string Name { get; }
        public string UserId { get; set; }
        public Message Message { get; set; }
        public string PokemonName { get; set; }
    }
}