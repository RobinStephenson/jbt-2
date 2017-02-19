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

    public InputField AuctionPrice;

    void Start()
    {
        auction = GameHandler.GetGameManager().auctionManager; 

        foodAuctionAmount.onValidateInput += ValidatePositiveInput;          
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
        int auctionPrice = int.Parse(AuctionPrice.text);

        GameHandler.gameManager.auctionManager.PutUpForAuction(resourcesToAuction, currentPlayer, auctionPrice);
    }

    public void RefreshWindow()
    {
        foodAuctionAmount.text = "0";
        energyAuctionAmount.text = "0";
        oreAuctionAmount.text = "0";
        AuctionPrice.text = "0";
    }
}