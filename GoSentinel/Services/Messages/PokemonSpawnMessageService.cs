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
            if (actionResponse.SpawnPokemon == null)
            {
                throw new ArgumentException("Pokemon spawn cannot be null");
            }

            StringBuilder builder = new StringBuilder();

            var pokemonName = GetPokemonName(actionResponse.SpawnPokemon.PokemonId);
            builder.AppendLine($"**{pokemonName} {GetIv(actionResponse.SpawnPokemon)}%**");

            var time = GetTime(actionResponse.SpawnPokemon.DisappearTime);
            var timeSpan = GetTimeSpan(actionResponse.SpawnPokemon.DisappearTime);
            builder.AppendLine($"Available until {time} ({timeSpan})");

            builder.AppendLine($"CP: {actionResponse.SpawnPokemon.Cp} (Level: {actionResponse.SpawnPokemon.Level})");
            return builder.ToString();
        }

        private string GetIv(SpawnPokemon spawnPokemon)
        {
            if (spawnPokemon.Attack == null || spawnPokemon.Defense == null || spawnPokemon.Stamina == null)
            {
                return "?";
            }

            return ((spawnPokemon.Attack.Value + spawnPokemon.Defense.Value + spawnPokemon.Stamina.Value) * 100 / 45f).ToString("0.0");
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
