using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Models;
using Action = GoSentinel.Models.Action;

namespace GoSentinel.Services.ActionMappings
{
    public class AiResponseToActionService
    {
        public AiResponseToActionService(
            PokemonFilterMapping pokemonFilterMapping,
            NearestPokemonMapping nearestPokemonMapping
        )
        {
            _actionMap = new Dictionary<string, Func<AIResponse, IAction>>()
            {
                { Action.AddPokemonFilter, pokemonFilterMapping.Map },
                { Action.NearestPokemon, nearestPokemonMapping.Map }
            };
        }

        private readonly IDictionary<string, Func<AIResponse, IAction>> _actionMap;

        public IAction Map(AIResponse aiResponse)
        {
            if (aiResponse.IsError)
            {
                return null;
            }

            return _actionMap[aiResponse.Result.Action](aiResponse);
        }
    }
}
