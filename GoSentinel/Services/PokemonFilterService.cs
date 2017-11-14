﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public class LogPokemonFilterService : IPokemonFilterService
    {
        public async Task<IAiActionResponse> Add(PokemonFilter action)
        {
            Console.WriteLine($"{action.GetType().Name} - {action.Message.From.Username} - {action.PokemonName} - {action.Stat}");
            return null;
        }
    }
}
