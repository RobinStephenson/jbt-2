using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionBuyWindowScript : MonoBehaviour
{
    public canvasScript AuctionCanvas;
    private AuctionManager auctionManager;
    private Player curPlayer;
    private AuctionManager.Auction auction;

    #region Resource amount labels
    public Text foodAuctionAmount;
    public Text energyAuctionAmount;
    public Text oreAuctionAmount;
    #endregion

    public Text AuctionBuyPrice;

    void Start ()
    {
        auctionManager = GameHandler.GetGameManager().auctionManager;
        curPlayer = GameHandler.gameManager.GetCurrentPlayer();
        auction = auctionManager.RetrieveAuction(curPlayer);

        if (auction != null)
        {
            foodAuctionAmount.text = auction.AuctionResources.food.ToString();
            energyAuctionAmount.text = auction.AuctionResources.energy.ToString();
            oreAuctionAmount.text = auction.AuctionResources.ore.ToString();
            AuctionBuyPrice.text = auction.AuctionPrice.ToString();
        }
        else
        {
            string noAuction = "no auctions to buy";
        }
	}

    public void OnBuyAuctionButtonPress()
    {
        GameHandler.gameManager.auctionManager.AuctionBuy(GameHandler.gameManager.GetCurrentPlayer());
    }

    public void RefreshWindow()
    {
        foodAuctionAmount.text = "0";
        energyAuctionAmount.text = "0";
        oreAuctionAmount.text = "0";
        AuctionBuyPrice.text = "0";
    }
}
