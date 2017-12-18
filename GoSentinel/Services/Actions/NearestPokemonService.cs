using System;
using GoSentinel.Models;

namespace GoSentinel.Services.Actions
{
    public class FakeNearestPokemonService : INearestPokemonService
    {
        public PokemonSpawn GetNearest(string pokemonId, int i, int i1)
        {
            var random = new Random();
            pokemonId = pokemonId[0].ToString().ToUpper() + pokemonId.Substring(1);
            return new PokemonSpawn()
            {
                PokemonId = Enum.Parse<POGOProtos.Enums.PokemonId>(pokemonId),
                Level = random.Next(1, 30),
                Attack = random.Next(0, 15),
                Defense = random.Next(0, 15),
                Stamina = random.Next(0, 15),
                DisappearTime = DateTime.Now.AddMinutes(28),
                Latitude = (float) (random.NextDouble() * (43.900148 - 43.851867) + 43.851867),
                Longitude = (float) (random.NextDouble() * (11.135097 - 11.052241) + 11.052241),
                SpawnpointId = new Guid().ToString()
            };
        }

        private string GetPokemonName(int pokemonId)
        {
            return ((POGOProtos.Enums.PokemonId)pokemonId).ToString();
        }
    }
}
