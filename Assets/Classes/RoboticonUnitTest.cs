using System;

public class RoboticonUnitTest
{
	public RoboticonUnitTest()
	{
	}

    public void RoboticonTest()
    {
        Roboticon testRbt1 = new Roboticon(0, 0, 0);
        Roboticon testRbt2 = new Roboticon(1, 1, 1);
        Roboticon testRbt3 = new Roboticon(-1, -1, -1);
        Roboticon testRbt4 = new Roboticon(1, 1, 1);
        Roboticon testRbt5 = new Roboticon(5, 5, 5);
        Roboticon testRbt6 = new Roboticon(0, 0, 0);
        Roboticon testRbt7 = new Roboticon(0, 0, 0);
        string errorString = "";

        try
        {
            testRbt1.GetName();
        }
        catch(ArgumentException e)
        {
            errorString += ($"TestRbt1's name has not been set correctly in test 1.4.0.1");
        }

        //Upgrade Tests
        testRbt2.Upgrade(0);
        errorString += UpgradeChecker(testRbt2, 2, 1, 1, "1.4.0.1");
        testRbt2.Upgrade(0);
        errorString += UpgradeChecker(testRbt2, 3, 1, 1, "1.4.0.2");
        testRbt2.Upgrade(1);
        errorString += UpgradeChecker(testRbt2, 3, 2, 1, "1.4.0.3");
        testRbt2.Upgrade(3);
        errorString += UpgradeChecker(testRbt2, 3, 2, 2, "1.4.0.4");

        //Downgrade Tests
        testRbt2.Downgrade(0);
        errorString += UpgradeChecker(testRbt2, 2, 2, 2, "1.4.0.5");
        testRbt2.Downgrade(0);
        errorString += UpgradeChecker(testRbt2, 1, 2, 2, "1.4.0.6");
        try
        {
            testRbt2.Downgrade(0);
        }
        catch (ArgumentException)
        {
            errorString += "Error should be caught, caught because attempt to downgrade to a negative value. Test 1.4.0.7";
        }


        //Price Tests
        testRbt4.GetPrice();
        errorString += UpgradeChecker(testRbt2, 50, 50, 50, "1.4.0.8");
        testRbt5.GetPrice();
        errorString += UpgradeChecker(testRbt3, 250, 250, 250, "1.4.0.9");
        testRbt6.GetPrice();
        errorString += UpgradeChecker(testRbt4, 0, 0, 0, "1.4.1.0");
    }

    public string UpgradeChecker(RoboticonUnitTest rbt, int expectedFood, int expectedEnergy, int expectedOre, string testId)
    {
        string errorString = ("");
        if (rbt.upgrades.getFood() != expectedFood)
        {
            errorString += $"Food resource is incorrect for test {testId}\nShould read {expectedFood}, actually reads {resources.food}\n\n";
        }

        if (rbt.upgrades.getEnergy() != expectedEnergy)
        {
            errorString += $"Energy resource is incorrect for test {testId}\nShould read {expectedEnergy}, actually reads {resources.energy}\n\n";
        }

        if (rbt.upgrades.getOre() != expectedOre)
        {
            errorString += $"Ore resource is incorrect for test {testId}\nShould read {expectedOre}, actually reads {resources.ore}\n\n";
        }
        return (errorString);
    }
}
