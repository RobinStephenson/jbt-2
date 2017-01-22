using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class marketScript : MonoBehaviour
{
    public canvasScript uiCanvas;

    #region Resource price labels
    private ResourceGroup marketBuyingPrices;
    private ResourceGroup marketSellingPrices;
    private int marketRoboticonPrice;

    public Text foodBuyPrice;
    public Text foodSellPrice;
    public Text energyBuyPrice;
    public Text energySellPrice;
    public Text oreBuyPrice;
    public Text oreSellPrice;
    public Text roboticonBuyPrice;

    public Text totalBuyPrice;
    public Text totalSellPrice;
    #endregion

    #region Resource amount labels
    public InputField foodBuyAmount;
    public InputField foodSellAmount;
    public InputField energyBuyAmount;
    public InputField energySellAmount;
    public InputField oreBuyAmount;
    public InputField oreSellAmount;
    public InputField roboticonBuyAmount;
    #endregion

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

    // Use this for initialization
    void Start ()
    {
        Market market = GameHandler.GetGameManager().market;
        SetShownMarketPrices(market.GetResourceBuyingPrices(), market.GetResourceSellingPrices(), market.GetRoboticonSellingPrice());

        foodBuyAmount.onValidateInput   += ValidatePositiveInput;       //Add the ValidatePositiveInput function to
        energyBuyAmount.onValidateInput += ValidatePositiveInput;       //each GUI Text.
        oreBuyAmount.onValidateInput    += ValidatePositiveInput;

        foodSellAmount.onValidateInput  += ValidatePositiveInput;
        energySellAmount.onValidateInput += ValidatePositiveInput;
        oreSellAmount.onValidateInput   += ValidatePositiveInput;
    }

    public void OnBuyButtonPress()
    {
        ResourceGroup resourcesToBuy = new ResourceGroup();
        resourcesToBuy.food = int.Parse(foodBuyAmount.text);
        resourcesToBuy.energy = int.Parse(energyBuyAmount.text);
        resourcesToBuy.ore = int.Parse(oreBuyAmount.text);
        int roboticonsToBuy = int.Parse(roboticonBuyAmount.text);

        int buyPrice = int.Parse(totalBuyPrice.text.Substring(1));

        uiCanvas.BuyFromMarket(resourcesToBuy, roboticonsToBuy, buyPrice);
    }

    public void OnSellButtonPress()
    {
        ResourceGroup resourcesToSell = new ResourceGroup();
        resourcesToSell.food = int.Parse(foodSellAmount.text);
        resourcesToSell.energy = int.Parse(energySellAmount.text);
        resourcesToSell.ore = int.Parse(oreSellAmount.text);

        int sellPrice = int.Parse(totalSellPrice.text.Substring(1));

        uiCanvas.SellToMarket(resourcesToSell, sellPrice);
    }

    public void PlayPurchaseDeclinedAnimation()
    {
        totalBuyPrice.GetComponent<Animator>().SetTrigger(HumanGui.ANIM_TRIGGER_FLASH_RED);
    }

    public void PlaySaleDeclinedAnimation()
    {
        totalSellPrice.GetComponent<Animator>().SetTrigger(HumanGui.ANIM_TRIGGER_FLASH_RED);
    }

    public void SetShownMarketPrices(ResourceGroup buyingPrices, ResourceGroup sellingPrices, int roboticonPrice)
    {
        marketBuyingPrices = buyingPrices;
        marketSellingPrices = sellingPrices;
        marketRoboticonPrice = roboticonPrice;

        UpdateShownMarketPrices();
        UpdateTotalBuyPrice();
        UpdateTotalSellPrice();
    }

    public void UpdateTotalBuyPrice()
    {
        int foodPrice = int.Parse(foodBuyAmount.text) * marketBuyingPrices.food;
        int energyPrice = int.Parse(energyBuyAmount.text) * marketBuyingPrices.energy;
        int orePrice = int.Parse(oreBuyAmount.text) * marketBuyingPrices.ore;
        int roboticonPrice = int.Parse(roboticonBuyAmount.text) * marketRoboticonPrice;

        totalBuyPrice.text = "£" + (foodPrice + energyPrice + orePrice + roboticonPrice).ToString();
    }

    public void UpdateTotalSellPrice()
    {
        int foodPrice = int.Parse(foodSellAmount.text) * marketSellingPrices.food;
        int energyPrice = int.Parse(energySellAmount.text) * marketSellingPrices.energy;
        int orePrice = int.Parse(oreSellAmount.text) * marketSellingPrices.ore;

        totalSellPrice.text = "£" + (foodPrice + energyPrice + orePrice).ToString();
    }

    private void UpdateShownMarketPrices()
    {
        foodBuyPrice.text      = "£" + marketBuyingPrices.food.ToString();
        energyBuyPrice.text    = "£" + marketBuyingPrices.energy.ToString();
        oreBuyPrice.text       = "£" + marketBuyingPrices.ore.ToString();
        roboticonBuyPrice.text = "£" + marketRoboticonPrice.ToString();
        
        foodSellPrice.text     = "£" + marketSellingPrices.food.ToString();
        energySellPrice.text   = "£" + marketSellingPrices.energy.ToString();
        oreSellPrice.text      = "£" + marketSellingPrices.ore.ToString();
    }
}
