using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PathfindingNode
{
    public readonly Vector2Int pos;
    public readonly bool walkable;

    public int g, h, f;
    public PathfindingNode parentNode;

    public PathfindingNode(Vector2Int pos, bool walkable)
    {
        this.pos = pos;
        this.walkable = walkable;
    }
}