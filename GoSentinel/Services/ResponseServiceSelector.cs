using System;
using GoSentinel.Models;

namespace GoSentinel.Services
{
    public class ResponseServiceSelector : IResponseServiceSelector
    {
        private readonly IServiceProvider _serviceProvider;

        public ResponseServiceSelector(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IActionResponseServiceBase GetService<T>(T actionResponse) where T : IActionResponse
        {
            Type actionResponseServiceType = typeof(IActionResponseService<>).MakeGenericType(actionResponse.GetType());
            IActionResponseServiceBase asd = (IActionResponseServiceBase) _serviceProvider.GetService(actionResponseServiceType);
            return asd;
        }
    }
}
