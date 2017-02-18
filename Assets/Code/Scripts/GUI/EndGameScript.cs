using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
