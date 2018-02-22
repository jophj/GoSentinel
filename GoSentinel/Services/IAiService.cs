using ApiAiSDK;
using ApiAiSDK.Model;

namespace GoSentinel.Services
{
    public interface IAiService
    {
        AIResponse TextRequest(string messageText);
    }

    public class ApiAiService : IAiService
    {
        private readonly ApiAi _apiApi;

        public ApiAiService(ApiAi apiApi)
        {
            _apiApi = apiApi;
        }

        public AIResponse TextRequest(string text)
        {
            return _apiApi.TextRequest(text);
        }
    }
}
