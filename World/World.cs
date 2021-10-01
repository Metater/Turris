using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    private WorldTile[,] tilemap = new WorldTile[17, 17];
    public Vector2Int corePos = new Vector2Int(8, 8);
}
