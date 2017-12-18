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
            builder.AppendLine($"**{pokemonName} {GetIv(actionResponse.PokemonSpawn)}%**");

            var time = GetTime(actionResponse.PokemonSpawn.DisappearTime);
            var timeSpan = GetTimeSpan(actionResponse.PokemonSpawn.DisappearTime);
            builder.AppendLine($"Available until {time} ({timeSpan})");

            builder.AppendLine($"CP: {actionResponse.PokemonSpawn.Cp} (Level: {actionResponse.PokemonSpawn.Level})");
            return builder.ToString();
        }

        private string GetIv(PokemonSpawn pokemon)
        {
            if (pokemon.Attack == null || pokemon.Defense == null || pokemon.Stamina == null)
            {
                return "?";
            }

            return ((pokemon.Attack.Value + pokemon.Defense.Value + pokemon.Stamina.Value) * 100 / 45f).ToString("0.0");
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

        private string GetPokemonName(POGOProtos.Enums.PokemonId pokemonId)
        {
            return pokemonId.ToString();
        }
    }
}
