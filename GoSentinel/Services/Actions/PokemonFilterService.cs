using System;
using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Services.Actions
{
    public interface IPokemonFilterActionService
    {
        Task<IActionResponse> Add(PokemonFilterAction action);
    }

    public class LogPokemonFilterActionService : IPokemonFilterActionService
    {
        public async Task<IActionResponse> Add(PokemonFilterAction action)
        {
            Console.WriteLine($"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat}");
            return new PokemonFilterActionResponse()
            {
                Action = action
            };
        }
    }
}
