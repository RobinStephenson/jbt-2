using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Added by JBT for gambling support in purchase phase
public class gamblingScript : MonoBehaviour {

    public Text marketBalance;
    public Text resultText;

    private Market market;

    void Awake()
    {
        market = GameHandler.GetGameManager().market;
    }
	
	public void PlayDoubleOrNothingButtonClicked(int amount)
    {
        Human currentPlayer = GameHandler.gameManager.GetHumanGui().GetCurrentHuman();

        if(currentPlayer.GetMoney() < amount)
        {
            SetResultText("You need at least £10 to play!", false);
            return;
        }

        currentPlayer.SetMoney(currentPlayer.GetMoney() - amount);

        try
        {
            int valueRolled;
            bool won = market.DoubleOrNothing(amount, 0, 100, out valueRolled);

            SetResultText("You rolled: " + valueRolled.ToString() + "\n" + (won ? "You won £" + (amount * 2).ToString() + "!" : "You Lose!"), won);

            if (won)
            {
                currentPlayer.SetMoney(currentPlayer.GetMoney() + (amount * 2));
            }
        }
        catch(System.ArgumentException e)
        {
            SetResultText(e.ToString(),false);
            currentPlayer.SetMoney(currentPlayer.GetMoney() + amount);
        }

        ResetGamblingWindow();
        GameHandler.gameManager.GetHumanGui().UpdateResourceBar(false);
    }

    public void ResetGamblingWindow()
    {
        RefreshMarketBalance();

        GameObject marketMenu = GameObject.Find("Market Menu");

        //Refresh the balance on the market menu if it is open
        if (marketMenu != null)
        {
            marketMenu.GetComponent<marketScript>().RefreshMarketBalance();
        }
    }

    public void RefreshMarketBalance()
    {
        marketBalance.text = "Market has £" + market.GetMoney();
    }

    public void OpenGamblingWindow()
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
