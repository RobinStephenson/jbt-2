using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class canvasScript : MonoBehaviour
{
    public Text WinnerText;

    public void SetWinnerText(string text)
    {
        WinnerText.text = text;
    }
    
    public void PlayAgain()
    {
        GameHandler;

    }

    public void Quit()
    {

    }
}
