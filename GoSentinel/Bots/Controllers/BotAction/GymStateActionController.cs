using System;
using GoSentinel.Data;
using GoSentinel.Services;

namespace GoSentinel.Bots.Controllers.BotAction
{
    public class GymStateActionController : IActionController<GymStateAction>
    {
        private readonly IGymByNameService _gymByNameService;

        public GymStateActionController(IGymByNameService gymByNameService)
        {
            _gymByNameService = gymByNameService;
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

            var gym = _gymByNameService.GetGym(action.GymName);

            return new GymStateActionResponse()
            {
                Action = action,
                Gym = gym
            };
        }
    }
}
