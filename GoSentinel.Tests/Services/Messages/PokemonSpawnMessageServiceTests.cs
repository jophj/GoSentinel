using System;
using GoSentinel.Data;
using GoSentinel.Models;
using GoSentinel.Services.Messages;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Services.Messages
{
    public class PokemonSpawnMessageServiceTests
    {
        private readonly PokemonSpawnMessageService _service;

        public PokemonSpawnMessageServiceTests()
        {
            _service = new PokemonSpawnMessageService();
        }

        [Fact]
        public void Generate_WithNoPokemonSpawn_ShouldThrow()
        {
            var actionResponse = MakeActionResponse(null);

            Assert.Throws<ArgumentException>(() => _service.Generate(actionResponse));
        }

        [Theory]
        [InlineData(147, "Dratini")]
        [InlineData(32, "NidoranMale")]
        [InlineData(359, "Absol")]
        [InlineData(9999, "9999")]
        public void Generate_WithPokemonSpawn_ShouldHaveFormattedFirstLine(int pokemonId, string pokemonName)
        {
            var actionResponse = MakeActionResponse(MakePokemonSpawn());
            actionResponse.PokemonSpawn.PokemonId = pokemonId;

            var message = _service.Generate(actionResponse);
            var lines = message.Split(Environment.NewLine);

            Assert.Equal($"**{pokemonName} 100%**", lines[0]);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(60)]
        [InlineData(1751)]
        [InlineData(3598)]
        [InlineData(4294)]
        public void Generate_WithPokemonSpawn_ShouldHaveFormattedTime(int timeSpan)
        {
            var actionResponse = MakeActionResponse(MakePokemonSpawn());
            actionResponse.PokemonSpawn.DisappearTime = DateTime.Now.AddSeconds(timeSpan);

            var message = _service.Generate(actionResponse);
            var lines = message.Split(Environment.NewLine);

            Assert.Matches($"Available until {actionResponse.PokemonSpawn.DisappearTime.ToLongTimeString()}.*", lines[1]);
        }

        [Theory]
        [InlineData(1, "0m1s")]
        [InlineData(60, "1m0s")]
        [InlineData(1751, "29m11s")]
        [InlineData(3598, "59m58s")]
        [InlineData(4294, "1h11m34s")]
        public void Generate_WithPokemonSpawn_ShouldHaveFormattedTimeSpan(int timeSpan, string timeSpanFormatted)
        {
            var actionResponse = MakeActionResponse(MakePokemonSpawn());
            actionResponse.PokemonSpawn.DisappearTime = DateTime.Now.AddSeconds(timeSpan + 1);

            var message = _service.Generate(actionResponse);
            var lines = message.Split(Environment.NewLine);

            Assert.Matches($".+\\({timeSpanFormatted}\\)", lines[1]);
        }

        private PokemonSpawn MakePokemonSpawn()
        {
            return new PokemonSpawn()
            {
                SpawnpointId = "",
                PokemonId = 134,
                Latitude = 43.123,
                Longitude = 11.123,
                DisappearTime = DateTime.Now,
                Attack = 15,
                Defense = 15,
                Stamina = 15,
                Level = 30,
                From = 2
            };
        }

        private NearestPokemonActionResponse MakeActionResponse(PokemonSpawn spawn)
        {
            return new NearestPokemonActionResponse()
            {
                PokemonSpawn = spawn,
                Action = new NearestPokemonAction()
                {
                    Message = new Message()
                    {
                        Chat = new Chat()
                        {
                            Id = 0
                        }
                    }
                }
            };
        }
    }
}
