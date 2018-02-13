using System;
using Google.Protobuf.Collections;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Services;
using Moq;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using Xunit;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class GymStateActionControllerTests : ControllerTests<GymStateAction>
    {
        public GymStateActionControllerTests()
        {
            Mock<IGymIdByNameService> gymByNameServiceStub = new Mock<IGymIdByNameService>();

            gymByNameServiceStub
                .Setup(s => s.GetGymId(It.IsAny<string>()))
                .Returns<string>(gymName => gymName);

            Mock<IGymStateService> gymStateServiceStub = new Mock<IGymStateService>();

            gymStateServiceStub
                .Setup(s => s.GetGymState(It.IsAny<string>()))
                .Returns<string>(gymId => new GymState()
                {
                    Id = gymId,
                    Name = gymId,
                    OwnedByTeam = TeamColor.Red,
                    Memberships = new RepeatedField<GymMembership>()
                    {
                        new GymMembership()
                        {
                            TrainerPublicProfile = new PlayerPublicProfile()
                            {
                                Name = "Ovit"
                            },
                            PokemonData = new PokemonData()
                            {
                                PokemonId = PokemonId.Kingler,
                                Cp = 2684,
                                IndividualAttack = 14,
                                IndividualDefense = 15,
                                IndividualStamina = 15
                            }
                        }
                    }
                });

            base.Controller = new GymStateActionController(gymByNameServiceStub.Object, gymStateServiceStub.Object);
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

            Assert.NotNull(response?.GymState.Name);
            Assert.Equal(gymName, response?.GymState.Name);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithGymId()
        {
            var action = MakeAction();

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.GymState.Id);
            Assert.False(string.IsNullOrEmpty(response.GymState.Id));
            // gymStateServiceStub uses gym name as mock gym id
            Assert.Equal(action.GymName, response.GymState.Id);
        }

        [Fact]
        public void Handle_WithGymStateAction_ShouldReturnGymStateActionResponseWithTeamColor()
        {
            var action = MakeAction();

            var response = Controller.Handle(action) as GymStateActionResponse;

            Assert.NotNull(response?.GymState.OwnedByTeam);
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
