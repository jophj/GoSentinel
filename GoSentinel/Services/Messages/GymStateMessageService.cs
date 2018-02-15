using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoSentinel.Data;
using GoSentinel.Services.Gameplay;
using POGOProtos.Data;
using POGOProtos.Enums;

namespace GoSentinel.Services.Messages
{
    public class GymStateMessageService : IMessageService<GymStateActionResponse>
    {
        private readonly FightCountService _fightCountService;

        private readonly Dictionary<TeamColor, string> _teamColorName = new Dictionary<TeamColor, string>()
        {
            { TeamColor.Red, "Valor" },
            { TeamColor.Blue, "Mystic" },
            { TeamColor.Yellow, "Instinct" },
            { TeamColor.Neutral, "Neutral" }
        };

        public GymStateMessageService(FightCountService fightCountService)
        {
            _fightCountService = fightCountService;
        }

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
            messageBuilder.Append($"*{ actionResponse.GymState.Name}* ");
            messageBuilder.Append($"({_teamColorName[actionResponse.GymState.OwnedByTeam]})");
            messageBuilder.Append($" at {actionResponse.GymState.Timestamp}");
            messageBuilder.AppendLine();

            var membershipMessageLines = actionResponse.GymState.Memberships.Select((gs, i) =>
            {
                PokemonData pokemonData = gs.PokemonData;
                int runs = _fightCountService.Count(pokemonData.Cp, pokemonData.DisplayCp);
                return
                    $"{i + 1}. {pokemonData.PokemonId.ToString()} {pokemonData.DisplayCp} {runs} run(s) - {pokemonData.OwnerName}";
            });

            foreach (string line in membershipMessageLines)
            {
                messageBuilder.AppendLine(line);
            }

            return messageBuilder.ToString();
        }
    }
}
