using System;
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
    public class AddPokemonFilterActionResponseControllerTests
    {
        private readonly AddPokemonFilterActionResponseController _controller;
        private readonly AddPokemonFilterMessageService _messageService;

        public AddPokemonFilterActionResponseControllerTests()
        {
            _messageService = new AddPokemonFilterMessageService();
            _controller = new AddPokemonFilterActionResponseController(_messageService);
        }

        [Fact]
        public async Task Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new NearestPokemonActionResponse();

            Task Handle() => _controller.HandleAsync(null, actionResponse);

            await Assert.ThrowsAsync<ArgumentException>(Handle);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock
                .Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Message());

            await _controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsyncWithCorrectParameters()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock
                .Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Message());
            var msg = _messageService.Generate(actionResponse);

            await _controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(
                It.Is<long>(i => i == actionResponse.Action.Message.Chat.Id),
                It.Is<string>(e => e == msg)
                ), Times.Once);
        }

        private AddPokemonFilterActionResponse MakeActionResponse()
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

