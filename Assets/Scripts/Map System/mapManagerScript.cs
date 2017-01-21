using UnityEngine;
using System.Collections;

public class mapManagerScript : MonoBehaviour
{
    private Map map;
    private const int LEFT_MOUSE_BUTTON = 0;

    private Tile lastTileHovered;
	
	// Update is called once per frame
	void Update ()
    {
        if (map != null)
        {
            Camera mainCamera = Camera.main;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))   //Ray hit something
            {
                if (hit.collider.tag == "mapTile")
                {
                    Tile hitTile = map.GetTile(hit.collider.GetComponent<mapTileScript>().GetTileId());

                    if (lastTileHovered != null)
                    {
                        lastTileHovered.TileNormal();
                    }

                    lastTileHovered = hitTile;

                    if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
                    {
                        hitTile.TileSelected();
                    }
                    else
                    {
                        hitTile.TileHovered();
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
