using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class MarketTest
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
        m.BuyFrom(new ResourceGroup(1, 0, 0), 0);
        Assert.AreEqual(new ResourceGroup(food - 1, Market.STARTING_ENERGY_AMOUNT, Market.STARTING_ORE_AMOUNT), m.GetResources());
    }

    [Test]
    public void BuyFromMarketFail()
    {
        Market m = new Market();

        int food = Market.STARTING_FOOD_AMOUNT;
        Assert.Throws<System.ArgumentException>(() => m.BuyFrom(new ResourceGroup(food + 1, 0, 0), 0));
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
        Assert.Throws<System.ArgumentException>(() => m.BuyFrom(new ResourceGroup(0, 0, -5), 0));
        Assert.AreEqual(new ResourceGroup(Market.STARTING_FOOD_AMOUNT, Market.STARTING_ENERGY_AMOUNT, ore), m.GetResources());
    }

    [Test]
    public void UpdateMarketPrice()
    {
        Market m = new Market();
        ResourceGroup expectedBuyPrice = new ResourceGroup(5,5,15);
        ResourceGroup expectedSellPrice = expectedBuyPrice * 1.1;

        //Money when market is initialised = 100, changed to 10.
        m.SetMoney(10);

        //Update the prices to account for changed money
        m.UpdatePrices();

        //Assert.AreEqual(expectedBuyPrice, m.GetResourceBuyingPrices);
        //Assert.AreEqual(expectedSellPrice, m.GetResourceSellingPrices);
    }
}	

