using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironementManager : MonoBehaviour
{
    public Camera cam;
    public Tilemap tilemap;
    public TileBase imuable;
    public GameObject FalllingDirt;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            BreakTile(tilemap.WorldToCell(pos));
        }
    }

    void BreakTile(Vector3Int pos)
    {
        tilemap.SetTile(pos, null);

        if (tilemap.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)) != null && !BlockIsStable(new Vector3Int(pos.x - 1, pos.y, pos.z)))
        {
            BreakTile(new Vector3Int(pos.x - 1, pos.y, pos.z));
            SpawnFallingBlock(new Vector3Int(pos.x - 1, pos.y, pos.z));
        }
        if (tilemap.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)) != null && !BlockIsStable(new Vector3Int(pos.x + 1, pos.y, pos.z)))
        {
            BreakTile(new Vector3Int(pos.x + 1, pos.y, pos.z));
            SpawnFallingBlock(new Vector3Int(pos.x + 1, pos.y, pos.z));
        }
        if (tilemap.GetTile(new Vector3Int(pos.x, pos.y + 1, pos.z)) != null && !BlockIsStable(new Vector3Int(pos.x, pos.y + 1, pos.z)))
        {
            BreakTile(new Vector3Int(pos.x, pos.y + 1, pos.z));
            SpawnFallingBlock(new Vector3Int(pos.x, pos.y + 1, pos.z));
        }
    }

    bool BlockIsStable(Vector3Int pos, int loopUnder = 0, bool checkedLeft = false, bool checkedRight = false)
    {
        if (BlockStableUnder(pos, loopUnder))
            return true;

        if (!checkedLeft && BlockStableLeft(pos))
            return true;

        if (!checkedRight && BlockStableRight(pos))
            return true;

        return false;
    }

    bool BlockStableUnder(Vector3Int pos, int loop)
    {
        if (tilemap.GetTile(pos) == imuable)
            return true;

        if (tilemap.GetTile(new Vector3Int(pos.x, pos.y - 1, pos.z)) != null && (loop > 0 || BlockIsStable(new Vector3Int(pos.x, pos.y - 1, pos.z), loop + 1)))
            return true;

        return false;
    }

    bool BlockStableLeft(Vector3Int pos)
    {
        if (tilemap.GetTile(pos) == imuable)
            return true;

        if (tilemap.GetTile(new Vector3Int(pos.x - 1, pos.y, pos.z)) != null && BlockIsStable(new Vector3Int(pos.x - 1, pos.y, pos.z), 0, false, true))
            return true;

        return false;
    }

    bool BlockStableRight(Vector3Int pos)
    {
        if (tilemap.GetTile(pos) == imuable)
            return true;

        if (tilemap.GetTile(new Vector3Int(pos.x + 1, pos.y, pos.z)) != null && BlockIsStable(new Vector3Int(pos.x + 1, pos.y, pos.z), 0, true, false))
            return true;

        return false;
    }

    void SpawnFallingBlock(Vector3Int pos)
    {
        Vector3 position = tilemap.CellToWorld(pos);
        position.x += .5f;
        position.y -= .5f;
        Instantiate(FalllingDirt, position, Quaternion.identity);
    }

    public TileBase GetDirtTile(Vector3Int pos)
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(pos.x, pos.y - 1, pos.z));

        if(tile != imuable)
        {
            int index = int.Parse(tile.name.Substring(5)) - ((int)ITileType.Dirt + 1);
            return (TileBase)Resources.Load("Dirt_" + index);
        }
        else
        {
            tile = tilemap.GetTile(new Vector3Int(pos.x, pos.y - 2, pos.z));
            int index = int.Parse(tile.name.Substring(5));
            return (TileBase)Resources.Load("Dirt_" + index);
        }
    }
}
