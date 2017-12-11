using Telegram.Bot.Types;

namespace GoSentinel.Data
{
    public abstract class BotAction : IAction
    {
        public static readonly string AddPokemonFilter = "AddPokemonFilter";
        public static readonly string NearestPokemon = "NearestPokemon";

        public abstract string Name { get; }
        public Message Message { get; set; }
    }
}
