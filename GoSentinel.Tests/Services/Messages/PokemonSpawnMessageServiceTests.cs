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
        [InlineData(147, 10, "Dratini")]
        [InlineData(32, 11, "NidoranMale")]
        [InlineData(359, 14, "Absol")]
        [InlineData(9999, 15, "9999")]
        public void Generate_WithPokemonSpawn_ShouldHaveFormattedFirstLine(int pokemonId, int atk, string pokemonName)
        {
            var actionResponse = MakeActionResponse(MakePokemonSpawn());
            actionResponse.PokemonSpawn.PokemonId = pokemonId;
            actionResponse.PokemonSpawn.Attack = atk;

            var message = _service.Generate(actionResponse);
            var iv = ((
                          actionResponse.PokemonSpawn.Attack +
                          actionResponse.PokemonSpawn.Defense +
                          actionResponse.PokemonSpawn.Stamina
                      ) / 45).ToString("0.0");

            var lines = message.Split(Environment.NewLine);

            Assert.Equal($"**{pokemonName} {iv}%**", lines[0]);
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

        [Theory]
        [InlineData(10, "CP: 10 (Level: 1)")]
        [InlineData(60, "CP: 60 (Level: 6)")]
        [InlineData(125, "CP: 125 (Level: 12)")]
        [InlineData(3568, "CP: 3568 (Level: 356)")]
        public void Generate_WithPokemonSpawn_ShouldHaveFormattedCpAndLevel(int cp, string lineFormatted)
        {
            var actionResponse = MakeActionResponse(MakePokemonSpawn());
            actionResponse.PokemonSpawn.Cp = cp;
            actionResponse.PokemonSpawn.Level = cp / 10;

            var message = _service.Generate(actionResponse);
            var lines = message.Split(Environment.NewLine);

            Assert.Equal(lineFormatted, lines[2]);
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
                Cp = 3000,
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
