﻿// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

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
