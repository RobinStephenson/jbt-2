using UnityEngine;
using System.Collections;

public class TileObject
{
    /// <summary>
    /// JBT added variable to help clean up editor object layout
    /// </summary>
    private static GameObject tileHolder;
    private static GameObject TILE_GRID_GAMEOBJECT;
    private const string TILE_GRID_PREFAB_PATH = "Prefabs/Map/Tile Grid/tileGridPrefab";
    private static Color TILE_DEFAULT_COLOUR = new Color(1, 1, 1);   //White
    private static Color TILE_DEFAULT_ENEMY = new Color(1, 0, 0);    //Red
    private static Color TILE_DEFAULT_OWNED = new Color(0, 0, 1);    //Blue
    private static Color TILE_HOVER_COLOUR = new Color(1, 1, 0);     //Yellow
    private static Color TILE_SELECT_COLOUR = new Color(0, 1, 0);    //Green
    private static float TILE_HIGHLIGHT_ALPHA = 0.1f;                //Value between 0-1, which determines the strength of tile highlights, added by JBT
    private static Vector3 tileGridPrefabSize;

    private Vector2 position;
    private Vector2 size;
    private int tileId;

    /// <summary>
    /// JBT added tileCenter Gameobject to better see which tile is highlighted
    /// </summary>
    private GameObject tileGameObjectInScene;
    private GameObject tileCenter;

    public enum TILE_OWNER_TYPE
    {
        CURRENT_PLAYER,
        ENEMY,
        UNOWNED
    };

    public TileObject(int id, Vector2 position, Vector2 dimensions)
    {
        LoadTileGridGameobject();

        this.position = position;
        this.size = dimensions;
        this.tileId = id;
    }

    public Vector2 GetTilePosition()
    {
        return this.position;
    }

    /// <summary>
    /// Instantiate the tile object at its stored position and size in the current scene.
    /// Edited by JBT to help reduce the amount of clutter in the editor objects window
    /// </summary>
    public void Instantiate(Vector3 mapCenterPosition)
    {
        if(TILE_GRID_GAMEOBJECT == null)
        {
            throw new System.NullReferenceException("Attempted to instantiate a tile without a reference to the tile grid gameobject prefab."+
                                                    "Check the file path for the tile grid gameobject prefab.");
        }

        if(tileHolder == null)
        {
            tileHolder = new GameObject();
            tileHolder.transform.position = Vector3.zero;
            tileHolder.name = "Tile Holder";
            MonoBehaviour.DontDestroyOnLoad(tileHolder);
        }

        Vector3 tilePositionInScene = new Vector3(position.x * size.x * (tileGridPrefabSize.x + 0.1f), 0, position.y * size.y * (tileGridPrefabSize.z + 0.1f));
        tilePositionInScene += mapCenterPosition;
        tileGameObjectInScene = (GameObject)GameObject.Instantiate(TILE_GRID_GAMEOBJECT, tilePositionInScene, Quaternion.identity);
        MonoBehaviour.DontDestroyOnLoad(tileGameObjectInScene);             //Instantiated in the main menu scene and carried over into the game scene.
                                                                            //This removes the need for callbacks as the LoadScene function is asynchronous.

        Vector3 objectScale = tileGameObjectInScene.transform.localScale;   //Cannot assign to individual components of scale.
        objectScale.x *= size.x;
        objectScale.z *= size.y;
        tileGameObjectInScene.transform.localScale = objectScale;
        tileGameObjectInScene.name = "Tile";
        tileGameObjectInScene.AddComponent<mapTileScript>().SetTileId(tileId);
        tileGameObjectInScene.transform.parent = tileHolder.transform;
        tileCenter = tileGameObjectInScene.transform.GetChild(0).gameObject;
        tileCenter.SetActive(false);
    }

    /// <summary>
    /// JBT changed this method to support a highlightable center of the tile, to make it easier to see what color each tile is
    /// </summary>
    public void OnTileSelected()
    {
        if (tileGameObjectInScene != null)
        {
            tileCenter.SetActive(true);
            tileCenter.GetComponent<MeshRenderer>().material.color = GetTransparentColor(TILE_SELECT_COLOUR, TILE_HIGHLIGHT_ALPHA);
            tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_SELECT_COLOUR;
        }
    }

    /// <summary>
    /// JBT changed this method to support a highlightable center of the tile, to make it easier to see what color each tile is
    /// </summary>
    public void OnTileHover()
    {
        if(tileGameObjectInScene != null)
        {
            tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_HOVER_COLOUR;
            tileCenter.SetActive(true);
            tileCenter.GetComponent<MeshRenderer>().material.color = GetTransparentColor(TILE_HOVER_COLOUR, TILE_HIGHLIGHT_ALPHA);
        }
    }

    /// <summary>
    /// JBT changed this method to support a highlightable center of the tile, to make it easier to see what color each tile is
    /// </summary>
    public void OnTileNormal(TILE_OWNER_TYPE ownerType)
    {
        if (tileGameObjectInScene != null)
        {
            switch (ownerType)
            {
                case TILE_OWNER_TYPE.CURRENT_PLAYER:
                    tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_DEFAULT_OWNED;
                    tileCenter.SetActive(true);
                    tileCenter.GetComponent<MeshRenderer>().material.color = GetTransparentColor(TILE_DEFAULT_OWNED, TILE_HIGHLIGHT_ALPHA);
                    break;

                case TILE_OWNER_TYPE.ENEMY:
                    tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_DEFAULT_ENEMY;
                    tileCenter.SetActive(true);
                    tileCenter.GetComponent<MeshRenderer>().material.color = GetTransparentColor(TILE_DEFAULT_ENEMY, TILE_HIGHLIGHT_ALPHA);
                    break;

                case TILE_OWNER_TYPE.UNOWNED:
                    tileGameObjectInScene.GetComponent<MeshRenderer>().material.color = TILE_DEFAULT_COLOUR;
                    tileCenter.SetActive(false);
                    break;
            }
        }
    }

    /// <summary>
    /// JBT added method to get transparent tile colors from a passed in color and alpha value
    /// </summary>
    /// <param name="c">The opaque color</param>
    /// <param name="a">The alpha value to set it to</param>
    /// <returns>The transparent color</returns>
    private Color GetTransparentColor(Color c, float a)
    {
        Color newColor = c;
        c.a = a;
        return c;
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
