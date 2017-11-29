﻿using GoSentinel.Models;
using GoSentinel.Services.Actions;

namespace GoSentinel.Bots.Controllers
{
    public class PokemonFilterActionController : IActionController<PokemonFilterAction>
    {
        private readonly IPokemonFilterActionService _pokemonFilterActionService;

        public PokemonFilterActionController(IPokemonFilterActionService pokemonFilterActionService)
        {
            _pokemonFilterActionService = pokemonFilterActionService;
        }

        public IActionResponse Handle(IAction action)
        {
            return _pokemonFilterActionService.Add(action as PokemonFilterAction).Result;
        }
    }

    public class PokemonFilterActionResponseController : IActionResponseController<PokemonFilterActionResponse>
    {
        public void Handle(IBot bot, IActionResponse actionResponse)
        {

            throw new System.NotImplementedException();
        }
    }
}
