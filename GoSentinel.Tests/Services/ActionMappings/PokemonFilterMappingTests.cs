using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services.ActionMappings;
using Xunit;

namespace GoSentinel.Tests.Services.ActionMappings
{
    public class PokemonFilterMappingTests
    {
        private readonly PokemonFilterMapping _pokemonFilterMapping;

        public PokemonFilterMappingTests()
        {
            _pokemonFilterMapping = new PokemonFilterMapping();
        }

        [Fact]
        public void Should_Map_AddPokemonFilterAction()
        {
            var action = _pokemonFilterMapping.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = BotAction.AddPokemonFilter,
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
            var action = _pokemonFilterMapping.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = BotAction.AddPokemonFilter,
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
            var action = _pokemonFilterMapping.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = BotAction.AddPokemonFilter,
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
            var action = _pokemonFilterMapping.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Action = BotAction.AddPokemonFilter,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Rattata" },
                        { "number", "4" },
                    }
                }
            });

            Assert.Equal("Rattata", action.PokemonName);
            Assert.Equal(PokemonStat.Iv, action.Stat);
            Assert.Null(action.ValueMax);
            Assert.Equal(4, action.ValueMin);
        }
    }
}
