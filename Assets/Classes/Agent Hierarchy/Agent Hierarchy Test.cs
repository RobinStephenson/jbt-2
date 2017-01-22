using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHierarchyTest : Agent
{
    
	public AgentHierarchyTest()
    {

    }

    public void MarketTest()
    {
        Market TestMarket = new Market();
        string errorString = "";
        ResourceGroup buyOrderCorrect = new ResourceGroup(2, 2, 0);
        ResourceGroup buyOrderToZero = new ResourceGroup(14, 14, 0);
        ResourceGroup buyOrderNegativeFood = new ResourceGroup(1, 0, 0); 
        ResourceGroup buyOrderNegativeEnergy = new ResourceGroup(0, 1, 0); 
        ResourceGroup buyOrderNegativeOre = new ResourceGroup(0, 0, 1);
        ResourceGroup sellOrderCorrect = new ResourceGroup(1, 1, 1);
        ResourceGroup sellOrderNegativeFood = new ResourceGroup(-1, 0, 0);
        ResourceGroup sellOrderNegativeEnergy = new ResourceGroup(0, -1, 0);
        ResourceGroup sellOrderNegativeOre = new ResourceGroup(0, 0, -1);

        //Contruction Tests
        //Testing Selling and Buying Prices, Amounts, Money, and Roboticons are consistant
        //with required constants

        errorString += ResourceChecker(TestMarket.resourceSellingPrices, 10, 10, 10, "1.2.0.0");

        errorString += ResourceChecker(TestMarket.resourceBuyingPrices, 10, 10, 10, "1.2.0.1");

        errorString += ResourceChecker(TestMarket.resources, 16, 16, 0, "1.2.0.2");

        if (TestMarket.numRoboticonsForSale != 12)
        {
            errorString += ($"Roboticon resource is incorrect for test 1.2.0.3\nShould read {12}, actually reads {TestMarket.numRoboticonsForSale}\n\n");
        }

        if (TestMarket.money != 100)
        {
            errorString += ($"Market money resource is incorrect for test 1.2.0.4\nShould read {100}, actually reads {TestMarket.money}\n\n");
        }


        // BuyFrom Tests

        TestMarket.BuyFrom(buyOrderCorrect); //Market now contains [14,14,0] and 140 money, checks below

        errorString += ResourceChecker(TestMarket.resources, 14, 14, 0, "1.2.1.0");


        if (TestMarket.money != 140)
        {
            errorString += ($"Market money resource is incorrect for test 1.2.1.1\nShould read {140}, actually reads {TestMarket.money}\n\n");
        }


        TestMarket.BuyFrom(buyOrderToZero); //Market now contains [0,0,0] and 420 money

        errorString += ResourceChecker(TestMarket.resources, 0, 0, 0, "1.2.1.2");

        if (TestMarket.money != 420)
        {
            errorString += ($"Market money resource is incorrect for test 1.2.1.3\nShould read {420}, actually reads {TestMarket.money}\n\n");
        }

        try
        {
            TestMarket.BuyFrom(buyOrderNegativeFood); //This should error and still have [0,0,0] and 420 money
        }
        catch(ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative food resource has not been thrown in test 1.2.1.4\nFood should read {0}, actually reads {TestMarket.resources.getfood}/n/n");
            }
        }

        try
        {
            TestMarket.BuyFrom(buyOrderNegativeEnergy); //This should error and still have [0,0,0] and 420 money
        }
        catch (ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative energy resource has not been thrown in test 1.2.1.5\nFood should read {0}, actually reads {TestMarket.resources.energy}/n/n");
            }
        }

        try
        {
            TestMarket.BuyFrom(buyOrderNegativeOre); //This should error and still have [0,0,0] and 420 money
        }
        catch (ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative energy resource has not been thrown in test 1.2.1.6\nFood should read {0}, actually reads {TestMarket.resources.ore}/n/n");
            }
        }


        //SellToTests

        TestMarket.SellTo(sellOrderCorrect);

        errorString += ResourceChecker(TestMarket.resources, 1, 1, 1, "1.2.2.0");

        if (TestMarket.money != 390)
        {
            errorString += ($"Market money resource is incorrect for test 1.2.2.0\nShould read {390}, actually reads {TestMarket.money}\n\n");
        }


        try
        {
            TestMarket.SellTo(sellOrderNegativeFood); //This should error and still have [1,1,1] and 390 money
        }
        catch (ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative food resource has not been thrown in test 1.2.2.1\nFood should read {1}, actually reads {TestMarket.resources.food}/n/n");
            }
        }

        try
        {
            TestMarket.SellTo(sellOrderNegativeEnergy); //This should error and still have [1,1,1] and 390 money
        }
        catch (ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative energy resource has not been thrown in test 1.2.2.2\nFood should read {1}, actually reads {TestMarket.resources.energy}/n/n");
            }
        }

        try
        {
            TestMarket.SellTo(sellOrderNegativeOre); //This should error and still have [1,1,1] and 390 money
        }
        catch (ArgumentException e)
        {
            bool stillPositive = true;
        }
        finally
        {
            if (stillPositive != true)
            {
                errorString += ($"Exception for negative ore resource has not been thrown in test 1.2.2.3\nFood should read {1}, actually reads {TestMarket.resources.ore}/n/n");
            }
        }

        //Update Price tests should be written later

        //Produce Roboitcon tests
        tempOre = TestMarket.resource.getOre();
        TestMarket.produceRoboticon();
        if (TestMarket.numRoboticonsForSale != 12)
        {
            errorString += ($"Roboticon amount has changed unexpectadly in test 1.2.4.0\nShould read {12}, actually reads {TestMarket.numRoboticonsForSale}/n/n");
        }
        TestMarket.ore = 15;
        TestMarket.produceRoboticon();
        if (TestMarket.numRoboticonsForSale != 13)
        {
            errorString += ($"Roboticon amount hasn't changed  in test 1.2.4.1\nShould read {13}, actually reads {TestMarket.numRoboticonsForSale}/n/n");
        }


    }


    
    public string ResourceChecker(ResourceGroup resources, int expectedFood, int expectedEnergy, int expectedOre, string testId )
    {
        string errorString = ("");
        if (resources.getFood != expectedFood)
        {
            errorString += $"Food resource is incorrect for test {testId}\nShould read {expectedFood}, actually reads {resources.food}\n\n";
        }

        if (resources.getEnergy != expectedEnergy)
        {
            errorString += $"Energy resource is incorrect for test {testId}\nShould read {expectedEnergy}, actually reads {resources.energy}\n\n";
        }

        if (resources.getOre != expectedOre)
        {
            errorString += $"Ore resource is incorrect for test {testId}\nShould read {expectedOre}, actually reads {resources.ore}\n\n";
        }
        return (errorString);
    }

    public void PlayerTest()
    {

    }

}
	
	

