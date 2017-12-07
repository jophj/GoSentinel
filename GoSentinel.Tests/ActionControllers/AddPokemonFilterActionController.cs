using System;
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
        public void Handle_WithNullArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Handle(null));
        }

        [Fact]
        public void Handle_WithWrongTypeAction_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _controller.Handle(new NearestPokemonAction()));
        }

        [Fact]
        public void Handle_WhenCalled_ShouldReturnAddPokemonFilterActionResponseType()
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

        [Fact]
        public void ActionResponse_WhenReturned_ShouldContainAction()
        {
            var action = new AddPokemonFilterAction()
            {
                Stat = PokemonStat.Iv,
                ValueMin = 99,
                PokemonName = "Charmander",
                ValueMax = 100,
                Message = new Message()
                {
                    From = new User()
                    {
                        Username = "xUnit"
                    }
                }
            };
            var response = _controller.Handle(action) as AddPokemonFilterActionResponse;

            Assert.Equal(action, response.Action);
        }
    }
}
