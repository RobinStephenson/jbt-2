using UnityEngine;
using System.Collections;
using System.IO;
using System

public static class GameSaveFiles
{
    public static void Save(GameManager gameManagerToSave)
    {
        Stream stream = File.Open("SaveFile.sav", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, gameManagerToSave);
        stream.Close();
    }

    public static GameManager Load(string gameName)
    {
        stream = File.Open("SaveFile.sav", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        GameManager returnedGameManager = (GameManager)formatter.Deserialize(stream);
        stream.Close();

        return returnedGameManager;
    }
}
