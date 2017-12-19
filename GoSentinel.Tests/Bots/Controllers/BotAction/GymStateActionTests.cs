using System;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class GymStateActionTests
    {
        private readonly GymStateActionController _controller;

        public GymStateActionTests()
        {
            _controller = new GymStateActionController();
        }

        [Fact]
        public void Handle_WithNullArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Handle(null));
        }

        [Fact]
        public void Handle_WithWrongTypeAction_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _controller.Handle(new NearestPokemonAction()));
        }

        [Fact]
        public void Handle_WithNoGymName_ShouldThrowArgumentException()
        {
            var action = MakeGymAction();
            action.GymName = null;

            void Handle() => _controller.Handle(action);

            Assert.Throws<ArgumentException>((Action) Handle);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponse()
        {
            var action = MakeGymAction();

            var response = _controller.Handle(action);

            Assert.IsType<GymStateActionResponse>(response);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithAction()
        {
            var action = MakeGymAction();

            var response = _controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Action);
        }

        [Theory]
        [InlineData("Madonnina")]
        [InlineData("Tabernacolo")]
        [InlineData("Madonna trollona incoronata")]
        [InlineData("路边的玛利亚")]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithGymName(string gymName)
        {
            var action = MakeGymAction();
            action.GymName = gymName;

            var response = _controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.Gym.Name);
            Assert.Equal(gymName, response?.Gym.Name);
        }

        private GymStateAction MakeGymAction()
        {
            return new GymStateAction()
            {
                GymName = "Madonnina",
            };
        }
    }
}
