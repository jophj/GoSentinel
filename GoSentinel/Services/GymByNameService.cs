using GoSentinel.Models;

namespace GoSentinel.Services
{
    public interface IGymIdByNameService
    {
        string GetGymId(string gymName);
    }
}
