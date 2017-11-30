using GoSentinel.Models;

namespace GoSentinel.Services.ActionResponse
{
    public class PokemonFilterActionResponseService
    {
        public string Handle(IActionResponse actionResponse)
        {
            // WUT THIS SHIT
            AddPokemonFilterAction action = ((PokemonFilterActionResponse)actionResponse).Action; // TODO
            return $"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat} - {action.ValueMin } - {action.ValueMax}";
        }
    }
}
