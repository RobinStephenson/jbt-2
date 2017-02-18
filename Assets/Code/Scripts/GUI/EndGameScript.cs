using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Made by JBT
public class EndGameScript : MonoBehaviour
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

    public void SetScoreBoard()
    {
        WinnerText.text = Scoreboard[0].PlayerName;
        LoserText.text = Scoreboard[1].PlayerName;
        WinnerScore.text = Scoreboard[0].PlayerScore.ToString();
        LoserScore.text = Scoreboard[1].PlayerScore.ToString();
    }
}
