using Google.Protobuf.Collections;
using GoSentinel.Bots.Controllers.BotActionResponse;
using GoSentinel.Data;
using GoSentinel.Services.Messages;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using Telegram.Bot.Types;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Tests.Bots.Controllers.BotActionResponse
{
    public class GymStateActionResponseControllerTests : ActionResponseControllerTests<GymStateActionResponse, GymStateAction>
    {
        public GymStateActionResponseControllerTests()
        {
            base.Controller = new GymStateActionResponseController(new GymStateMessageService());
        }

        protected override GymStateActionResponse MakeActionResponse()
        {
            return new GymStateActionResponse()
            {
                GymState = new GymState()
                {
                    Id = "GymId",
                    Name = "GymName",
                    OwnedByTeam = TeamColor.Red,
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