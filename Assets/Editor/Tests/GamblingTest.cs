using NUnit.Framework;

/// <summary>
/// Created by JBT
/// </summary>
public class GamblingTest {

	[Test]
    public void WinTest()
    {
        Market m = new Market();

        int actualRoll;
        bool won = m.DoubleOrNothing(10, 50, 50, out actualRoll);

        Assert.AreEqual(true, won);
        Assert.AreEqual(50, actualRoll);
    }

    [Test]
    public void LoseTest()
    {
        Market m = new Market();

        int actualRoll;
        bool won = m.DoubleOrNothing(10, 49, 49, out actualRoll);

        Assert.AreEqual(false, won);
        Assert.AreEqual(49, actualRoll);
    }

    [Test]
    public void MarketNotEnoughMoneyTest()
    {
        Market m = new Market();
        m.SetMoney(5);

        int actualRoll;
        Assert.Throws<System.ArgumentException>(() => m.DoubleOrNothing(10, 49, 49, out actualRoll));
    }

    /// <summary>
    /// Bit of a weird test to explain. This test ensures that the market will still be able to provide gambling if it doesnt have enough money, but will have enough money when the player pays to play.
    /// For example, the market has £15, a player wants to play. The market can't afford the £20 reward, but it can after recieving payment from the player
    /// </summary>
    [Test]
    public void MarketGetsEnoughMoney()
    {
        Market m = new Market();
        m.SetMoney(15);

        int actualRoll;
        m.DoubleOrNothing(10, 49, 49, out actualRoll);
    }
}
