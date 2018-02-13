using System;
using System.Collections.Generic;
using System.Linq;
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

            var membershipMessageLines = actionResponse.GymState.Memberships.Select((gs, i) =>
                $"{i + 1}. {gs.PokemonData.PokemonId.ToString()} {gs.PokemonData.DisplayCp} - {gs.PokemonData.OwnerName}"
            );

            foreach (string line in membershipMessageLines)
            {
                messageBuilder.AppendLine(line);
            }

            return messageBuilder.ToString();
        }
    }
}
