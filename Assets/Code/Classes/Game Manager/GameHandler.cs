using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameHandler
{
    public static GameManager gameManager;

    /// <summary>
    /// Throws System.ArgumentException if given a list of players not containing any
    /// Human players.
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="players"></param>
    /// <returns></returns>
    public static void CreateNew(string gameName, List<Player> players)
    {
        gameManager = new GameManager(gameName, players);
    }

    //Altered by JBT 
    public static void Save()
    {
        // If the save file already exists, Delete it
        if (File.Exists("\\SaveFile.sav") == false)
        {
            File.Delete("\\SaveFile.sav");
        }
        // Then save gameManager to stream
        Stream stream = File.Open("\\SaveFile.sav", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, gameManager);
        stream.Close();    
    }

    //Altered by JBT
    public static void Load()
    {
        FileStream stream = File.Open("\\SaveFile.sav", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        GameManager returnedGameManager = (GameManager)formatter.Deserialize(stream);
        stream.Close();

        gameManager = returnedGameManager;
    }

    public static GameManager GetGameManager()
    {
        return gameManager;
    }
}
