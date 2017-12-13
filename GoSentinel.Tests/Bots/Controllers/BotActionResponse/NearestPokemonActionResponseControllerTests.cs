using System;
using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers.BotActionResponse;
using GoSentinel.Data;
using GoSentinel.Models;
using GoSentinel.Services.Messages;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotActionResponse
{
    public class NearestPokemonActionResponseControllerTests
    {
        private readonly NearestPokemonActionResponseController _controller;
        private readonly PokemonSpawnMessageService _spawnMessageService;

        public NearestPokemonActionResponseControllerTests()
        {
            _spawnMessageService = new PokemonSpawnMessageService();
            _controller = new NearestPokemonActionResponseController(_spawnMessageService);
        }

        [Fact]
        public void Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new AddPokemonFilterActionResponse();

            void Handle() => _controller.Handle(null, actionResponse);

            Assert.Throws<ArgumentException>((Action)Handle);
        }

        [Fact]
        public void Handle_WithNoPokemonSpawn_ShouldThrowArgumentException()
        {
            var actionResponse = MakeActionResponse();
            actionResponse.PokemonSpawn = null;

            void Handle() => _controller.Handle(null, actionResponse);

            Assert.Throws<ArgumentException>((Action)Handle);
        }

        [Fact]
        public void Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsyncWithCorrectParameters()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));
            var msg = _spawnMessageService.Generate(actionResponse);

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(
                actionResponse.Action.Message.Chat.Id,
                msg
            ), Times.Once);
        }

        [Fact]
        public void Handle_WithCorrectActionResponse_ShouldCallSendLocationAsyncWithCorrectParameters()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendLocationAsync(It.IsAny<long>(), It.IsAny<float>(), It.IsAny<float>()));

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendLocationAsync(
                actionResponse.Action.Message.Chat.Id,
                actionResponse.PokemonSpawn.Latitude,
                actionResponse.PokemonSpawn.Longitude
            ), Times.Once);
        }

        [Fact]
        public void Handle_WhenCalled_ShouldCallSendTextMessageAsyncBeforeSendLocationAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>(MockBehavior.Strict);
            var sequence = new MockSequence();
            botMock
                .InSequence(sequence)
                .Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Message());
            botMock
                .InSequence(sequence)
                .Setup(b => b.SendLocationAsync(It.IsAny<long>(), It.IsAny<float>(), It.IsAny<float>()))
                .ReturnsAsync(new Message());

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
            botMock.Verify(b => b.SendLocationAsync(
                It.IsAny<long>(),
                It.IsAny<float>(),
                It.IsAny<float>()
            ), Times.Once);

        }

        private NearestPokemonActionResponse MakeActionResponse()
        {
            return new NearestPokemonActionResponse()
            {
                PokemonSpawn = new PokemonSpawn(),
                Action = new NearestPokemonAction()
                {
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
