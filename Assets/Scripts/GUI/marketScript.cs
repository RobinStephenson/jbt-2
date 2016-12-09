using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class marketScript : MonoBehaviour
{
    #region Resource price labels
    private ResourceGroup marketBuyingPrices;
    private ResourceGroup marketSellingPrices;

    public Text foodBuyPrice;
    public Text foodSellPrice;
    public Text energyBuyPrice;
    public Text energySellPrice;
    public Text oreBuyPrice;
    public Text oreSellPrice;

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
            return '\0';
        }
    }

    // Use this for initialization
    void Start ()
    {
        //TEMP
        SetShownMarketPrices(new ResourceGroup(5, 10, 3), new ResourceGroup(2, 6, 1));
        ///

        foodBuyAmount.onValidateInput   += ValidatePositiveInput;
        energyBuyAmount.onValidateInput += ValidatePositiveInput;
        oreBuyAmount.onValidateInput    += ValidatePositiveInput;

        foodSellAmount.onValidateInput += ValidatePositiveInput;
        energySellAmount.onValidateInput += ValidatePositiveInput;
        oreSellAmount.onValidateInput += ValidatePositiveInput;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    public void SetShownMarketPrices(ResourceGroup buyingPrices, ResourceGroup sellingPrices)
    {
        marketBuyingPrices = buyingPrices;
        marketSellingPrices = sellingPrices;

        UpdateShownMarketPrices();
        UpdateTotalBuyPrice();
        UpdateTotalSellPrice();
    }

    public void UpdateTotalBuyPrice()
    {
        int foodPrice = int.Parse(foodBuyAmount.text) * marketBuyingPrices.food;
        int energyPrice = int.Parse(energyBuyAmount.text) * marketBuyingPrices.energy;
        int orePrice = int.Parse(oreBuyAmount.text) * marketBuyingPrices.ore;

        totalBuyPrice.text = "£" + (foodPrice + energyPrice + orePrice).ToString();
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
        foodBuyPrice.text   = "£" + marketBuyingPrices.food.ToString();
        energyBuyPrice.text = "£" + marketBuyingPrices.energy.ToString();
        oreBuyPrice.text    = "£" + marketBuyingPrices.ore.ToString();

        foodSellPrice.text = "£" + marketSellingPrices.food.ToString();
        energySellPrice.text = "£" + marketSellingPrices.energy.ToString();
        oreSellPrice.text = "£" + marketSellingPrices.ore.ToString();
    }
}
