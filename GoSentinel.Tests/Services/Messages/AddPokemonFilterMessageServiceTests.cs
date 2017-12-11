using GoSentinel.Data;
using GoSentinel.Services.Messages;
using Xunit;

namespace GoSentinel.Tests.Services.Messages
{
    public class AddPokemonFilterMessageServiceTests
    {
        private readonly AddPokemonFilterMessageService _service;

        public AddPokemonFilterMessageServiceTests()
        {
            _service = new AddPokemonFilterMessageService();
        }

        [Fact]
        public void Generate_WithNoStat_ShouldReturnNoStatMessage()
        {
            var actionResponse = MakeActionResponse(null, null, null);

            var message = _service.Generate(actionResponse);

            Assert.Equal("Dratini aggiunto alle notifiche", message);
        }

        [Theory]
        [InlineData(PokemonStat.Iv, 90)]
        [InlineData(PokemonStat.Cp, 900)]
        [InlineData(PokemonStat.Level, 30)]
        public void Generate_WithOnlyMinValue_ShouldReturnMinValueMessage(PokemonStat stat, int? valueMin)
        {
            var actionResponse = MakeActionResponse(stat, valueMin, null);

            var message = _service.Generate(actionResponse);

            Assert.Equal($"Dratini ({stat.ToString().ToUpper()} min: {valueMin}) aggiunto alle notifiche", message);
        }

        [Theory]
        [InlineData(PokemonStat.Iv, 0)]
        [InlineData(PokemonStat.Cp, 10)]
        [InlineData(PokemonStat.Level, 1)]
        public void Generate_WithOnlyMaxValue_ShouldReturnMaxValueMessage(PokemonStat stat, int? valueMax)
        {
            var actionResponse = MakeActionResponse(stat, null, valueMax);

            var message = _service.Generate(actionResponse);

            Assert.Equal($"Dratini ({stat.ToString().ToUpper()} max: {valueMax}) aggiunto alle notifiche", message);
        }

        [Theory]
        [InlineData(PokemonStat.Iv, 0, 100)]
        [InlineData(PokemonStat.Cp, 10, 1000)]
        [InlineData(PokemonStat.Level, 1, 10)]
        public void Generate_WithMinMaxValue_ShouldReturnMinMaxValueMessage(PokemonStat stat, int? valueMin, int? valueMax)
        {
            var actionResponse = MakeActionResponse(stat, valueMin, valueMax);

            var message = _service.Generate(actionResponse);

            Assert.Equal($"Dratini ({stat.ToString().ToUpper()} min: {valueMin} max: {valueMax}) aggiunto alle notifiche", message);
        }

        private AddPokemonFilterActionResponse MakeActionResponse(PokemonStat? stat, int? valueMin, int? valueMax)
        {
            return new AddPokemonFilterActionResponse()
            {
                Action = new AddPokemonFilterAction()
                {
                    PokemonName = "Dratini",
                    Stat = stat,
                    ValueMin = valueMin,
                    ValueMax = valueMax
                }
            };
        }
    }
}
