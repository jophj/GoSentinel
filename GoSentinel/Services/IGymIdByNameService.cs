namespace GoSentinel.Services
{
    public interface IGymIdByNameService
    {
        string GetGymId(string gymName);
    }

    public class FakeGymIdByNameService : IGymIdByNameService
    {
        public string GetGymId(string gymName)
        {
            return gymName;
        }
    }
}
