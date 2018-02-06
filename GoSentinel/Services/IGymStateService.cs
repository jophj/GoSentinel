
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IGymStateService
    {
        GymState GetGymState(string gymId);
    }
}