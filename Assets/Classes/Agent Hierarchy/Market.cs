using UnityEngine;
using System.Collections;

public class Market : Agent
{
    private Casino casino;
    private ResourceGroup resourceSellingPrices;
    private ResourceGroup resourceBuyingPrices;
    private ResourceGroup resourceAmount;
    private int numRoboticonsForSale;
    private int marketMoney;

    public void BuyFrom(ResourceGroup resources, int price)
    {
        //TODO - Alter market's resources and money
        //TODO - Check incoming resource
        this.resourceAmount -= resources; //Requires subtraction overload
        this.marketMoney = this.marketMoney + (resources * resourceSellingPrices); //Overloading * to perform dot product to get total gain

    }

    public void SellTo(ResourceGroup resource, int price)
    {
        //TODO - Alter market's resources and money
        this.resourceAmount += resources;
        this.marketMoney = this.marketMoney - (resources * resourceSellingPrices); //Overloading * to perform dot product to get total expenditure
    }

    public void SetResourceSellingPrices(ResourceGroup newPrices)
    {
        //TODO - Setter for selling prices.c
        resourceSellingPrices = newPrices;
    }

    public void SetResourceBuyingPrices(ResourceGroup newPrices)
    {
        //TODO - Setter for buying prices.
        resourceBuyingPrices = newPrices;
    }

    private void ProduceRoboticon()
    {
        //TODO - increment numRoboticons for sale and deduct ore
        // cost of roboticon production from market resources
        if (resources.ore >= 12)
        {
            numRoboticonsForSale++;
            resources.ore -= 12;
        }  
    }
}
