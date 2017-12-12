using System;
using System.Text;
using GoSentinel.Data;

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
            builder.AppendLine($"**{pokemonName} 100%**");

            var time = GetTime(actionResponse.PokemonSpawn.DisappearTime);
            var timeSpan = GetTimeSpan(actionResponse.PokemonSpawn.DisappearTime);
            builder.AppendLine($"Available until {time} ({timeSpan})");

            return builder.ToString();
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
