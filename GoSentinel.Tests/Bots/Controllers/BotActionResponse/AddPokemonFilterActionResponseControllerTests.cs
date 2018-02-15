using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers.BotActionResponse;
using GoSentinel.Data;
using GoSentinel.Services.Messages;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotActionResponse
{
    public class AddPokemonFilterActionResponseControllerTests : ActionResponseControllerTests<AddPokemonFilterActionResponse, AddPokemonFilterAction>
    {
        private readonly AddPokemonFilterMessageService _messageService;

        public AddPokemonFilterActionResponseControllerTests()
        {
            _messageService = new AddPokemonFilterMessageService();
            base.Controller = new AddPokemonFilterActionResponseController(_messageService);
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

        protected override AddPokemonFilterActionResponse MakeActionResponse()
        {
            return new AddPokemonFilterActionResponse()
            {
                Action = new AddPokemonFilterAction()
                {
                    Stat = PokemonStat.Iv,
                    ValueMin = 98,
                    ValueMax = 100,
                    PokemonName = "Dratini",
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

