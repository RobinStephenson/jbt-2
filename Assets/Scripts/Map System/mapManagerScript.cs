using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class mapManagerScript : MonoBehaviour
{
    private Map map;
    private const int LEFT_MOUSE_BUTTON = 0;
    
    private Tile lastTileHovered;
    private Tile currentTileSelected;
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (map != null)
        {
            Camera mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && !eventSystem.IsPointerOverGameObject() )   //Ray hit something and cursor is not over GUI object
            {
                if (hit.collider.tag == "mapTile")
                {
                    Tile hitTile = map.GetTile(hit.collider.GetComponent<mapTileScript>().GetTileId());

                    if (hitTile != currentTileSelected)
                    {
                        if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
                        {
                            if (currentTileSelected != null)
                            {
                                currentTileSelected.TileNormal();
                            }

                            currentTileSelected = hitTile;
                            hitTile.TileSelected();
                        }
                        else
                        {
                            if (lastTileHovered != null && lastTileHovered != currentTileSelected)
                            {
                                lastTileHovered.TileNormal();
                            }

                            lastTileHovered = hitTile;
                            hitTile.TileHovered();
                        }
                    }
                }
            }
        }
	}

    public void SetMap(Map map)
    {
        this.map = map;
    }
}
