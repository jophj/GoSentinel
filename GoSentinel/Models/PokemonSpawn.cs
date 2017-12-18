using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoSentinel.Models
{
    public class PokemonSpawn
    {
        public string SpawnpointId { get; set; }
        public POGOProtos.Enums.PokemonId PokemonId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime DisappearTime { get; set; }
        public int? Attack { get; set; }
        public int? Defense { get; set; }
        public int? Stamina { get; set; }
        public int? Level { get; set; }
        public int? Cp { get; set; }
    }
}
