using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class AgentUnitTests
{
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

    [Test]
    public void CreateHumanTest()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Assert.IsTrue(testHuman.IsHuman());
    }

    [Test]
    public void CreateAITest()
    {
        AI testAi = new AI(ResourceGroup.Empty, "bot", 400);
        Assert.IsFalse(testAi.IsHuman());
    }

    [Test]
    public void AcquireTileTest()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Tile t = new Tile(ResourceGroup.Empty, new Vector2(0, 0), 1);

        testHuman.AcquireTile(t);
        Assert.AreEqual(testHuman, t.GetOwner());
        Assert.AreEqual(testHuman.GetOwnedTiles()[0],t);
    }

    [Test]
    public void AcquireSameTileTest()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Tile t = new Tile(ResourceGroup.Empty, new Vector2(0, 0), 1);

        testHuman.AcquireTile(t);
        Assert.Throws<System.Exception>(() => testHuman.AcquireTile(t));
    }

    public void AcquireRoboticon()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Roboticon r = new Roboticon();

        testHuman.AcquireRoboticon(r);
        Assert.AreEqual(testHuman.GetRoboticons()[0], r);
    }

    [Test]
    public void InstallRoboticonTest()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Tile t = new Tile(ResourceGroup.Empty, new Vector2(0, 0), 1);
        Roboticon r = new Roboticon();

        testHuman.AcquireTile(t);
        testHuman.AcquireRoboticon(r);
        testHuman.InstallRoboticon(r, t);

        Assert.AreEqual(testHuman.GetOwnedTiles()[0].GetInstalledRoboticons()[0], r);
    }

    [Test]
    public void ScoreTest()
    {
        Human testHuman = new Human(ResourceGroup.Empty, "Test", 500);
        Tile t = new Tile(new ResourceGroup(3,4,7), new Vector2(0, 0), 1);
        Roboticon r = new Roboticon(new ResourceGroup(1,0,2));

        testHuman.AcquireTile(t);
        testHuman.AcquireRoboticon(r);
        testHuman.InstallRoboticon(r, t);

        Assert.AreEqual(testHuman.CalculateScore(), 164);
    }
}	

