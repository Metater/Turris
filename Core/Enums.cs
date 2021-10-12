public enum EntityType : byte
{
    None,
    Player,
    BoomBox,
    CannonTower,
}

// need a way to say client left to client server

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