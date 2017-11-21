using GoSentinel.Models;

namespace GoSentinel.Services.ActionResponse
{
    public class PokemonFilterActionResponseService : IActionResponseService<PokemonFilterActionResponse>
    {
        public string Handle(IActionResponse actionResponse)
        {
            PokemonFilterAction action = (PokemonFilterAction) actionResponse.Action;
            return $"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat} - {action.ValueMin } - {action.ValueMax}";
        }
    }
}
