using GoSentinel.Models;

namespace GoSentinel.Data
{
    public class NearestPokemonAction : BotAction
    {
        public override string Name => NearestPokemon;
        public string PokemonName { get; set; }
    }

    public class NearestPokemonActionResponse : IActionResponse<NearestPokemonAction>
    {
        public NearestPokemonAction Action { get; set; }
        public PokemonSpawn PokemonSpawn { get; set; }
    }
}
