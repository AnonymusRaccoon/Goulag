using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTexturer : EditorWindow
{
    ITileType tileType;
    Tilemap tilemap;
    TileBase tile;
    

    [MenuItem("Window/TileTexturer")]
    static void Init()
    {
        TileTexturer window = (TileTexturer)GetWindow(typeof(TileTexturer));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Texturer", EditorStyles.boldLabel);
        tileType = (ITileType)EditorGUILayout.EnumPopup("Texture Type", tileType);
        tilemap = (Tilemap)EditorGUILayout.ObjectField("Tilemap", tilemap, typeof(Tilemap), true);
        tile = (TileBase)EditorGUILayout.ObjectField("Tile to Replace", tile, typeof(TileBase), false);


        if (GUILayout.Button("Texturize"))
        {
            BoundsInt bounds = tilemap.cellBounds;

            string tilePrefix = "Assets/Tiles/" + tileType.ToString() + "/Resources/" + tileType.ToString() + "_";

            for (int x = 0; x < (int)tileType; x++)
            {
                for (int y = 0; y < (int)tileType; y++)
                {
                    Vector3Int pos = new Vector3Int(x + bounds.xMin, bounds.yMax - y, 0);
                    if (tilemap.GetTile(pos) == tile)
                    {
                        int index = x + y * ((int)tileType + 1);
                        TileBase newTile = (TileBase)EditorGUIUtility.Load(tilePrefix + index + ".asset");
                        tilemap.SetTile(pos, newTile);
                    }
                }
            }
        }
    }
}
