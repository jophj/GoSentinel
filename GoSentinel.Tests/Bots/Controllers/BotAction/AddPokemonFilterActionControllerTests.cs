using GoSentinel.Bots.Controllers.BotAction;
using GoSentinel.Data;
using GoSentinel.Services.Actions;
using Telegram.Bot.Types;
using Xunit;

namespace GoSentinel.Tests.Bots.Controllers.BotAction
{
    public class AddPokemonFilterActionControllerTests : ControllerTests<AddPokemonFilterAction>
    {
        public AddPokemonFilterActionControllerTests() : base(new AddPokemonFilterActionController(new LogPokemonFilterService()))
        {}

        [Fact]
        public void Handle_WhenCalled_ShouldReturnAddPokemonFilterActionResponseType()
        {
            var response = Controller.Handle(MakeAction());
            var typedResponse = response as AddPokemonFilterActionResponse;
            Assert.NotNull(typedResponse);
        }

        [Fact]
        public void ActionResponse_WhenReturned_ShouldContainAction()
        {
            var action = MakeAction();
            var response = Controller.Handle(action) as AddPokemonFilterActionResponse;

            Assert.NotNull(response);
            Assert.Equal(action, response.Action);
        }

        protected override AddPokemonFilterAction MakeAction()
        {
            return new AddPokemonFilterAction()
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
        }
    }
}
