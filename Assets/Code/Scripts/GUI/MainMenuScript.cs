using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;



public class mainMenuScript : MonoBehaviour
{
    public Toggle AIToggle;
    public InputField Player1Name;
    public InputField Player2Name;
    public const int GAME_SCENE_INDEX = 1;
    public const string AIPlayerName = "Bot";
    public string gameName = "game";

    public void StartGame()
    {
        //If a player is quitting to the menu from the game itself, or the end of game screen, then remove the GUI canvas that was not destroyed on load
        if (GameObject.Find("Player GUI Canvas(Clone)") != null)
        {
            Destroy(GameObject.Find("Player GUI Canvas(Clone)"));
        }

        //TODO - BALANCE PLAYER RESOURCES
        List<Player> players = new List<Player>();
        if (AIToggle.isOn)
        {
            players.Add(new Human(new ResourceGroup(50, 999, 50), Player1Name.text, 999));
            players.Add(new AI(new ResourceGroup(3, 2, 3), AIPlayerName, 500));
        }
        else
        {
            players.Add(new Human(new ResourceGroup(5, 8, 9), Player1Name.text, 10));
            players.Add(new Human(new ResourceGroup(50, 999, 50), Player2Name.text, 999));
        }

        GameHandler.CreateNew(gameName, players);
        GameHandler.GetGameManager().StartGame();
            
        SceneManager.LoadScene(GAME_SCENE_INDEX);   //LoadScene is asynchronous   

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleAI()
    {
        if (AIToggle.isOn)
        {
            Player2Name.text = AIPlayerName;
            Player2Name.interactable = false;
        }
        else
        {
            Player2Name.text = "Enter Name Here...";
            Player2Name.interactable = true;
        }
    }
}