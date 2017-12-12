using System;
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
        public void Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsync()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock.Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));

            _controller.Handle(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));
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
                It.Is<long>(i => i == actionResponse.Action.Message.Chat.Id),
                It.Is<string>(e => e == msg)
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
