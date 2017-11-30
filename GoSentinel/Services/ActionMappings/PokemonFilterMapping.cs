﻿using System;
using System.Collections.Generic;
using System.Linq;
using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public class PokemonFilterMapping : IAiResponseMapper<AddPokemonFilterAction>
    {
        public AddPokemonFilterAction Map(AIResponse r)
        {
            {
                int v1;
                int? v2;
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
                    string statName = char.ToUpper(r.Result.Parameters["PokemonStat"].ToString()[0]) +
                                      r.Result.Parameters["PokemonStat"].ToString().Substring(1);
                    stat = (PokemonStat)Enum.Parse(typeof(PokemonStat), statName);
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
                    ValueMax = v2 != null ? Math.Max(v1, v2.Value) : (int?)null
                };

                return action;
            }
        }
    }
}
