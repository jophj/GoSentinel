using System;
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
        public void Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new NearestPokemonActionResponse();

            void Handle() => _controller.Handle(null, actionResponse);

            Assert.Throws<ArgumentException>((Action) Handle);
        }

        [Fact]
        public void Handle_WhenCalled_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();

            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));
            var msg = _messageService.Generate(actionResponse);

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.Is<string>(e => e == msg)), Times.Once);
        }

        private AddPokemonFilterActionResponse MakeActionResponse()
        {
            return new AddPokemonFilterActionResponse()
            {
                BotAction = new AddPokemonFilterBotAction()
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

