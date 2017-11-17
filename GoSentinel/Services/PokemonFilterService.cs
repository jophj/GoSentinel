using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSentinel.Bots.Controllers;
using GoSentinel.Models;

namespace GoSentinel.Services
{
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
