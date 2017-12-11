using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public class NearestPokemonMapping : IAiResponseMapper<NearestPokemonBotAction>
    {
        public NearestPokemonBotAction Map(AIResponse response)
        {
            return new NearestPokemonBotAction()
            {
                PokemonName = response.Result.Parameters["Pokemon"].ToString(),
            };
        }
    }
}
