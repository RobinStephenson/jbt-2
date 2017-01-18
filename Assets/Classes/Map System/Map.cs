using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    private List<Tile> tiles;
    public Vector2 MAP_DIMENSIONS = new Vector2(100, 100);

    public Tile GetTile(int tileId)
    {
        for(int i = 0; i < this.tiles.Count; i++)
        {
            if(this.tiles[i].GetID == tileId)
            {
                return this.tiles[i];
            }
        }
    }
}
