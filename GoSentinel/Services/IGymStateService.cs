using Google.Protobuf.Collections;
using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using GymState = GoSentinel.Models.GymState;

namespace GoSentinel.Services
{
    public interface IGymStateService
    {
        GymState GetGymState(string gymId);
    }

    public class FakeGymStateService : IGymStateService
    {
        public GymState GetGymState(string gymId)
        {
            return new GymState()
            {
                Id = gymId,
                Name = gymId,
                OwnedByTeam = TeamColor.Red,
                Memberships = new RepeatedField<GymMembership>()
                {
                    new GymMembership()
                    {
                        TrainerPublicProfile = new PlayerPublicProfile()
                        {
                            Name = "Ovit"
                        },
                        PokemonData = new PokemonData()
                        {
                            PokemonId = PokemonId.Kingler,
                            Cp = 2684,
                            IndividualAttack = 14,
                            IndividualDefense = 15,
                            IndividualStamina = 15
                        }
                    }
                }
            };
        }
    }
}