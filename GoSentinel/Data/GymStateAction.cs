using GymState = GoSentinel.Models.GymState;

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
        public GymState GymState { get; set; }
    }
}
