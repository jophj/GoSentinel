using System.Threading.Tasks;
using Google.Protobuf.Collections;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers.BotActionResponse;
using GoSentinel.Data;
using GoSentinel.Services.Messages;
using Moq;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using Telegram.Bot.Types;
using Xunit;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Tests.Bots.Controllers.BotActionResponse
{
    public class GymStateActionResponseControllerTests : ActionResponseControllerTests<GymStateActionResponse, GymStateAction>
    {
        private readonly IMessageService<GymStateActionResponse> _messageService;

        public GymStateActionResponseControllerTests()
        {
            _messageService = new GymStateMessageService();
            base.Controller = new GymStateActionResponseController(_messageService);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsyncWithGeneratedMessage()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock
                .Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Message());
            var msg = _messageService.Generate(actionResponse);

            await Controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(
                It.Is<long>(i => i == actionResponse.Action.Message.Chat.Id),
                It.Is<string>(e => e == msg)
            ), Times.Once);
        }

        protected override GymStateActionResponse MakeActionResponse()
        {
            return new GymStateActionResponse()
            {
                GymState = new GymState()
                {
                    Id = "GymId",
                    Name = "GymName",
                    OwnedByTeam = TeamColor.Red,
                    Memberships = new RepeatedField<GymMembership>()
                    {
                        new GymMembership()
                        {
                            PokemonData = new PokemonData()
                            {
                                Id = 666,
                                PokemonId = PokemonId.Kingler,
                                Cp = 2456,
                                DisplayCp = 345
                            },
                            TrainerPublicProfile = new PlayerPublicProfile()
                            {
                                Name = "Ovit",
                                TeamColor = TeamColor.Red
                            }
                        }
                    }
                },
                Action = new GymStateAction()
                {
                    GymName = "GymName",
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