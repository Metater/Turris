using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class WorldTile
{
    public bool walkable;
}

public class NoneWorldTile : WorldTile
{
    public NoneWorldTile()
    {
        walkable = true;
    }
}

public class TowerWorldTile : WorldTile
{
    public TowerWorldTile()
    {
        walkable = false;
    }
}

