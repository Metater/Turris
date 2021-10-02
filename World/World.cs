using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public const int TilemapRadius = 10;

    public Vector2Int corePos = new Vector2Int(TilemapRadius, TilemapRadius);

    private WorldTile[,] tilemap = new WorldTile[(TilemapRadius * 2) + 1, (TilemapRadius * 2) + 1];

    public World()
    {
        for (int y = -TilemapRadius; y <= TilemapRadius; y++)
            for (int x = -TilemapRadius; x <= TilemapRadius; x++)
                SetWorldTile(new Vector2Int(x, y), new EmptyWorldTile());
    }

    public void SetWorldTile(Vector2Int pos, WorldTile worldTile, bool dispose = true)
    {
        if (worldTile != null && dispose) worldTile.Dispose();
        tilemap[pos.x + TilemapRadius, pos.y + TilemapRadius] = worldTile;
    }
    public WorldTile GetWorldTile(Vector2Int pos)
    {
        return tilemap[pos.x + TilemapRadius, pos.y + TilemapRadius];
    }
    public bool IsWorldTile(Vector2Int pos, WorldTileType worldTileType)
    {
        return GetWorldTile(pos).worldTileType == worldTileType;
    }
}
