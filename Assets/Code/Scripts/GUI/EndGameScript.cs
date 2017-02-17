using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

//Made by JBT
public class EndGameScript : MonoBehaviour
{
    public Text WinnerText;

    /// <summary>
    /// Sets the winner
    /// </summary>
    /// <param name="text">The name of the winner</param>
    public void SetWinnerText(string text)
    {
        WinnerText.text = text;
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


}
