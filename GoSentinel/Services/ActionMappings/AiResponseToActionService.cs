using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public class AiResponseToActionService
    {
        public AiResponseToActionService(
            PokemonFilterMapping pokemonFilterMapping,
            NearestPokemonMapping nearestPokemonMapping,
            GymStateMapping gymStateMapping
        )
        {
            _actionMap = new Dictionary<string, Func<AIResponse, IAction>>()
            {
                { BotAction.AddPokemonFilter, pokemonFilterMapping.Map },
                { BotAction.NearestPokemon, nearestPokemonMapping.Map },
                { BotAction.GymStateRequest, gymStateMapping.Map }
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
