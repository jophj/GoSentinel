using System;
using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public class NearestPokemonMapping : IAiResponseMapper<NearestPokemonAction>
    {
        public NearestPokemonAction Map(AIResponse response)
        {
            if (
                response?.Result?.Parameters == null ||
                !response.Result.Parameters.ContainsKey("Pokemon"))
            {
                throw new ArgumentException("Bad AIResponse");
            }

            return new NearestPokemonAction()
            {
                PokemonName = response.Result.Parameters["Pokemon"].ToString(),
            };
        }
    }
}
