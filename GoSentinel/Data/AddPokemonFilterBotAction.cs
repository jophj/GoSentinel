namespace GoSentinel.Data
{
    public class AddPokemonFilterBotAction : BotAction
    {
        public override string Name => AddPokemonFilter;
        public string PokemonName { get; set; }
        public PokemonStat? Stat { get; set; }
        public int? ValueMin { get; set; }
        public int? ValueMax { get; set; }
    }

    public class AddPokemonFilterActionResponse : IActionResponse<AddPokemonFilterBotAction>
    {
        public AddPokemonFilterBotAction Action { get; set; }
    }
}
