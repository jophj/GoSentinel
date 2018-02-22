using GoSentinel.Models;

namespace GoSentinel.Services.Actions
{
    public interface INearestPokemonService
    {
        SpawnPokemon GetNearest(string pokemonId, int i, int i1);
    }
}