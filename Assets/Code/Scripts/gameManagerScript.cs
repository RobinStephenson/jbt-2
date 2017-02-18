﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class gameManagerScript : MonoBehaviour
{
    public const int GAME_SCENE_INDEX = 1;
    public string gameName = "game";

	// Use this for initialization
	void Start ()
    {
        //If a player is quitting to the menu from the game itself, or the end of game screen, then remove the GUI canvas that was not destroyed on load
        if (GameObject.Find("Player GUI Canvas(Clone)") != null)
        {
            Destroy(GameObject.Find("Player GUI Canvas(Clone)"));
        }

        //TODO - Implement main menu and loading/saving.
        DontDestroyOnLoad(this);

        ///TEMP - TODO - Implement player screen
        List<Player> players = new List<Player>();
        players.Add(new Human(new ResourceGroup(50, 999, 50), "Buddy", 999));
        //players.Add(new Human(new ResourceGroup(5, 8, 9), "Joe", 10));
        players.Add(new AI(new ResourceGroup(3, 2, 3), "BeepBoop", 500));
        //players.Add(new Human(new ResourceGroup(55, 8, 9), "Hugo", 10));
        //players.Add(new Human(new ResourceGroup(5, 8, 9), "Richard", 10));

        GameHandler.CreateNew(gameName, players);
        GameHandler.GetGameManager().StartGame();

        SceneManager.LoadScene(GAME_SCENE_INDEX);   //LoadScene is asynchronous
        ///
	}
}
