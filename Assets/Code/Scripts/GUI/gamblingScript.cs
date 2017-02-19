using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamblingScript : MonoBehaviour {

    public Text marketBalance;

    private Market market;

    // Use this for initialization
    void Start()
    {
        market = GameHandler.GetGameManager().market;
    }
	
	public void PlayDoubleOrNothingButtonClicked()
    {

    }

    private void RefreshMarketBalance()
    {
        marketBalance.text = "Market has £" + market.GetMoney();

        GameObject marketMenu = GameObject.Find("Market Menu");

        if(marketMenu != null)
        {
            marketMenu.GetComponent<marketScript>().SetShownMarketPrices();
        }
    }
}
