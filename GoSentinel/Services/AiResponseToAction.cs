﻿using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public class AiResponseToAction
    {
        private static readonly IDictionary<string, Func<AIResponse, IAiAction>> ActionMap = new Dictionary<string, Func<AIResponse, IAiAction>>()
        {
            { AiActionName.ADD_POKEMON_FILTER, r =>
                {
                    int v1 = 0;
                    int? v2 = null;
                    try
                    {
                        v1 = int.Parse(r.Result.Parameters["number"].ToString());
                    }
                    catch (Exception)
                    {
                        v1 = 0;
                    }
                    try
                    {
                        v2 = int.Parse(r.Result.Parameters["number1"].ToString());
                    }
                    catch (Exception)
                    {
                        v2 = null;
                    }

                    PokemonStat stat;
                    try
                    {
                        stat = (PokemonStat) Enum.Parse(typeof(PokemonStat),
                            r.Result.Parameters["PokemonStat"].ToString());
                    }
                    catch (Exception)
                    {
                        stat = PokemonStat.Iv;
                    }
                    
                    var action = new AddPokemonFilterAction()
                    {
                        PokemonName = r.Result.Parameters["Pokemon"].ToString(),
                        Stat = stat,
                        ValueMin = v2 != null ? Math.Min(v1, v2.Value) : v1,
                        ValueMax = v2 != null ? Math.Max(v1, v2.Value) : (int?) null
                    };

                    return action;
                }
            }
        };

        public static IAiAction Map(AIResponse aiResponse)
        {
            if (aiResponse.IsError)
            {
                return null;
            }

            return ActionMap[aiResponse.Result.Action](aiResponse);
        }
    }
}
