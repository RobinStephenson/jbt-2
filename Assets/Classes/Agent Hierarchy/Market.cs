using UnityEngine;
using System.Collections;

public class Market : Agent
{
    private Casino casino;
    private ResourceGroup resourceSellingPrices { get; set;  }
    private ResourceGroup resourceBuyingPrices { get; set; }
    private ResourceGroup resourceAmounts { get; set; }
    private int numRoboticonsForSale { get; set; }
    private int marketMoney { get; set; }

    public Market(ResourceGroup resourceSellingPrices, ResourceGroup resourceBuyingPrices, int marketMoney)
    {
        this.resourceSellingPrices = resourceSellingPrices;
        this.resourceBuyingPrices = resourceBuyingPrices;
        this.resourceAmounts = new ResourceGroup(16, 16, 0);
        this.numRoboticonsForSale = 12;
        this.marketMoney = marketMoney;
    }
    
    public bool BuyFrom(ResourceGroup resources, int price)
    {
        bool hasEnoughResources = !(resources.food > this.resourceAmounts.food || resourceAmounts.energy > this.resourceAmounts.energy || resourcesAmounts.ore > this.resourceAmounts.ore);
        if (hasEnoughResource)
        {
            this.resourceAmounts -= resources; //Requires subtraction overload
            this.marketMoney = this.marketMoney + (resources * resourceSellingPrices).Sum(); //Overloading * to perform dot product to get total gain 
        }  
        
    }

    public void SellTo(ResourceGroup resource, int price)
    {
        this.resourceAmounts += resources;
        this.marketMoney = this.marketMoney - (resources * resourceSellingPrices); //Overloading * to perform dot product to get total expenditure
    }

    public void UpdatePrices()
    {
        //Skeleton for later use when adding supply & demand
    }

    private void ProduceRoboticon()
    {
        
        if (resources.ore >= 12)
        {
            numRoboticonsForSale++;
            resources.ore -= 12;
        }  
    }

}
