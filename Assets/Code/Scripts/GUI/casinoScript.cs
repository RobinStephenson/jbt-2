using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casinoScript : MonoBehaviour {

    private Market market;

    // Use this for initialization
    void Start()
    {
        market = GameHandler.GetGameManager().market;
    }
	
	public void PlayDoubleOrNothingButtonClicked()
    {

    }
}
