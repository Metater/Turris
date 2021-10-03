using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WorldPathfinder
{
    private PathfindingNode[,] nodemap = new PathfindingNode[(World.TilemapRadius * 2) + 1, (World.TilemapRadius * 2) + 1];

    public WorldPathfinder(World world, Vector2Int startPos, Vector2Int finishPos)
    {
        for (int y = -World.TilemapRadius; y <= World.TilemapRadius; y++)
        {
            for (int x = -World.TilemapRadius; x <= World.TilemapRadius; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                SetPathfindingNode(pos, new PathfindingNode(pos, world.GetWorldTile(pos).walkable));
            }
        }
    }

    public void SetPathfindingNode(Vector2Int pos, PathfindingNode node)
    {
        nodemap[pos.x + World.TilemapRadius, pos.y + World.TilemapRadius] = node;
    }

    public PathfindingNode GetPathfindingNode(Vector2Int pos)
    {
        return nodemap[pos.x + World.TilemapRadius, pos.y + World.TilemapRadius];
    }
}
