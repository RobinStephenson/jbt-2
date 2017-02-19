using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Added by JBT for gambling support in purchase phase
public class gamblingScript : MonoBehaviour {

    public Text marketBalance;
    public Text resultText;

    private Market market;

    void Start()
    {
        market = GameHandler.GetGameManager().market;
    }
	
	public void PlayDoubleOrNothingButtonClicked()
    {
        Human currentPlayer = GameHandler.gameManager.GetHumanGui().GetCurrentHuman();

        if(currentPlayer.GetMoney() < 10)
        {
            SetResultText("You need at least £10 to play!", false);
            return;
        }

        currentPlayer.SetMoney(currentPlayer.GetMoney() - 10);

        int valueRolled;
        bool won = market.DoubleOrNothing(10, 0, 100, out valueRolled);

        SetResultText("You rolled: " + valueRolled.ToString() + "\n" + (won ? "You won!" : "You Lose!"), won);
    }

    public void RefreshMarketBalance()
    {
        marketBalance.text = "Market has £" + market.GetMoney();

        GameObject marketMenu = GameObject.Find("Market Menu");

        //Refresh the balance on the market menu if it is open
        if(marketMenu != null)
        {
            marketMenu.GetComponent<marketScript>().SetShownMarketPrices();
        }
    }

    public void ResetGamblingWindow()
    {
        resultText.text = "";
        RefreshMarketBalance();
    }

    /// <summary>
    /// Sets the text of the result textbox
    /// </summary>
    /// <param name="text">The text to display</param>
    /// <param name="green">Displays the text in green if true, red otherwise</param>
    private void SetResultText(string text, bool green)
    {
        string output = "";

        if(green)
        {
            output += "<color=green>";
        }
        else
        {
            output += "<color=red>";
        }

        output += text;
        output += "</color>";
        resultText.text = output;
    }
}
