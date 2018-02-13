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
        public async Task Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new AddPokemonFilterActionResponse();

            Task Handle() => _controller.HandleAsync(null, actionResponse);

            await Assert.ThrowsAsync<ArgumentException>(Handle);
        }

        [Fact]
        public async Task Handle_WithNoPokemonSpawn_ShouldThrowArgumentException()
        {
            var actionResponse = MakeActionResponse();
            actionResponse.SpawnPokemon = null;

            Task Handle() => _controller.HandleAsync(null, actionResponse);

            await Assert.ThrowsAsync<ArgumentException>(Handle);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));

            await _controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsyncWithCorrectParameters()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));
            var msg = _spawnMessageService.Generate(actionResponse);

            await _controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(
                actionResponse.Action.Message.Chat.Id,
                msg
            ), Times.Once);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendLocationAsyncWithCorrectParameters()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendLocationAsync(It.IsAny<long>(), It.IsAny<float>(), It.IsAny<float>()));

            await _controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendLocationAsync(
                actionResponse.Action.Message.Chat.Id,
                actionResponse.SpawnPokemon.Latitude,
                actionResponse.SpawnPokemon.Longitude
            ), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenCalled_ShouldCallSendTextMessageAsyncBeforeSendLocationAsync()
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

            await _controller.HandleAsync(botMock.Object, actionResponse);

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
                SpawnPokemon = new SpawnPokemon(),
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
