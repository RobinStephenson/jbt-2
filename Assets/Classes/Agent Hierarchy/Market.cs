using UnityEngine;
using System.Collections;

public class Market : Agent
{
    private Casino casino;
    private ResourceGroup resourceSellingPrices;
    private ResourceGroup resourceBuyingPrices;
    private ResourceGroup resources;
    private int numRoboticonsForSale;

    public void BuyFrom(ResourceGroup resources, int price)
    {
        //TODO - Alter market's resources and money
        //TODO - Check incoming resource
        this.resources -= resources;
        //TODO - Assuming infinte money, not, will deduct price 
        //this.money += price

    }

    public void SellTo(ResourceGroup resource, int price)
    {
        //TODO - Alter market's resources and money
        this.resources += resources;
        //this.money -= price
    }

    public void SetResourceSellingPrices(ResourceGroup newPrices)
    {
        //TODO - Setter for selling prices.c
    }

    public void SetResourceBuyingPrices(ResourceGroup newPrices)
    {
        //TODO - Setter for buying prices.
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
