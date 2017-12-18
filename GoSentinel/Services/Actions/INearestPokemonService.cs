using GoSentinel.Models;

namespace GoSentinel.Services.Actions
{
    public interface INearestPokemonService
    {
        PokemonSpawn GetNearest(string pokemonId, int i, int i1);
    }
}