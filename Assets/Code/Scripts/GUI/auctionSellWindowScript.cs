using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class auctionSellWindowScript : MonoBehaviour
{
    public canvasScript AuctionCanvas;

    private AuctionManager auction;

    #region Resource amount labels
    public InputField foodAuctionAmount;
    public InputField energyAuctionAmount;
    public InputField oreAuctionAmount;
    #endregion

    void Start()
    {
        auction = GameHandler.GetGameManager().auction;
        //SetShownMarketPrices(); 

        foodAuctionAmount.onValidateInput += ValidatePositiveInput;          //Add the ValidatePositiveInput function to
        energyAuctionAmount.onValidateInput += ValidatePositiveInput;
        oreAuctionAmount.onValidateInput += ValidatePositiveInput;
    }

    public char ValidatePositiveInput(string text, int charIndex, char addedChar)
    {
        int tryParseResult;

        if (int.TryParse(addedChar.ToString(), out tryParseResult)) //Only accept characters which are integers (no '-')
        {
            return addedChar;
        }
        else
        {
            return '\0';    //Empty string character
        }
    }

    public void OnListAuctionButtonPress()
    {
        ResourceGroup resourcesToAuction = new ResourceGroup();
        Player currentPlayer = GameHandler.gameManager.GetCurrentPlayer();
        resourcesToAuction.food = int.Parse(foodAuctionAmount.text);
        resourcesToAuction.energy = int.Parse(energyAuctionAmount.text);
        resourcesToAuction.ore = int.Parse(oreAuctionAmount.text);
        GameHandler.gameManager.auction.PutUpForAuction(resourcesToAuction, currentPlayer);

    }

    public void OnMakeOfferButtonPress()
    {
        //ResourceGroup 
    }

}