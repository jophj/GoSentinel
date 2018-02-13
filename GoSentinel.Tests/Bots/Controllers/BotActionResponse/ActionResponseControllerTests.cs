using System;
using System.Threading.Tasks;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotActionResponse
{
    public abstract class ActionResponseControllerTests<TAR, TA> where TAR : IActionResponse<TA> where TA : IAction
    {
        protected IActionResponseController<TAR> Controller;

        protected ActionResponseControllerTests() { }

        protected ActionResponseControllerTests(IActionResponseController<TAR> controller)
        {
            Controller = controller;
        }

        [Fact]
        public async Task Handle_WithWrongTypeActionResponseArgument_ShouldThrowArgumentException()
        {
            var actionResponse = new WrongTypeActionResponse();

            Task Handle() => Controller.HandleAsync(null, actionResponse);

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

            await Controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WithCorrectActionResponse_ShouldCallSendTextMessageAsyncWithCorrectChatId()
        {
            var actionResponse = MakeActionResponse();
            var botMock = new Mock<IBot>();
            botMock
                .Setup(b => b.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()))
                .ReturnsAsync(new Message());

            await Controller.HandleAsync(botMock.Object, actionResponse);

            botMock.Verify(b => b.SendTextMessageAsync(
                It.Is<long>(i => i == actionResponse.Action.Message.Chat.Id),
                It.IsAny<string>()
            ), Times.Once);
        }

        protected abstract TAR MakeActionResponse();

        public class WrongTypeActionResponse : IActionResponse
        {
        }
    }
}