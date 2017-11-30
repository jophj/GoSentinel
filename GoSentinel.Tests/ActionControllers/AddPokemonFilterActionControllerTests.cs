using System;
using System.Collections.Generic;
using System.Text;
using GoSentinel.Bots.Controllers;
using GoSentinel.Data;
using GoSentinel.Services.Actions;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.ActionControllers
{
    public class AddPokemonFilterActionControllerTests
    {
        private AddPokemonFilterActionController _controller;

        public AddPokemonFilterActionControllerTests()
        {
            _controller = new AddPokemonFilterActionController(new LogPokemonFilterActionService());
        }

        [Fact]
        public void Should_Throw_When_Argument_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Handle(null));
        }

        [Fact]
        public void Should_Throw_When_Action_Invalid()
        {
            Assert.Throws<ArgumentException>(() => _controller.Handle(new NearestPokemonAction()));
        }

        [Fact]
        public void Should_Return_Correct_ActionResponse()
        {
            var response = _controller.Handle(new AddPokemonFilterAction()
            {
                Message = new Message()
                {
                    From = new User()
                    {
                        Username = "xUnit"
                    }
                }
            });
            var typedResponse = response as AddPokemonFilterActionResponse;
            Assert.NotNull(typedResponse);
        }
    }
}
