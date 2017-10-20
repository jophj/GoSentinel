using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSentinel.Models
{
    public class AddPokemonFilterAction : IAiAction
    {
        public string PokemonName { get; set; }
        public PokemonStat Stat { get; set; }
        public int ValueMin { get; set; }
        public int? ValueMax { get; set; }
    }
}
