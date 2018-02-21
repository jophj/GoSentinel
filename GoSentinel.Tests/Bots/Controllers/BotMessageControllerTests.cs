using System;
using System.Threading.Tasks;
using ApiAiSDK.Model;
using GoSentinel.Bots;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using GoSentinel.Services;
using GoSentinel.Services.ActionMappings;
using Moq;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers
{
    public class BotMessageControllerTests
    {
        private readonly BotMessageController _controller;
        private readonly Mock<IAiResponseToActionService> _aiResponseToActionServiceMock;

        public BotMessageControllerTests()
        {
            _aiResponseToActionServiceMock = new Mock<IAiResponseToActionService>();
            _controller =
                new BotMessageController(null, null, null);
        }

        [Fact]
        public void OnMessage_WithNullMessage_ShouldThrowArgumentException()
        {
            var botStub = new Mock<IBot>();

            void Act() => _controller.OnMessage(botStub.Object, null);

            Assert.Throws<ArgumentException>((Action) Act);
        }

        [Fact]
        public void OnMessage_WithNullBot_ShouldThrowArgumentException()
        {
            void Act() => _controller.OnMessage(null, new Message());

            Assert.Throws<ArgumentException>((Action) Act);
        }

        [Fact]
        public void OnMessage_WithUnrecognazibleMessage_ShouldThrowUnrecognizableIntentException()
        {
            var aiServiceStub = new Mock<IAiService>();
            aiServiceStub
                .Setup(s => s.TextRequest(It.IsAny<string>()))
                .Returns(MakeUnrecognizableAiResponse());
            var controllerMock = new Mock<BotMessageController>(aiServiceStub.Object, null, null);
            var botStub = new Mock<IBot>();

            void Act() => controllerMock.Object.OnMessage(botStub.Object, new Message());

            Assert.Throws<UnrecognizableIntentException>((Action) Act);
        }

        [Fact]
        public void OnMessage_WithValidMessage_ShouldCallActionControllerHandle()
        {
            var botStub = new Mock<IBot>();
            var aiServiceStub = new Mock<IAiService>();
            aiServiceStub
                .Setup(s => s.TextRequest(It.IsAny<string>()))
                .Returns(MakeAiResponse());
            var actionControllerMock = new Mock<IActionController>();
            actionControllerMock.Setup(m => m.Handle(It.IsAny<IAction>())).Returns(new AddPokemonFilterActionResponse());
            var message = MakeMessage();
            var controllerMock = MakeControllerMock(
                aiServiceStub.Object,
                actionControllerMock.Object,
                null
            );

            controllerMock.Object.OnMessage(botStub.Object, message);

            actionControllerMock.Verify(m => m.Handle(It.IsAny<IAction>()), Times.Once);
        }

        [Fact]
        public void OnMessage_WithValidMessage_ShouldCallActionResponseControllerHandle()
        {
            var botStub = new Mock<IBot>();
            var aiServiceStub = new Mock<IAiService>();
            aiServiceStub
                .Setup(s => s.TextRequest(It.IsAny<string>()))
                .Returns(MakeAiResponse());
            var actionResponseControllerMock = new Mock<IActionResponseController>();
            actionResponseControllerMock
                .Setup(m => m.HandleAsync(It.IsAny<IBot>(), It.IsAny<IActionResponse>()))
                .Returns(Task.CompletedTask);
            var message = MakeMessage();
            var controllerMock = MakeControllerMock(
                aiServiceStub.Object,
                null,
                actionResponseControllerMock.Object
            );

            controllerMock.Object.OnMessage(botStub.Object, message);

            actionResponseControllerMock.Verify(m => m.HandleAsync(It.IsAny<IBot>(), It.IsAny<IActionResponse>()), Times.Once);
        }

        private Mock<BotMessageController> MakeControllerMock(
            IAiService aiService,
            IActionController actionController,
            IActionResponseController actionResponseController
        )
        {
            var controllerMock = new Mock<BotMessageController>(aiService, null, null);
            controllerMock
                .Setup(m => m.GetActionController(It.IsAny<IAction>()))
                .Returns(actionController);
            controllerMock
                .Setup(m => m.GetActionResponseController(It.IsAny<IActionResponse>()))
                .Returns(actionResponseController);
            controllerMock
                .Setup(m => m.MapAiResponse(It.IsAny<AIResponse>()))
                .Returns(new AddPokemonFilterAction());

            return controllerMock;
        }

        [Fact]
        public void MapAiResponse_WhenCalled_ShouldCallMap()
        {
            _aiResponseToActionServiceMock.ResetCalls();
            var controller = new BotMessageController(
                null,
                _aiResponseToActionServiceMock.Object,
                null
            );

            controller.MapAiResponse(null);

            _aiResponseToActionServiceMock.Verify(m => m.Map(It.IsAny<AIResponse>()), Times.Once);
        }

        private Message MakeMessage()
        {
            return new Message()
            {
                Text = "Notificami Kingler 98"
            };
        }

        private AIResponse MakeAiResponse()
        {
            return new AIResponse()
            {
                Result = new Result()
                {
                    Action = Data.BotAction.AddPokemonFilter
                }
            };
        }

        private AIResponse MakeUnrecognizableAiResponse()
        {
            return new AIResponse()
            {
                Result = new Result()
                {
                    Action = Data.BotAction.InputUnknown
                }
            };
        }
    }
}
