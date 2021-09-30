public enum EntityType : byte
{
    Player = 0,
    OtherPlayer = 1,
    WalkingBox = 2
}

public enum PacketType : byte
{
    SpawnEntity = 0,
    DespawnEntity = 1,
    EntitySnapshot = 2,
    EntityEvent = 3
}

public enum PacketRoutingType : byte
{
    BroadcastButToSender = 0,
    SendToLeader = 1,
}
