using System;
using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.ActionResponseControllers
{
    public class AddPokemonFilterActionResponseControllerTests
    {
        private readonly AddPokemonFilterActionResponseController _controller;

        public AddPokemonFilterActionResponseControllerTests()
        {
            _controller = new AddPokemonFilterActionResponseController();
        }

        [Fact]
        public void Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new NearestPokemonActionResponse();

            void Handle() => _controller.Handle(null, actionResponse);

            Assert.Throws<ArgumentException>((System.Action) Handle);
        }

        [Fact]
        public void Handle_WhenCalled_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();

            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        private IActionResponse MakeActionResponse()
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
