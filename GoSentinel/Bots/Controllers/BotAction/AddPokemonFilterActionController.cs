﻿using System;
using GoSentinel.Data;
using GoSentinel.Services.Actions;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class AddPokemonFilterActionController : IActionController<AddPokemonFilterAction>
    {
        private readonly IPokemonFilterService _pokemonFilterService;

        public AddPokemonFilterActionController(IPokemonFilterService pokemonFilterService)
        {
            _pokemonFilterService = pokemonFilterService;
        }

        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is AddPokemonFilterAction action))
            {
                throw new ArgumentException();
            }

            return _pokemonFilterService.Add(action).Result;
        }
    }
}
