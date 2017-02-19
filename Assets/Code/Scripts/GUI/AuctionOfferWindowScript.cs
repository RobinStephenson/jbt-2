using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionOfferWindowScript : MonoBehaviour
{
    public canvasScript AuctionCanvas;
    private AuctionManager auction;

    #region Resource amount labels
    public Text foodAuctionAmount;
    public Text energyAuctionAmount;
    public Text oreAuctionAmount;
    #endregion

    public Text AuctionBuyPrice;

    void Start ()
    {
        auction = GameHandler.GetGameManager().auction;
        foodAuctionAmount = auction.AuctionResources.food;
        energyAuctionAmount = auction.AuctionResources.energy;
        oreAuctionAmount = auction.AuctionResources.ore;
	}

    public void OnBuyAuctionButtonPress()
    {
        GameHandler.gameManager.auction.AuctionBuy(GameHandler.gameManager.GetCurrentPlayer());
    }



}
