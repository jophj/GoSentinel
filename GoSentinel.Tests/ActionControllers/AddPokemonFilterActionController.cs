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
    public class AddPokemonFilterActionController
    {
        private readonly Bots.Controllers.AddPokemonFilterActionController _controller;

        public AddPokemonFilterActionController()
        {
            _controller = new Bots.Controllers.AddPokemonFilterActionController(new LogPokemonFilterActionService());
        }

        [Fact]
        public void WhenArgumentNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Handle(null));
        }

        [Fact]
        public void WhenActionIsWrongType_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _controller.Handle(new NearestPokemonAction()));
        }

        [Fact]
        public void HandleCall_ShouldReturnAddPokemonFilterActionResponse()
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
