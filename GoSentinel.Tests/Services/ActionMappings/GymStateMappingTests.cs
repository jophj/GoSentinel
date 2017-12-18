using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Data;
using GoSentinel.Services.ActionMappings;
using Xunit;

namespace GoSentinel.Tests.Services.ActionMappings
{
    public class GymStateMappingTests
    {
        private readonly GymStateMapping _gymStateMapping;

        public GymStateMappingTests()
        {
            _gymStateMapping = new GymStateMapping();
        }

        [Fact]
        public void Map_WhenCalled_ShouldReturnGymStateAction()
        {
            AIResponse aiResponse = MakeAIResponse();

            var action = _gymStateMapping.Map(aiResponse);

            Assert.IsType<GymStateAction>(action);
        }

        [Fact]
        public void Map_WhenCalled_ShouldReturnGymStateActionWithName()
        {
            AIResponse aiResponse = MakeAIResponse();

            var action = _gymStateMapping.Map(aiResponse);

            Assert.Equal(BotAction.GymStateRequest, action.Name);
        }

        [Theory]
        [InlineData("Madonnina")]
        [InlineData("Tabernacolo")]
        [InlineData("Madonna trollona incoronata")]
        [InlineData("路边的玛利亚")]
        public void Map_WithGymName_ShouldReturnGymNameInAction(string gymName)
        {
            AIResponse aiResponse = MakeAIResponse();
            aiResponse.Result.Parameters["GymName"] = gymName;

            var action = _gymStateMapping.Map(aiResponse);

            Assert.Equal(gymName, action.GymName);
        }

        [Fact]
        public void Map_WithNullGymName_ShouldReturnNullGymName()
        {
            AIResponse aiResponse = MakeAIResponse();
            aiResponse.Result.Parameters["GymName"] = null;

            var action = _gymStateMapping.Map(aiResponse);

            Assert.Null(action.GymName);
        }

        [Fact]
        public void Map_WithNoGymName_ShouldReturnNullGymName()
        {
            AIResponse aiResponse = MakeAIResponse();
            aiResponse.Result.Parameters.Remove("GymName");

            var action = _gymStateMapping.Map(aiResponse);

            Assert.Null(action.GymName);
        }

        private AIResponse MakeAIResponse()
        {
            return new AIResponse()
            {
                Id = new Guid().ToString(),
                Result = new Result()
                {
                    Action = BotAction.GymStateRequest,
                    Parameters = new Dictionary<string, object>()
                    {
                        { "GymName", "Madonnina di Coiano" }
                    }
                }
            };
        }
    }
}