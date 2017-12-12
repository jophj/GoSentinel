using System;
using System.Text;
using GoSentinel.Data;
using GoSentinel.Models;

namespace GoSentinel.Services.Messages
{
    public class PokemonSpawnMessageService : IMessageService<NearestPokemonActionResponse>
    {
        public string Generate(NearestPokemonActionResponse actionResponse)
        {
            if (actionResponse.PokemonSpawn == null)
            {
                throw new ArgumentException("Pokemon spawn cannot be null");
            }

            StringBuilder builder = new StringBuilder();

            var pokemonName = GetPokemonName(actionResponse.PokemonSpawn.PokemonId);
            builder.AppendLine($"**{pokemonName} {getIv(actionResponse.PokemonSpawn)}%**");

            var time = GetTime(actionResponse.PokemonSpawn.DisappearTime);
            var timeSpan = GetTimeSpan(actionResponse.PokemonSpawn.DisappearTime);
            builder.AppendLine($"Available until {time} ({timeSpan})");

            builder.AppendLine($"CP: {actionResponse.PokemonSpawn.Cp} (Level: {actionResponse.PokemonSpawn.Level})");
            return builder.ToString();
        }

        private string getIv(PokemonSpawn pokemon)
        {
            return ((pokemon.Attack + pokemon.Defense + pokemon.Stamina) / 45).ToString("0.0");
        }

        private string GetTimeSpan(DateTime disappearTime)
        {
            var timeSpan = disappearTime.Subtract(DateTime.Now);
            var formatted = $"{timeSpan.Minutes}m{timeSpan.Seconds}s";
            if (timeSpan.Hours > 0)
            {
                formatted = formatted.Insert(0, $"{timeSpan.Hours}h");
            }

            return formatted;
        }

        private string GetTime(DateTime disappearTime)
        {
            return disappearTime.ToLongTimeString();
        }

        private string GetPokemonName(int pokemonId)
        {
            return ((POGOProtos.Enums.PokemonId)pokemonId).ToString();
        }
    }
}
