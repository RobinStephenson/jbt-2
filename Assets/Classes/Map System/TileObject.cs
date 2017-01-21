using UnityEngine;
using System.Collections;

public class TileObject
{
    private static GameObject TILE_GRID_GAMEOBJECT;
    private const string TILE_GRID_PREFAB_PATH = "Prefabs/Map/Tile Grid/tileGridPrefab";
    private Color TILE_DEFAULT_COLOUR = new Color(1, 1, 1);   //Cyan
    private Color TILE_HOVER_COLOUR = new Color(255, 255, 0);   //Yellow
    private static Vector3 tileGridPrefabSize;

    private Vector2 position;
    private Vector2 size;
    private int tileId;

    private GameObject tileGameObjectInScene;

    public TileObject(int id, Vector2 position, Vector2 dimensions)
    {
        LoadTileGridGameobject();

        this.position = position;
        this.size = dimensions;
        this.tileId = id;
    }

    /// <summary>
    /// Instantiate the tile object at its stored position and size in the current scene.
    /// </summary>
    public void Instantiate(Vector3 mapCenterPosition)
    {
        if(TILE_GRID_GAMEOBJECT == null)
        {
            throw new System.NullReferenceException("Attempted to instantiate a tile without a reference to the tile grid gameobject prefab."+
                                                    "Check the file path for the tile grid gameobject prefab.");
        }

        Vector3 tilePositionInScene = new Vector3(position.x * size.x * tileGridPrefabSize.x, 0, position.y * size.y * tileGridPrefabSize.z);
        tilePositionInScene += mapCenterPosition;
        tileGameObjectInScene = (GameObject)GameObject.Instantiate(TILE_GRID_GAMEOBJECT, tilePositionInScene, Quaternion.identity);
        MonoBehaviour.DontDestroyOnLoad(tileGameObjectInScene);             //Instantiated in the main menu scene and carried over into the game scene.
                                                                            //This removes the need for callbacks as the LoadScene function is asynchronous.

        Vector3 objectScale = tileGameObjectInScene.transform.localScale;   //Cannot assign to individual components of scale.
        objectScale.x *= size.x;
        objectScale.z *= size.y;
        tileGameObjectInScene.transform.localScale = objectScale;

        tileGameObjectInScene.AddComponent<mapTileScript>().SetTileId(tileId);
    }

    public void OnTileHover()
    {
        if(tileGameObjectInScene != null)
        {
            tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_HOVER_COLOUR;
        }
    }
    
    public void OnTileNormal()
    {
        if (tileGameObjectInScene != null)
        {
            tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_DEFAULT_COLOUR;
        }
    }

    /// <summary>
    /// Load the tile grid gameobject from resources if it has not already been loaded.
    /// </summary>
    private void LoadTileGridGameobject()
    {
        if(TILE_GRID_GAMEOBJECT == null)
        {
            TILE_GRID_GAMEOBJECT = (GameObject)Resources.Load(TILE_GRID_PREFAB_PATH);
            tileGridPrefabSize = TILE_GRID_GAMEOBJECT.GetComponent<Renderer>().bounds.size;
        }
    }
}
