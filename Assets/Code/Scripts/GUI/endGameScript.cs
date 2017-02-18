using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//Made by JBT
public class endGameScript : MonoBehaviour
{
    public static List<ScoreboardEntry> Scoreboard;

    public Text WinnerText;
    public Text LoserText;
    public Text WinnerScore;
    public Text LoserScore;

    void Start()
    {
        //Destroy objects that got carried over from the main game
        Destroy(GameObject.Find("Player GUI Canvas(Clone)"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Tile Holder"));
        Destroy(GameObject.Find("Map Manager"));

        GameObject newEventSystem = new GameObject();
        newEventSystem.AddComponent<EventSystem>();
        newEventSystem.AddComponent<StandaloneInputModule>();
        
        SetScoreBoard();
    }


    /// <summary>
    /// Script for Play again button. Loads main menu scene
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Sets the Scoreboard text objects within the EndGameScene
    /// </summary>
    public void SetScoreBoard()
    {
        WinnerText.text = Scoreboard[1].PlayerName;
        LoserText.text = Scoreboard[0].PlayerName;
        WinnerScore.text = ("Score: " + Scoreboard[1].PlayerScore.ToString());
        LoserScore.text = ("Score: " + Scoreboard[0].PlayerScore.ToString());
    }
}
