using GoSentinel.Data;

namespace GoSentinel.Services.ActionResponse
{
    public class PokemonFilterActionResponseService
    {
        public string Handle(IActionResponse actionResponse)
        {
            // WUT THIS SHIT
            AddPokemonFilterBotAction botAction = ((AddPokemonFilterActionResponse)actionResponse).BotAction; // TODO
            return $"{botAction.GetType().Name} - {botAction.Message.From.Username} - {botAction.PokemonName} - {botAction.Stat} - {botAction.ValueMin } - {botAction.ValueMax}";
        }
    }
}
