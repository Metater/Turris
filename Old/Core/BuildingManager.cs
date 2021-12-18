using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform buildingsParent;
    [SerializeField] private List<GameObject> buildings = new List<GameObject>();
    [SerializeField] private float reach;

    private bool isBuilding = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isBuilding = !isBuilding;
            Debug.Log($"Building: {isBuilding}");
        }
        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            TryBuild();
        }
    }

    private void TryBuild()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10))
        {
            if (raycastHit.point.y != 0) return;
            if (raycastHit.distance > reach) return;
            WorldManager worldManager = GameManager.I.worldManager;
            Vector3Int cellPos = worldManager.grid.WorldToCell(raycastHit.point);
            if (Math.Abs(cellPos.x) > World.TilemapRadius || Math.Abs(cellPos.y) > World.TilemapRadius) return;
            Vector2Int worldPos = (Vector2Int)cellPos;
            if (!worldManager.world.IsWorldTile(worldPos, WorldTileType.Empty)) return;
            GameObject built = Instantiate(buildings[0], worldManager.grid.CellToWorld(cellPos) + new Vector3(worldManager.grid.cellSize.x / 2, 0f, worldManager.grid.cellSize.y / 2), Quaternion.identity, buildingsParent);
            worldManager.world.SetWorldTile(worldPos, new WallWorldTile(built));
        }
    }
}
