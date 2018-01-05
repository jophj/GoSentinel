using System;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class GymStateActionTests : ControllerTests<GymStateAction>
    {
        public GymStateActionTests() : base(new GymStateActionController())
        {}

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

        protected override GymStateAction MakeAction()
        {
            return new GymStateAction()
            {
                GymName = "Madonnina",
            };
        }
    }
}
