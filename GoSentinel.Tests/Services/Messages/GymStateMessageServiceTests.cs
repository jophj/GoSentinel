using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using GoSentinel.Data;
using GoSentinel.Services.Gameplays;
using GoSentinel.Services.Messages;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Enums;
using Telegram.Bot.Types;
using Xunit;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Tests.Services.Messages
{
    public class GymStateMessageServiceTests
    {
        private readonly Dictionary<TeamColor, string> _teamColorNames = new Dictionary<TeamColor, string>()
        {
            { TeamColor.Red, "Valor" },
            { TeamColor.Blue, "Mystic" },
            { TeamColor.Yellow, "Instinct" },
            { TeamColor.Neutral, "Neutral" }
        };

        private readonly GymStateMessageService _service;

        public GymStateMessageServiceTests()
        {
            _service = new GymStateMessageService(new FightCountService());
        }

        [Fact]
        public void Generate_WithNullArgument_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Generate(null));
        }

        [Fact]
        public void Generate_WithNoGymState_ShouldThrowArgumentException()
        {
            var actionResponse = MakeActionResponse();
            actionResponse.GymState = null;

            Assert.Throws<ArgumentException>(() => _service.Generate(actionResponse));
        }

        [Fact]
        public void Generate_WithCompleteGymState_ShoulHaveFormattedFirstLine()
        {
            var actionResponse = MakeActionResponse();

            var message = _service.Generate(actionResponse);

            var lines = message.Split(Environment.NewLine);
            Assert.Equal(
                $"*{actionResponse.GymState.Name}* ({_teamColorNames[actionResponse.GymState.OwnedByTeam]}) at {actionResponse.GymState.Timestamp}",
                lines[0]
                );
        }

        [Theory]
        [InlineData(TeamColor.Red)]
        [InlineData(TeamColor.Blue)]
        [InlineData(TeamColor.Yellow)]
        [InlineData(TeamColor.Neutral)]
        public void Generate_WithDifferentGymColor_ShouldUseCorrectTeamName(TeamColor teamColor)
        {
            var actionResponse = MakeActionResponse();
            actionResponse.GymState.OwnedByTeam = teamColor;

            var message = _service.Generate(actionResponse);

            var lines = message.Split(Environment.NewLine);
            Assert.Contains(_teamColorNames[teamColor], lines[0]);
        }

        [Fact]
        public void Generate_WithGymMembers_ShouldHaveALineForEachMember()
        {
            var actionResponse = MakeActionResponse();

            var message = _service.Generate(actionResponse);

            var lineCount = message.Trim().Split(Environment.NewLine).Length;
            Assert.Equal(lineCount - 1, actionResponse.GymState.Memberships.Count);
        }

        [Fact]
        public void Generate_WithGymMembers_ShouldHaveFormattedLineForEachMember()
        {
            var actionResponse = MakeActionResponse();

            var message = _service.Generate(actionResponse);

            var lines = message.Trim().Split(Environment.NewLine).Skip(1);
            int i = 0;
            foreach (string line in lines)
            {
                PokemonData pokemonData = actionResponse.GymState.Memberships[i].PokemonData;
                int runs = new FightCountService().Count(pokemonData.Cp, pokemonData.DisplayCp);

                string expectedLine = $"{i + 1}. ";
                expectedLine += $"{pokemonData.PokemonId.ToString()} ";
                expectedLine += $"{pokemonData.DisplayCp} {runs} run(s) - ";
                expectedLine += $"{pokemonData.OwnerName}";
                Assert.Equal(expectedLine, line);
                i += 1;
            }
        }

        protected GymStateActionResponse MakeActionResponse()
        {
            return new GymStateActionResponse()
            {
                GymState = new GymState()
                {
                    Id = "GymId",
                    Name = "GymName",
                    OwnedByTeam = TeamColor.Red,
                    Timestamp = DateTime.Now,
                    Memberships = new RepeatedField<GymMembership>()
                    {
                        new GymMembership()
                        {
                            PokemonData = new PokemonData()
                            {
                                Id = 666,
                                PokemonId = PokemonId.Kingler,
                                Cp = 2456,
                                DisplayCp = 345,
                                OwnerName = "Ovit"
                            }
                        },
                        new GymMembership()
                        {
                            PokemonData = new PokemonData()
                            {
                                Id = 666,
                                PokemonId = PokemonId.Snorlax,
                                Cp = 3456,
                                DisplayCp = 423,
                                OwnerName = "Naashira"
                            }
                        }
                    }
                },
                Action = new GymStateAction()
                {
                    GymName = "GymName",
                    Message = new Message()
                    {
                        Chat = new Chat()
                        {
                            Id = 0
                        }
                    }
                }
            };
        }
    }
}
