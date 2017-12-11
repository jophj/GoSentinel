using System;
using System.Threading.Tasks;
using GoSentinel.Data;

namespace GoSentinel.Services.Actions
{
    public interface IPokemonFilterActionService
    {
        Task<IActionResponse> Add(AddPokemonFilterBotAction botAction);
    }

    public class LogPokemonFilterActionService : IPokemonFilterActionService
    {
        public async Task<IActionResponse> Add(AddPokemonFilterBotAction botAction)
        {
            Console.WriteLine($"{botAction.GetType().Name} - {botAction.Message.From.Username} - {botAction.PokemonName} - {botAction.Stat}");
            return new AddPokemonFilterActionResponse()
            {
                Action = botAction
            };
        }
    }
}
