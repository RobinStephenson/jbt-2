using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class RoboticonTest
{
    [Test]
    public void TestRoboticonName()
    {
        Roboticon rb1 = new Roboticon();
        Assert.AreEqual("RBN#" + Roboticon.TotalRoboticons.ToString("0000"), rb1.GetName());
        Roboticon rb2 = new Roboticon();
        Assert.AreEqual("RBN#" + Roboticon.TotalRoboticons.ToString("0000"), rb2.GetName());
    }

    [Test]
    public void TestRoboticonEmptyUpgrade()
    {
        Roboticon rb = new Roboticon();
        Assert.AreEqual(new ResourceGroup(0, 0, 0), rb.GetUpgrades());
    }

    [Test]
    public void TestDownGrade()
    {
        Roboticon rb = new Roboticon(new ResourceGroup(5, 3, 7));
        rb.Downgrade(new ResourceGroup(2, 1, 3));
        Assert.AreEqual(new ResourceGroup(3, 2, 4), rb.GetUpgrades());
    }

    [Test]
    public void TestUpgrade()
    {
        Roboticon rb = new Roboticon(new ResourceGroup(2,9,3));
        rb.Upgrade(new ResourceGroup(0, 0, 1));
        Assert.AreEqual(new ResourceGroup(2,9,4), rb.GetUpgrades());
    }

    [Test]
    public void TestPrice()
    {
        Roboticon rb = new Roboticon(new ResourceGroup(3,2,1));
        Assert.AreEqual(Roboticon.UPGRADEVALUE * 6, rb.GetPrice());
    }   
}
