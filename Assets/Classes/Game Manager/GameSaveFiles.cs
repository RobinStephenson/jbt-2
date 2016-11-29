using UnityEngine;
using System.Collections;

public static class GameSaveFiles
{
    public static void Save(GameManager gameManagerToSave)
    {
        //TODO - Serialise GameManager to binary and save.
    }

    public static GameManager Load(string gameName)
    {
        //TODO - deserialise and return GameManager object.
        return new GameManager("TEMP");
    }
}
