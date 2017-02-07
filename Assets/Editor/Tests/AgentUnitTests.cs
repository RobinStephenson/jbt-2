// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class AgentUnitTests
{
    ResourceGroup buyOrderCorrect = new ResourceGroup(2, 2, 0);
    ResourceGroup buyOrderToZero = new ResourceGroup(14, 14, 0);
    ResourceGroup buyOrderNegativeFood = new ResourceGroup(1, 0, 0);
    ResourceGroup buyOrderNegativeEnergy = new ResourceGroup(0, 1, 0);
    ResourceGroup buyOrderNegativeOre = new ResourceGroup(0, 0, 1);
    ResourceGroup sellOrderCorrect = new ResourceGroup(1, 1, 1);
    ResourceGroup sellOrderNegativeFood = new ResourceGroup(-1, 0, 0);
    ResourceGroup sellOrderNegativeEnergy = new ResourceGroup(0, -1, 0);
    ResourceGroup sellOrderNegativeOre = new ResourceGroup(0, 0, -1);

    [Test]
    public void MarketCreate()
    {
        Market m = new Market();

        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_AMOUNT, Market.STARTING_ENERGY_AMOUNT, Market.STARTING_ORE_AMOUNT), m.GetResources());
        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_BUY_PRICE, Market.STARTING_ENERGY_BUY_PRICE, Market.STARTING_ORE_BUY_PRICE), m.GetResourceBuyingPrices());
        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_SELL_PRICE, Market.STARTING_ENERGY_SELL_PRICE, Market.STARTING_ORE_SELL_PRICE), m.GetResourceSellingPrices());
        Assert.AreEqual(Market.STARTING_ROBOTICON_AMOUNT, m.GetNumRoboticonsForSale());
        Assert.AreEqual(Market.STARTING_MONEY, m.GetMoney());
    }

    [Test]
    public void BuyFromMarketSuccess()
    {
        Market m = new Market();

        int food = Market.STARTING_FOOD_AMOUNT;
        m.BuyFrom(new ResourceGroup(1, 0, 0));
        Assert.AreEqual(new ResourceGroup(food - 1, Market.STARTING_ENERGY_AMOUNT, Market.STARTING_ORE_AMOUNT), m.GetResources());
    }

    [Test]
    public void BuyFromMarketFail()
    {
        Market m = new Market();

        int food = Market.STARTING_FOOD_AMOUNT;
        Assert.Throws<System.ArgumentException>(() => m.BuyFrom(new ResourceGroup(food + 1, 0, 0)));
        Assert.AreEqual(new ResourceGroup(food, Market.STARTING_ENERGY_AMOUNT, Market.STARTING_ORE_AMOUNT), m.GetResources());
    }

    [Test]
    public void SellToMarketSuccess()
    {
        Market m = new Market();

        int ore = Market.STARTING_ORE_AMOUNT;
        m.SellTo(new ResourceGroup(0, 0, 2));
        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_AMOUNT, Market.STARTING_ENERGY_AMOUNT, ore + 2), m.GetResources());
    }

    [Test]
    public void SellToMarketFail()
    {
        Market m = new Market();

        int ore = Market.STARTING_ORE_AMOUNT;
        Assert.Throws<System.ArgumentException>(() => m.BuyFrom(new ResourceGroup(0, 0, -5)));
        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_AMOUNT, Market.STARTING_ENERGY_AMOUNT, ore), m.GetResources());
    }

    private string TestHuman()
    {
        //As player is an abstract class, a choice was made to instantiate player as a human (1.4), therefore testing of these two Classes will be done concurrently
        ResourceGroup humanResources = new ResourceGroup(50, 50, 50);
        ResourceGroup testGroup = new ResourceGroup(2, 2, 2);
        Human testHuman = new Human(humanResources, "Test", 500);
        Tile testTile1 = new Tile(testGroup, new Vector2(0, 0), 1);
        Tile testTile2 = new Tile(testGroup, new Vector2(0, 1), 2);
        Tile testTile3 = new Tile(testGroup, new Vector2(1, 0), 3);
        Roboticon testRoboticon1 = new Roboticon();

        string errorString = "";

        //No tests implemented for 1.1.1

        //Tests 1.1.(2,3,4,5,6,7,8).x will be implemented out of order due to their interdependancy, comments will be given for clarity

        //Tests for 1.1.5
        //1.1.5.1
        testHuman.AcquireTile(testTile1);
        if (testHuman.GetOwnedTiles()[0] != testTile1)
        {
            errorString += string.Format("Owned tiles is not equal to expected value for test 1.1.5.1\r\nShould read {0} for owned tile ID, actually reads {1}\r\n\r\n", testTile1.GetId(), testHuman.GetOwnedTiles()[0].GetId());
        }

        //1.1.5.2

        bool returnedError = false;
        try
        {
            testHuman.AcquireTile(testTile1);
        }
        catch(System.Exception e)
        {
            returnedError = true;
        }
        finally
        {
            if (!returnedError)
            {
                errorString += "Exception for owned Tile aquisition has not been thrown in test 1.1.5.2\r\n\r\n";
            }
        }

        //Tests for 1.1.6
        //1.1.6.1
        testHuman.AcquireRoboticon(testRoboticon1);
        if (testHuman.GetRoboticons()[0] != testRoboticon1)
        {
            errorString += string.Format("Owned Roboticons is not equal to expected value for test 1.1.6.1\r\nShould read {0}, actually reads {1}\r\n\r\n", testRoboticon1.GetName(), testHuman.GetRoboticons()[0].GetName());
        }

        //1.1.6.2
        try
        {
            testHuman.AcquireRoboticon(testRoboticon1);
        }
        catch(System.Exception e)
        {
            returnedError = true;
        }
        finally
        {
            if (!returnedError)
            {
                errorString += "Exception for owned Roboticon has not been thrown in test 1.1.6.2\r\n\r\n";
            }
        }

        //Tests for 1.1.7
        //1.1.7.1
        ResourceGroup roboticonValues = testRoboticon1.GetUpgrades();
        testHuman.UpgradeRoboticon(testRoboticon1, testGroup);

        errorString += ResourceChecker(testRoboticon1.GetUpgrades(), (roboticonValues.food)+2, (roboticonValues.energy)+2, (roboticonValues.ore)+2, "1.1.7.1");

        //Tests for 1.1.8
        //1.1.8.1
        testHuman.InstallRoboticon(testRoboticon1, testTile1);
        if (testTile1.GetInstalledRoboticons()[0] != testRoboticon1)
        {
            errorString += string.Format("Installed robotcons on test tile is not equal to expected value for test 1.1.8.1\r\nShould read {0}, actually reads {1}\r\n\r\n", testRoboticon1.GetName(), testTile1.GetInstalledRoboticons()[0]);
        }

        //Tests for 1.1.4
        //1.1.4.1
        errorString += ResourceChecker(testHuman.CalculateTotalResourcesGenerated(), 2+testRoboticon1.GetUpgrades().food,2+testRoboticon1.GetUpgrades().energy ,2+testRoboticon1.GetUpgrades().ore, "1.1.4.1");

        //Tests for 1.1.3
        //1.1.3.1
        int testScore = ((2 + testRoboticon1.GetUpgrades().food) + (2 + testRoboticon1.GetUpgrades().energy) + (2 + testRoboticon1.GetUpgrades().ore));
        if (testHuman.CalculateScore() != testScore)
        {
            errorString += string.Format("Score is not equal to expected value for test 1.1.3.1\r\nShould read {0}, actually reads {1}\r\n\r\n", testScore, testHuman.CalculateScore());
        }

        //Tests for 1.1.2
        //1.1.2.1
        

        //Tests for 1.1.9
        //1.1.9.1

        if (testHuman.IsHuman() == false)
        {
            errorString += "testHuman has not been initialised as Human in test 1.1.9.1";
        }

        return errorString;
    }
}
	
	

