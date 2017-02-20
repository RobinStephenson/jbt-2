using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//JBT
public class auctionSellWindowScript : MonoBehaviour
{
    public canvasScript AuctionCanvas;

    private AuctionManager auction;
    private Player currentPlayer;

    #region Resource amount labels
    public InputField foodAuctionAmount;
    public InputField energyAuctionAmount;
    public InputField oreAuctionAmount;
    #endregion

    public InputField AuctionPrice;
    public GameObject AuctionListedText;
    public GameObject ListingWindow;

    #region errorMessage labels
    public GameObject NotEnoughResourcesMessage;
    public GameObject NoResourcesMessage;
    public GameObject NoPriceMessage;
    #endregion

    void Start()
    {
        auction = GameHandler.GetGameManager().auctionManager;

        foodAuctionAmount.onValidateInput += ValidatePositiveInput;
        energyAuctionAmount.onValidateInput += ValidatePositiveInput;
        oreAuctionAmount.onValidateInput += ValidatePositiveInput;
        AuctionPrice.onValidateInput += ValidatePositiveInput;
    }

    public char ValidatePositiveInput(string text, int charIndex, char addedChar)
    {
        NotEnoughResourcesMessage.SetActive(false);
        NoResourcesMessage.SetActive(false);
        NoPriceMessage.SetActive(false);
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
        currentPlayer = GameHandler.gameManager.GetCurrentPlayer();

        resourcesToAuction.food = int.Parse(foodAuctionAmount.text);
        resourcesToAuction.energy = int.Parse(energyAuctionAmount.text);
        resourcesToAuction.ore = int.Parse(oreAuctionAmount.text);
        int auctionPrice = int.Parse(AuctionPrice.text);

        if ((currentPlayer.GetResources().food < resourcesToAuction.food) || (currentPlayer.GetResources().energy < resourcesToAuction.energy) || (currentPlayer.GetResources().ore < resourcesToAuction.ore))
        {
            NotEnoughResourcesMessage.SetActive(true);
        }
        else if (resourcesToAuction.Sum() == 0)
        {
            NoResourcesMessage.SetActive(true);
        }
        else if (auctionPrice == 0)
        {
            NoPriceMessage.SetActive(true);
        }
        else
        {
            GameHandler.gameManager.auctionManager.PutUpForAuction(resourcesToAuction, currentPlayer, auctionPrice);
            ClearWindow();
        }
    }

    public void LoadWindow()
    {
        currentPlayer = GameHandler.gameManager.GetCurrentPlayer();
        foodAuctionAmount.text = "0";
        energyAuctionAmount.text = "0";
        oreAuctionAmount.text = "0";
        AuctionPrice.text = "0";
        AuctionListedText.SetActive(false);
        ListingWindow.SetActive(true);
        NotEnoughResourcesMessage.SetActive(false);
        NoResourcesMessage.SetActive(false);
        NoPriceMessage.SetActive(false);

        foreach (AuctionManager.Auction curAuction in GameHandler.GetGameManager().auctionManager.auctionListings)
        {
            if (currentPlayer == curAuction.Owner)
            {
                ListingWindow.SetActive(false);
                AuctionListedText.SetActive(true);
            }
        }
    }

    public void ClearWindow()
    {
        AuctionListedText.SetActive(true);
        ListingWindow.SetActive(false);
        NotEnoughResourcesMessage.SetActive(false);
        NoResourcesMessage.SetActive(false);
        NoPriceMessage.SetActive(false);
    }
}