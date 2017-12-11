using GoSentinel.Data;

namespace GoSentinel.Services.Messages
{
    public class NearestPokemonMessageService : IMessageService<NearestPokemonActionResponse>
    {
        public string Generate(NearestPokemonActionResponse actionResponse)
        {
            return "I can't send a location yet";
        }
    }
}