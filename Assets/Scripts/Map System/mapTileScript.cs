using UnityEngine;
using System.Collections;

public class mapTileScript : MonoBehaviour
{
    private int tileId;
    
    public int GetTileId()
    {
        return tileId;
    }

    public void SetTileId(int id)
    {
        tileId = id;
    }
}
