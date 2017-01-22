using UnityEngine;
using System.Collections;

public class Market : Agent
{
    private Casino casino;
    private ResourceGroup resourceSellingPrices { get; set;  }
    private ResourceGroup resourceBuyingPrices { get; set; }
    private int numRoboticonsForSale { get; set; }

    #region Market Starting Constants
    private const int STARTING_FOOD_AMOUNT = 16;
    private const int STARTING_ENERGY_AMOUNT = 16;
    private const int STARTING_ORE_AMOUNT = 0;
    private const int STARTING_ROBOTICON_AMOUNT = 12;

    private const int STARTING_FOOD_BUY_PRICE = 10;
    private const int STARTING_ORE_BUY_PRICE = 10;
    private const int STARTING_ENERGY_BUY_PRICE = 10;
    private const int STARTING_FOOD_SELL_PRICE = 10;
    private const int STARTING_ORE_SELL_PRICE = 10;
    private const int STARTING_ENERGY_SELL_PRICE = 10;

    private const int STARTING_MONEY = 100;
    #endregion

    private const int ROBOTICON_PRODUCTION_COST = 12;

    public Market()
    {
        this.resourceSellingPrices = new ResourceGroup(STARTING_FOOD_BUY_PRICE, STARTING_ENERGY_BUY_PRICE, STARTING_ORE_BUY_PRICE);
        this.resourceBuyingPrices = new ResourceGroup(STARTING_FOOD_SELL_PRICE, STARTING_ENERGY_SELL_PRICE, STARTING_ORE_SELL_PRICE);
        this.resources = new ResourceGroup(STARTING_FOOD_AMOUNT,STARTING_ENERGY_AMOUNT,STARTING_ORE_AMOUNT);
        this.numRoboticonsForSale = STARTING_ROBOTICON_AMOUNT;
        this.money = STARTING_MONEY;
    }
    
    /// <summary>
    /// Throws System.ArgumentException if the market does not have enough resources 
    /// to complete the transaction.
    /// </summary>
    /// <param name="resources"></param>
    /// <param name="price"></param>
    public void BuyFrom(ResourceGroup resourcesToBuy, int price)
    {
        bool hasEnoughResources = !(resourcesToBuy.food > this.resources.food 
            || resourcesToBuy.energy > this.resources.energy 
            || resourcesToBuy.ore > this.resources.ore);

        if (hasEnoughResources)
        {
            this.resources -= resourcesToBuy; //Requires subtraction overload
            this.money = this.money + (resourcesToBuy * resourceSellingPrices).Sum(); //Overloading * to perform element-wise product to get total gain 
        }  
        else
        {
            throw new System.ArgumentException("Market does not have enough resources to perform this transaction.");
        }
        
    }

    /// <summary>
    /// Throws System.ArgumentException if the market does not have enough money
    /// to complete the transaction.
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="price"></param>
    public void SellTo(ResourceGroup resourcesToSell, int price)
    {
        if (price <= money)
        {
            resources += resourcesToSell;
            money = money - (resourcesToSell * resourceBuyingPrices).Sum(); //Overloading * to perform element-wise product to get total expenditure
        }
        else
        {
            throw new System.ArgumentException("Market does not have enough money to perform this transaction.");
        }
    }

    public void UpdatePrices()
    {
        //Skeleton for later use when adding supply & demand
    }

    private void ProduceRoboticon()
    {  
        if (resources.ore >= ROBOTICON_PRODUCTION_COST)
        {
            resources.ore -= ROBOTICON_PRODUCTION_COST;
            numRoboticonsForSale++;
        }  
    }
}
