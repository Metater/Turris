using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WorldTile
{
    public WorldTileType worldTileType;
    public bool walkable;
    public abstract void Dispose();
}

public abstract class BuildingWorldTile : WorldTile
{
    public GameObject building;
}

public class EmptyWorldTile : WorldTile
{
    public EmptyWorldTile()
    {
        worldTileType = WorldTileType.Empty;
        walkable = true;
    }

    public override void Dispose()
    {

    }
}

public class WallWorldTile : BuildingWorldTile
{
    public WallWorldTile(GameObject building)
    {
        worldTileType = WorldTileType.Wall;
        walkable = false;
        this.building = building;
    }

    public override void Dispose()
    {

    }
}

public class CannonTowerWorldTile : BuildingWorldTile
{
    public CannonTowerWorldTile(GameObject building)
    {
        worldTileType = WorldTileType.CannonTower;
        walkable = false;
        this.building = building;
    }

    public override void Dispose()
    {

    }
}

public class BatteryTowerWorldTile : BuildingWorldTile
{
    public BatteryTowerWorldTile(GameObject building)
    {
        worldTileType = WorldTileType.BatteryTower;
        walkable = false;
        this.building = building;
    }

    public override void Dispose()
    {

    }
}

public class LaserTowerWorldTile : BuildingWorldTile
{
    public LaserTowerWorldTile(GameObject building)
    {
        worldTileType = WorldTileType.LaserTower;
        walkable = false;
        this.building = building;
    }

    public override void Dispose()
    {

    }
}
