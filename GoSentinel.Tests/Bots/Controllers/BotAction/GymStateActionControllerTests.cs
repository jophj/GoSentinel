using System;
using System.Collections.Generic;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Models;
using GoSentinel.Services;
using Moq;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class GymStateActionControllerTests : ControllerTests<GymStateAction>
    {
        public GymStateActionControllerTests()
        {
            Mock<IGymByNameService> serviceStub = new Mock<IGymByNameService>();

            serviceStub
                .Setup(s => s.GetGym(It.IsAny<string>()))
                .Returns<string>(gymName => new Gym()
                {
                    Id = gymName + "Id",
                    Name = gymName,
                    Pokemons = new List<DefenderPokemon>()
                });

            base.Controller = new GymStateActionController(serviceStub.Object);
        }

        [Fact]
        public void Handle_WithNoGymName_ShouldThrowArgumentException()
        {
            var action = MakeAction();
            action.GymName = null;

            void Handle() => Controller.Handle(action);

            Assert.Throws<ArgumentException>((Action) Handle);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponse()
        {
            var action = MakeAction();

            var response = Controller.Handle(action);

            Assert.IsType<GymStateActionResponse>(response);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithAction()
        {
            var action = MakeAction();

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Action);
        }

        [Theory]
        [InlineData("Madonnina")]
        [InlineData("Tabernacolo")]
        [InlineData("Madonna trollona incoronata")]
        [InlineData("路边的玛利亚")]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithGymName(string gymName)
        {
            var action = MakeAction();
            action.GymName = gymName;

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Gym.Name);
            Assert.Equal(gymName, response?.Gym.Name);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithGymId()
        {
            var action = MakeAction();

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Gym.Id);
            Assert.False(string.IsNullOrEmpty(response.Gym.Id));
            Assert.Equal(action.GymName+"Id", response.Gym.Id);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithPokemons()
        {
            var action = MakeAction();

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Gym.Pokemons);
        }

        protected override GymStateAction MakeAction()
        {
            return new GymStateAction()
            {
                GymName = "Madonnina",
            };
        }
    }
}
