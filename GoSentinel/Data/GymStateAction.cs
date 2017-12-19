using GoSentinel.Models;

namespace GoSentinel.Data
{
    public class GymStateAction : BotAction
    {
        public override string Name => GymStateRequest;
        public string GymName { get; set; }
    }

    public class GymStateActionResponse : IActionResponse<GymStateAction>
    {
        public GymStateAction Action { get; set; }
        public Gym Gym { get; set; }
    }
}
