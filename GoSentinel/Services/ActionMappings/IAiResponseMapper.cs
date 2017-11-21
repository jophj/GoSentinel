using ApiAiSDK.Model;
using GoSentinel.Models;

namespace GoSentinel.Services.ActionMappings
{
    internal interface IAiResponseMapper<out T> where T : IAction
    {
        T Map(AIResponse response);
    }
}