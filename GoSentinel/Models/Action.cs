using System.Threading.Tasks;
using GoSentinel.Bots.Controllers;
using Telegram.Bot.Types;

namespace GoSentinel.Models
{
    public abstract class Action : IAction
    {
        public static readonly string AddPokemonFilter = "AddPokemonFilter";
        public static readonly string NearestPokemon = "NearestPokemon";

        public abstract string Name { get; }
        public string UserId { get; set; }
        public Message Message { get; set; }
    }
}
