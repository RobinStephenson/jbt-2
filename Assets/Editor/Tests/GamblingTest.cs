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
}
