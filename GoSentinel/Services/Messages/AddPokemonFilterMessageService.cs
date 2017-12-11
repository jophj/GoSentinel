using GoSentinel.Data;
using GoSentinel.Services.Messages;

namespace GoSentinel.Services.Messages
{
    public class AddPokemonFilterMessageService : IMessageService<AddPokemonFilterActionResponse>
    {
        public string Generate(AddPokemonFilterActionResponse actionResponse)
        {
            var action = actionResponse.BotAction;
            var statMsg = "";
            if (action.ValueMin != null || action.ValueMax != null)
            {
                statMsg = $"({action.Stat.ToString().ToUpper()}";
                if (action.ValueMin != null)
                {
                    statMsg += $" min: {action.ValueMin}";
                }
                if (action.ValueMax != null)
                {
                    statMsg += $" max: {action.ValueMax}";
                }
                statMsg += ") ";
            }

            var message = $"{action.PokemonName} {statMsg}aggiunto alle notifiche";

            return message;
        }
    }
}
