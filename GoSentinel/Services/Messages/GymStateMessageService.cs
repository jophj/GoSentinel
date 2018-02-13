using System;
using System.Collections.Generic;
using System.Text;
using GoSentinel.Data;
using POGOProtos.Enums;

namespace GoSentinel.Services.Messages
{
    public class GymStateMessageService : IMessageService<GymStateActionResponse>
    {
        private readonly Dictionary<TeamColor, string> _teamColorEmoji = new Dictionary<TeamColor, string>()
        {
            { TeamColor.Red, ":red_hearth:"},
            { TeamColor.Blue, ":blue_hearth:"},
            { TeamColor.Yellow, ":yellow_hearth:"},
            { TeamColor.Neutral, ":white_circle:" }
        };

        public string Generate(GymStateActionResponse actionResponse)
        {
            if (actionResponse == null)
            {
                throw new ArgumentNullException();
            }

            if (actionResponse.GymState == null)
            {
                throw new ArgumentException("Gym state should not be null");
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.Append(_teamColorEmoji[actionResponse.GymState.OwnedByTeam]);
            messageBuilder.Append($" *{ actionResponse.GymState.Name}*");
            messageBuilder.Append($" at {actionResponse.GymState.Timestamp}");
            messageBuilder.AppendLine();



            return messageBuilder.ToString();
        }
    }
}
