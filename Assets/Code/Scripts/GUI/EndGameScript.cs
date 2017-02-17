using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Made by JBT
public class EndGameScript : MonoBehaviour
{
    public static Player Winner;
    public static Player Loser;
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
        WinnerText.text = Winner.GetName();
        LoserText.text = Loser.GetName();
        WinnerScore.text = Winner.CalculateScore().ToString();
        LoserScore.text = Loser.CalculateScore().ToString();
    }

    /// <summary>
    /// Sets the winner to the winning player
    /// </summary>
    public void SetWinnerText()
    {
        
    }
}
