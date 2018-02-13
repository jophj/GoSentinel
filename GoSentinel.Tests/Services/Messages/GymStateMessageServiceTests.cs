using System;
using Google.Protobuf.Collections;
using GoSentinel.Data;
using GoSentinel.Services.Messages;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using Telegram.Bot.Types;
using Xunit;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Tests.Services.Messages
{
    public class GymStateMessageServiceTests
    {
        private readonly GymStateMessageService _service;

        public GymStateMessageServiceTests()
        {
            _service = new GymStateMessageService();
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
            Assert.Equal($":red_hearth: *{actionResponse.GymState.Name}* at {actionResponse.GymState.Timestamp}", lines[0]);
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
                                DisplayCp = 345
                            },
                            TrainerPublicProfile = new PlayerPublicProfile()
                            {
                                Name = "Ovit",
                                TeamColor = TeamColor.Red
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