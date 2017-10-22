using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IPokemonFilterService
    {
        void Add(AddPokemonFilterAction action);
    }
}