using System;
using GoSentinel.Bots.Controllers;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Services.Actions;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class NearestPokemonActionControllerTests
    {
        private readonly IActionController<NearestPokemonAction> _controller;

        public NearestPokemonActionControllerTests()
        {
            _controller = new NearestPokemonActionController(new FakeNearestPokemonService());
        }

        [Fact]
        public void Handle_WithNoPokemonName_ShouldThrowArgumentException()
        {
            var action = MakeNearestPokemonAction();
            action.PokemonName = null;

            Assert.Throws<ArgumentException>(() => _controller.Handle(action));
        }

        [Fact]
        public void Handle_WhenCalled_ShouldReturnNearestPokemonActionResponseType()
        {
            var response = _controller.Handle(MakeNearestPokemonAction());
            var typedResponse = response as NearestPokemonActionResponse;
            Assert.NotNull(typedResponse);
        }

        [Fact]
        public void Handle_ReturnedActionResponse_ShouldContainAction()
        {
            var action = MakeNearestPokemonAction();
            var response = _controller.Handle(action) as NearestPokemonActionResponse;

            Assert.NotNull(response);
            Assert.Equal(action, response.Action);
        }

        [Fact]
        public void Handle_ReturnedActionResponse_ShouldContainPokemonSpawnInfo()
        {
            var action = MakeNearestPokemonAction();
            var response = _controller.Handle(action) as NearestPokemonActionResponse;

            Assert.NotNull(response);
            Assert.NotNull(response.PokemonSpawn);
            Assert.True(response.PokemonSpawn.Latitude > 0);
            Assert.True(response.PokemonSpawn.Longitude > 0);
            Assert.True(response.PokemonSpawn.PokemonId > 0);
        }

        private NearestPokemonAction MakeNearestPokemonAction()
        {
            return new NearestPokemonAction()
            {
                PokemonName = "Dratini",
                Message = new Message()
                {
                    From = new User()
                    {
                        Username = "xUnit"
                    }
                }
            };
        }
    }
}