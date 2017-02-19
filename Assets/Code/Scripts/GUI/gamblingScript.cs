using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamblingScript : MonoBehaviour {

    public Text marketBalance;
    public Text resultText;

    private Market market;

    // Use this for initialization
    void Start()
    {
        market = GameHandler.GetGameManager().market;
    }
	
	public void PlayDoubleOrNothingButtonClicked()
    {

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
}
