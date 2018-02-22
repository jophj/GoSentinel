using System;
using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.Collections;
using POGOProtos.Data.Gym;
using POGOProtos.Enums;

namespace GoSentinel.Models
{
    public class GymState
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RepeatedField<GymMembership> Memberships { get; set; }
        public TeamColor OwnedByTeam { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
