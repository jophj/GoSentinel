using System;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Services.ActionMappings;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers
{
    public class BotMessageControllerTests
    {
        private readonly BotMessageController _controller;

        private class BotMessageControllerMock : BotMessageController
        {
            public BotMessageControllerMock(ApiAi apiAi, AiResponseToActionService aiResponseToActionService, IServiceProvider serviceProvider) : base(apiAi, aiResponseToActionService, serviceProvider)
            {
            }

            protected override AIResponse TextRequest(string messageText)
            {
                return new AIResponse()
                {
                    Result = new Result()
                    {
                        Action = "input.unknown"
                    }
                };
            }
        }

        public BotMessageControllerTests()
        {
            _controller = new BotMessageController(null, null, null);
        }

        [Fact]
        public void OnMessage_WithNullMessage_ShouldThrowArgumentException()
        {
            var botStub = new Mock<IBot>();

            Assert.Throws<ArgumentException>(() => _controller.OnMessage(botStub.Object, null));
        }

        [Fact]
        public void OnMessage_WithNullBot_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _controller.OnMessage(null, new Message()));
        }

        [Fact]
        public void OnMessage_WithUnrecognazibleMessage_ShouldThrowUnrecognizableIntentException()
        {
            BotMessageControllerMock botMessageController = new BotMessageControllerMock(null, null, null);
            var botMock = new Mock<IBot>();

            Assert.Throws<UnrecognizableIntentException>(() => botMessageController.OnMessage(botMock.Object, new Message()));
        }
    }
}
