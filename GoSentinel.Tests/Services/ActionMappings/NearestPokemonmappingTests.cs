using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Services.ActionMappings;
using Xunit;

namespace GoSentinel.Tests.Services.ActionMappings
{
    public class NearestPokemonmappingTests
    {
        [Fact]
        public void Map_WithWithNullResponse_ShouldThrowArgumentException()
        {
            var nearestPokemonMapping = new NearestPokemonMapping();

            void Act() => nearestPokemonMapping.Map(null);

            Assert.Throws<ArgumentException>((Action)Act);
        }

        [Fact]
        public void Map_WithNoResponsePokemonParameter_ShouldThrowArgumentException()
        {
            var nearestPokemonMapping = new NearestPokemonMapping();

            void Act() => nearestPokemonMapping.Map(new AIResponse());

            Assert.Throws<ArgumentException>((Action) Act);
        }

        [Fact]
        public void Map_WithResponsePokemonParameter_ReturnNearestPokemonAction()
        {
            var nearestPokemonMapping = new NearestPokemonMapping();

            var nearestPokemonAction = nearestPokemonMapping.Map(new AIResponse()
            {
                Result = new Result()
                {
                    Parameters = new Dictionary<string, object>()
                    {
                        { "Pokemon", "Kingler" }
                    }
                }
            });

            Assert.Equal("Kingler", nearestPokemonAction.PokemonName);
        }
    }
}