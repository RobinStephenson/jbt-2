using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionOfferWindowScript : MonoBehaviour
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
        auctionManager = GameHandler.GetGameManager().auction;
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
            //FUCKING DISPLAY A MESSAGE THAT TELLS THE USER THERE ARE NO AUCTIONS TOM
        }
	}

    public void OnBuyAuctionButtonPress()
    {
        GameHandler.gameManager.auction.AuctionBuy(GameHandler.gameManager.GetCurrentPlayer());
    }

    public void RefreshWindow()
    {
        //DO IT TOM
    }
}
