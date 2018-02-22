using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public interface IAiResponseMapper<out T> where T : IAction
    {
        T Map(AIResponse response);
    }
}