public enum EntityType : byte
{
    None,
    Player,
    BoomBox
}

// Events, Spawn, Despawn, are reliable-ordered; Snapshots are sequenced
public enum PacketType : byte
{
    // Sent by a client server saying to a client to spawn their player in
    // Received by one client
    ClientServerMessage,
    // Sent by a client server saying to a client to spawn their player in
    // Received by one client
    SpawnPlayer,
    // Sent by a client server saying any new entity spawned in
    // Received by everyone but sender, true clients only
    SpawnEntity,
    // Sent by a client server saying any entity despawned
    // Received by everyone but sender, true clients only
    DespawnEntity,
    // Sent by a client server or client
    // if (client server) Send a snapshot for all non other player entities
    // else Send a snapshot for themselves, their player only
    // Received by everyone but sender
    EntitySnapshot, // IS A TRUE CLIENT INPUT
    // Sent by a player saying they did something
    // Received by everyone but sender
    EntityEventOut, // IS A TRUE CLIENT INPUT
    // Sent by client server giving the results of something that happened in the game
    // Received by everyone but sender, true clients only
    EntityEventIn,
    // Sent by client server saying something happened with a tile in the game
    // Received by everyone but sender, true clients only
    TileEventIn,
}

public enum PacketRoutingType : byte
{
    BroadcastButToSender = 0,
    SendToClientServer = 1,
    SendToClient = 2,
}

// Comes from a client decision
public enum EntityEventOutType : byte
{
    FireBullet,
    InteractTile
}

// Comes from a client-server decision
public enum EntityEventInType : byte
{
    Hurt,
    Die
}

public enum WorldTileType : byte
{
    Empty,
    Wall,
    CannonTower,
    BatteryTower,
    LaserTower
}

public enum TileEventInType : byte
{

}
