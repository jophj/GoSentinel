﻿using System.Threading.Tasks;
using GoSentinel.Bots.Controllers;

namespace GoSentinel.Models
{
    public class PokemonFilterAction : Action
    {
        public override string Name => AddPokemonFilter;
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }


        public override async Task<IActionResponse> Accept(IActionHandler handler)
        {
            return await handler.HandleAsync(this);
        }
    }

    public class PokemonFilterActionResponse : IPokemonFilterActionResponse {
        public IAction Action { get; set; }
    }

    public interface IPokemonFilterActionResponse : IActionResponse
    {}
}
