using UnityEngine;
using System.Collections;
using System.IO;
using System
using System.collections.generic;

public static class GameHandler
{
    public static GameManager CreateNew(string gameName, List<int> players)
    {
        return new GameManager(gameName, players);
    }

    public static void Save(GameManager gameManagerToSave, string filePath)
    {
        Stream stream = File.Open(filePath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, gameManagerToSave);
        stream.Close();
    }

    public static GameManager Load(string filePath)
    {
        stream = File.Open(filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        GameManager returnedGameManager = (GameManager)formatter.Deserialize(stream);
        stream.Close();

        return returnedGameManager;
    }
}
