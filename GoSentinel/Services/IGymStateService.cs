using System;
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
        private readonly Random _random = new Random();

        public GymState GetGymState(string gymId)
        {
            return new GymState()
            {
                Id = gymId,
                Name = gymId,
                OwnedByTeam = (TeamColor) (_random.NextDouble() * 3 + 1),
                Timestamp = DateTime.Now,
                Memberships = new RepeatedField<GymMembership>()
                {
                    new GymMembership()
                    {
                        PokemonData = new PokemonData()
                        {
                            OwnerName = "Ovit",
                            PokemonId = PokemonId.Kingler,
                            Cp = (int) (_random.NextDouble() * 2684 + 1000),
                            DisplayCp = (int) (_random.NextDouble() * 1000),
                            IndividualAttack = 14,
                            IndividualDefense = 15,
                            IndividualStamina = 15
                        }
                    },
                    new GymMembership()
                    {
                        PokemonData = new PokemonData()
                        {
                            OwnerName = "Naashira",
                            PokemonId = PokemonId.Snorlax,
                            Cp = (int) (_random.NextDouble() * 1000 + 2000),
                            DisplayCp = (int) (_random.NextDouble() * 1000),
                            IndividualAttack = 15,
                            IndividualDefense = 15,
                            IndividualStamina = 15
                        }
                    }
                }
            };
        }
    }
}