namespace GoSentinel.Data
{
    public class NearestPokemonBotAction : BotAction
    {
        public override string Name => NearestPokemon;
        public string PokemonName { get; set; }
    }

    public class NearestPokemonActionResponse : IActionResponse<NearestPokemonBotAction>
    {
        public NearestPokemonBotAction BotAction { get; set; }
    }
}
