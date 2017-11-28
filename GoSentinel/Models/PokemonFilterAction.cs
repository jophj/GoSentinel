﻿namespace GoSentinel.Models
{
    public class PokemonFilterAction : Action
    {
        public override string Name => AddPokemonFilter;
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }
    }

    public class PokemonFilterActionResponse : IActionResponse<PokemonFilterAction>
    {
        public PokemonFilterAction Action { get; set; }
    }
}
