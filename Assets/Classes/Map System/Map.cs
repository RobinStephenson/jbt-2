using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    private List<Tile> tiles;

    public Tile GetTile(int tileId)
    {
        //TODO - Get tile from tiles based on tileId.
        return new Tile(new ResourceGroup());
    }
}
