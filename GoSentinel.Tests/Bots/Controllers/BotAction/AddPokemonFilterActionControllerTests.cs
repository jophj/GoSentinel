using System;
using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Services.Actions;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class AddPokemonFilterActionControllerTests
    {
        private readonly AddPokemonFilterActionController _controller;

        public AddPokemonFilterActionControllerTests()
        {
            _controller = new AddPokemonFilterActionController(new LogPokemonFilterService());
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

            Assert.NotNull(response);
            Assert.Equal(action, response.Action);
        }
    }
}
