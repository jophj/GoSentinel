using System;
using GoSentinel.Bots.Controllers;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Services.Actions;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class NearestPokemonActionControllerTests : ControllerTests<NearestPokemonAction>
    {
        public NearestPokemonActionControllerTests() : base(new NearestPokemonActionController(new FakeNearestPokemonService()))
        {}

        [Fact]
        public void Handle_WithNoPokemonName_ShouldThrowArgumentException()
        {
            var action = MakeAction();
            action.PokemonName = null;

            Assert.Throws<ArgumentException>(() => Controller.Handle(action));
        }

        [Fact]
        public void Handle_WhenCalled_ShouldReturnNearestPokemonActionResponseType()
        {
            var response = Controller.Handle(MakeAction());
            var typedResponse = response as NearestPokemonActionResponse;
            Assert.NotNull(typedResponse);
        }

        [Fact]
        public void Handle_ReturnedActionResponse_ShouldContainAction()
        {
            var action = MakeAction();
            var response = Controller.Handle(action) as NearestPokemonActionResponse;

            Assert.NotNull(response);
            Assert.Equal(action, response.Action);
        }

        [Fact]
        public void Handle_ReturnedActionResponse_ShouldContainPokemonSpawnInfo()
        {
            var action = MakeAction();
            var response = Controller.Handle(action) as NearestPokemonActionResponse;

            Assert.NotNull(response);
            Assert.NotNull(response.PokemonSpawn);
            Assert.True(response.PokemonSpawn.Latitude > 0);
            Assert.True(response.PokemonSpawn.Longitude > 0);
            Assert.True(response.PokemonSpawn.PokemonId > 0);
        }

        protected override NearestPokemonAction MakeAction()
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