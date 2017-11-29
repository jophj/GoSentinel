using ApiAiSDK.Model;
using GoSentinel.Models;

namespace GoSentinel.Services.ActionMappings
{
    public class NearestPokemonMapping : IAiResponseMapper<NearestPokemonAction>
    {
        public NearestPokemonAction Map(AIResponse response)
        {
            return new NearestPokemonAction()
            {
                PokemonName = response.Result.Parameters["Pokemon"].ToString(),
            };
        }
    }
}
