using System;
using GoSentinel.Data;
using GoSentinel.Services;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class GymStateActionController : IActionController<GymStateAction>
    {
        private readonly IGymIdByNameService _gymIdByNameService;
        private readonly IGymStateService _gymStateService;

        public GymStateActionController(IGymIdByNameService gymIdByNameService, IGymStateService gymStateService)
        {
            _gymIdByNameService = gymIdByNameService;
            _gymStateService = gymStateService;
        }

        public IActionResponse Handle(IAction baseAction)
        {
            if (baseAction == null)
            {
                throw new ArgumentNullException();
            }

            if (!(baseAction is GymStateAction action) || string.IsNullOrEmpty(action.GymName))
            {
                throw new ArgumentException();
            }

            string gymId = _gymIdByNameService.GetGymId(action.GymName);
            var gym = _gymStateService.GetGymState(gymId);

            return new GymStateActionResponse()
            {
                Action = action,
                GymState = gym
            };
        }
    }
}
