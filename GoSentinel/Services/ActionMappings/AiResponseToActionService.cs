using System;
using System.Collections.Generic;
using ApiAiSDK.Model;
using GoSentinel.Data;

namespace GoSentinel.Services.ActionMappings
{
    public interface IAiResponseToActionService
    {
        IAction Map(AIResponse aiResponse);
    }

    public class AiResponseToActionService : IAiResponseToActionService
    {
        public AiResponseToActionService()
        {
            _actionMap = new Dictionary<string, Func<AIResponse, IAction>>();
        }

        private readonly IDictionary<string, Func<AIResponse, IAction>> _actionMap;

        public void AddMapping(string actionName, Func<AIResponse, IAction> map)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException($"{nameof(actionName)} should not be null");
            }

            if (map == null)
            {
                throw new ArgumentNullException($"{nameof(map)} should not be null");
            }

            _actionMap.Add(actionName, map);
        }


        public IAction Map(AIResponse aiResponse)
        {
            if (aiResponse == null)
            {
                throw new ArgumentNullException($"{nameof(aiResponse)} should not be null");
            }

            if (aiResponse.Result == null)
            {
                throw new ArgumentException($"{nameof(aiResponse.Result)} should not be null");
            }

            if (aiResponse.IsError)
            {
                throw new ArgumentException($"{nameof(aiResponse)} is error response"); ;
            }

            if (!_actionMap.ContainsKey(aiResponse?.Result?.Action))
            {
                throw new UnmappedActionException($"{aiResponse?.Result?.Action} action is not mapped");
            }

            return _actionMap[aiResponse.Result.Action](aiResponse);
        }
    }

    public class UnmappedActionException : Exception
    {
        public UnmappedActionException(string message) : base(message)
        {}
    }
}
