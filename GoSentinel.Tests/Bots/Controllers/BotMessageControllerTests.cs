using System;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using GoSentinel.Services.ActionMappings;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers
{
    public class BotMessageControllerTests
    {
        private readonly BotMessageController _controller;

        private class FakeBotMessageController : BotMessageController
        {
            public string FakeActionName { get; set; }
            public IAction FakeAction { get; set; }
            public IActionResponseController ActionResponseController { get; set; }
            public IActionController ActionController { get; set; }

            public FakeBotMessageController(ApiAi apiAi, AiResponseToActionService aiResponseToActionService, IServiceProvider serviceProvider) : base(apiAi, aiResponseToActionService, serviceProvider)
            {}

            protected override AIResponse TextRequest(string messageText)
            {
                return new AIResponse()
                {
                    Result = new Result()
                    {
                        Action = FakeActionName
                    }
                };
            }

            protected override IAction MapAiResponse(AIResponse aiResponse)
            {
                return FakeAction;
            }

            protected override IActionController GetActionController(IAction action)
            {
                return ActionController;
            }

            protected override IActionResponseController GetActionResponseController(IActionResponse actionResponse)
            {
                return ActionResponseController;
            }
        }

        public BotMessageControllerTests()
        {
            _controller = new FakeBotMessageController(null, null, null);
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
            FakeBotMessageController fakeBotMessageController =
                new FakeBotMessageController(null, null, null)
                {
                    FakeActionName = Data.BotAction.InputUnknown
                };
            var botStub = new Mock<IBot>();

            Assert.Throws<UnrecognizableIntentException>(() => fakeBotMessageController.OnMessage(botStub.Object, new Message()));
        }

        [Fact]
        public void OnMessage_WithValidMessage_ShouldCallActionControllerHandle()
        {
            var botStub = new Mock<IBot>();
            var message = MakeMessage();
            var actionControllerMock = new Mock<IActionController>();
            actionControllerMock.Setup(m => m.Handle(It.IsAny<IAction>())).Returns(new AddPokemonFilterActionResponse());
            var actionResponseControllerStub = new Mock<IActionResponseController>();
            actionControllerMock.Setup(m => m.Handle(It.IsAny<IAction>())).Returns(new AddPokemonFilterActionResponse());

            var fakeBotMessageController =
                new FakeBotMessageController(null, null, null)
                {
                    FakeActionName = Data.BotAction.AddPokemonFilter,
                    ActionController = actionControllerMock.Object,
                    FakeAction = new AddPokemonFilterAction(),
                    ActionResponseController = actionResponseControllerStub.Object
                };

            fakeBotMessageController.OnMessage(botStub.Object, message);

            actionControllerMock.Verify(m => m.Handle(It.IsAny<IAction>()), Times.Once);
        }

        [Fact]
        public void OnMessage_WithValidMessage_ShouldCallActionResponseControllerHandle()
        {
            var botStub = new Mock<IBot>();
            var message = MakeMessage();
            var actionControllerStub = new Mock<IActionController>();
            actionControllerStub.Setup(m => m.Handle(It.IsAny<IAction>())).Returns(new AddPokemonFilterActionResponse());
            var actionResponseControllerMock = new Mock<IActionResponseController>();
            actionResponseControllerMock.Setup(m => m.HandleAsync(It.IsAny<IBot>(), It.IsAny<IActionResponse>())).Returns(Task.CompletedTask);

            var fakeBotMessageController =
                new FakeBotMessageController(null, null, null)
                {
                    FakeActionName = Data.BotAction.AddPokemonFilter,
                    ActionController = actionControllerStub.Object,
                    FakeAction = new AddPokemonFilterAction(),
                    ActionResponseController = actionResponseControllerMock.Object
                };

            fakeBotMessageController.OnMessage(botStub.Object, message);

            actionResponseControllerMock.Verify(m => m.HandleAsync(It.IsAny<IBot>(), It.IsAny<IActionResponse>()), Times.Once);
        }

        private Message MakeMessage()
        {
            return new Message()
            {
                Text = "Notificami Kingler 98"
            };
        }
    }
}
