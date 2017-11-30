namespace GoSentinel.Models
{
    public class AddPokemonFilterAction : Action
    {
        public override string Name => AddPokemonFilter;
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }
    }

    public class PokemonFilterActionResponse : IActionResponse<AddPokemonFilterAction>
    {
        public AddPokemonFilterAction Action { get; set; }
    }
}
