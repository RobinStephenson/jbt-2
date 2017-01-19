using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameHandler
{
    /// <summary>
    /// Throws System.ArgumentException if given a list of players not containing any
    /// Human players.
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="players"></param>
    /// <returns></returns>
    public static GameManager CreateNew(string gameName, List<Player> players)
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
        FileStream stream;
        stream = File.Open(filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        GameManager returnedGameManager = (GameManager)formatter.Deserialize(stream);
        stream.Close();

        return returnedGameManager;
    }
}
