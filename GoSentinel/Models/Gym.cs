using System.Collections;
using System.Collections.Generic;

namespace GoSentinel.Models
{
    public class Gym
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DefenderPokemon> Pokemons { get; set; }
    }
}