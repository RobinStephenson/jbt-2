using UnityEngine;
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
        //TODO - Implement main menu and loading/saving.
        DontDestroyOnLoad(this);

        ///TEMP
        List<Player> players = new List<Player>();
        players.Add(new AI(new ResourceGroup(), 0));
        players.Add(new Human(new ResourceGroup(50, 999, 50), 999));
        players.Add(new Human(new ResourceGroup(), 0));
        players.Add(new AI(new ResourceGroup(), 0));

        GameHandler.CreateNew(gameName, players);
        GameHandler.GetGameManager().StartGame();

        SceneManager.LoadScene(GAME_SCENE_INDEX);   //LoadScene is asynchronous
        ///
	}
}
