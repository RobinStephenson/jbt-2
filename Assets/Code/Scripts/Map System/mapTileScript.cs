//Game executable hosted by JBT at: http://robins.tech/jbt/documents/assthree/GameExecutable.zip

using UnityEngine;

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
