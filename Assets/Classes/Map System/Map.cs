using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    private List<Tile> tiles = new List<Tile>();
    public Vector2 MAP_DIMENSIONS = new Vector2(100, 100);

    public Tile GetTile(int tileId)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].GetId() == tileId)
            {
                return tiles[i];
            }
        }

        throw new System.IndexOutOfRangeException("Tile with index " + tileId.ToString() + " does not exist in the map.");
    }

    public int GetNumUnownedTilesRemaining()
    {
        int numTiles = 0;

        foreach(Tile tile in tiles)
        {
            if(tile.GetOwner() == null)
            {
                numTiles++;
            }
        }

        return numTiles;
    }
}
