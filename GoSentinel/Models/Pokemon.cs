using System;

namespace GoSentinel.Models
{
    public class SpawnPokemon : Pokemon
    {
        public string SpawnpointId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime DisappearTime { get; set; }
        public int? Level { get; set; }
        public int? Cp { get; set; }
    }

    public class Pokemon
    {
        public POGOProtos.Enums.PokemonId PokemonId { get; set; }
        public int? Attack { get; set; }
        public int? Defense { get; set; }
        public int? Stamina { get; set; }
    }

    public class DefenderPokemon : Pokemon
    {
        public int Cp { get; set; }
        public int DecayedCp { get; set; }
        public Trainer Trainer { get; set; }
    }
}
