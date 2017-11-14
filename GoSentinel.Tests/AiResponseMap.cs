using System;
using System.Collections.Generic;
using System.Text;
using ApiAiSDK.Model;
using GoSentinel.Models;
using GoSentinel.Services;
using Xunit;

namespace GoSentinel.Tests
{
    public class AiResponseMap
    {
        [Fact]
        public void Should_Map_AddPokemonFilterAction()
        {
            var action = (PokemonFilter) AiResponseToAction.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = AiActionName.ADD_POKEMON_FILTER,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Rattata" },
                        { "PokemonStat", "Level" },
                        { "number", "4" },
                        { "number1", 6 }
                    }
                }
            });

            Assert.Equal("Rattata", action.PokemonName);
            Assert.Equal(PokemonStat.Level, action.Stat);
            Assert.Equal(6, action.ValueMax);
            Assert.Equal(4, action.ValueMin);
        }

        [Fact]
        public void Should_Get_Iv_Default()
        {
            var action = (PokemonFilter)AiResponseToAction.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = AiActionName.ADD_POKEMON_FILTER,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Rattata" },
                        { "number", "4" },
                        { "number1", 6 }
                    }
                }
            });

            Assert.Equal("Rattata", action.PokemonName);
            Assert.Equal(PokemonStat.Iv, action.Stat);
            Assert.Equal(6, action.ValueMax);
            Assert.Equal(4, action.ValueMin);
        }

        [Fact]
        public void Should_Get_Correct_Stat()
        {
            var action = (PokemonFilter)AiResponseToAction.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = AiActionName.ADD_POKEMON_FILTER,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Rattata" },
                        { "PokemonStat", "cp" },
                        { "number1", 6 },
                    }
                }
            });

            Assert.Equal(PokemonStat.Cp, action.Stat);
        }

        [Fact]
        public void Should_Get_Null_MaxValue()
        {
            var action = (PokemonFilter)AiResponseToAction.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = AiActionName.ADD_POKEMON_FILTER,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Rattata" },
                        { "number", "4" },
                    }
                }
            });

            Assert.Equal("Rattata", action.PokemonName);
            Assert.Equal(PokemonStat.Iv, action.Stat);
            Assert.Equal(null, action.ValueMax);
            Assert.Equal(4, action.ValueMin);
        }
    }
}
