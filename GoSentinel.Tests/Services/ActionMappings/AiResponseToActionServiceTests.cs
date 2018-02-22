using System;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services.ActionMappings;
using Moq;
using Xunit;

namespace GoSentinel.Tests.Services.ActionMappings
{
    public class AiResponseToActionServiceTests
    {
        private readonly AiResponseToActionService _service;

        public AiResponseToActionServiceTests()
        {
            _service = new AiResponseToActionService();
        }

        [Fact]
        public void AddMapping_WithNullMapping_ShouldThrowArgumentNullException()
        {
            void Act() => _service.AddMapping(BotAction.AddPokemonFilter, null);

            Assert.Throws<ArgumentNullException>((Action) Act);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddMapping_WithNullOrEmpyActionName_ShouldThrowArgumentException(string actionName)
        {
            void Act() => _service.AddMapping(actionName, new PokemonFilterMapping().Map);

            Assert.Throws<ArgumentException>((Action)Act);
        }

        [Fact]
        public void Map_WithNullAiResponse_ShouldThrowArgumentNullException()
        {
            void Act() => _service.Map(null);

            Assert.Throws<ArgumentNullException>((Action) Act);
        }

        [Fact]
        public void Map_WithNullAiResponseResult_ShouldThrowArgumentNullException()
        {
            void Act() => _service.Map(new AIResponse());

            Assert.Throws<ArgumentException>((Action)Act);
        }

        [Fact]
        public void Map_WithMappedResponseAction_ShouldCallTheCorrectMapping()
        {
            var mappingMock = new Mock<IAiResponseMapper<IAction>>();
            mappingMock.Setup(m => m.Map(It.IsAny<AIResponse>()));
            _service.AddMapping("mappingMock", mappingMock.Object.Map);
            var aiResponse = new AIResponse()
            {
                Result = new Result()
                {
                    Action = "mappingMock"
                }
            };

            _service.Map(aiResponse);

            mappingMock.Verify(m => m.Map(It.Is<AIResponse>(p => p == aiResponse)), Times.Once);
        }

        [Fact]
        public void Map_WithUnmappedResponseAction_ShouldThrowUnmappedActionException()
        {
            var mappingMock = new Mock<IAiResponseMapper<IAction>>();
            mappingMock.Setup(m => m.Map(It.IsAny<AIResponse>()));
            _service.AddMapping("notMappedAction", mappingMock.Object.Map);
            var aiResponse = new AIResponse()
            {
                Result = new Result()
                {
                    Action = "mappedAction"
                }
            };

            void Act() => _service.Map(aiResponse);

            Assert.Throws<UnmappedActionException>((Action) Act);
        }
    }
}
