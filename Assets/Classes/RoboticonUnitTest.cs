using System;

public class RoboticonUnitTest
{
    public void TestRoboticon()
    {
        Roboticon testRbt1 = new Roboticon(new ResourceGroup(0, 0, 0));
        Roboticon testRbt2 = new Roboticon(new ResourceGroup(1, 1, 1));
        Roboticon testRbt3 = new Roboticon(new ResourceGroup(-1, -1, -1));
        Roboticon testRbt4 = new Roboticon(new ResourceGroup(1, 1, 1));
        Roboticon testRbt5 = new Roboticon(new ResourceGroup(5, 5, 5));
        Roboticon testRbt6 = new Roboticon(new ResourceGroup(0, 0, 0));
        Roboticon testRbt7 = new Roboticon(new ResourceGroup(0, 0, 0));
        string errorString = "";

        try
        {
            testRbt1.GetName();
        }
        catch(ArgumentException e)
        {
            errorString += (string.Format("TestRbt1's name has not been set correctly in test 1.4.0.1"));
        }

        //Upgrade Tests
        testRbt2.Upgrade(new ResourceGroup(1, 0, 0));
        errorString += UpgradeChecker(testRbt2, 2, 1, 1, "1.4.0.1");
        testRbt2.Upgrade(new ResourceGroup(1, 0, 0));
        errorString += UpgradeChecker(testRbt2, 3, 1, 1, "1.4.0.2");
        testRbt2.Upgrade(new ResourceGroup(0, 1, 0));
        errorString += UpgradeChecker(testRbt2, 3, 2, 1, "1.4.0.3");
        testRbt2.Upgrade(new ResourceGroup(0, 0, 1));
        errorString += UpgradeChecker(testRbt2, 3, 2, 2, "1.4.0.4");

        //Downgrade Tests
        testRbt2.Downgrade(new ResourceGroup(1, 0, 0));
        errorString += UpgradeChecker(testRbt2, 2, 2, 2, "1.4.0.5");
        testRbt2.Downgrade(new ResourceGroup(1, 0, 0));
        errorString += UpgradeChecker(testRbt2, 1, 2, 2, "1.4.0.6");
        try
        {
            testRbt2.Downgrade(new ResourceGroup(1, 0, 0));
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

    public string UpgradeChecker(Roboticon rbt, int expectedFood, int expectedEnergy, int expectedOre, string testId)
    {
        string errorString = ("");
        ResourceGroup upgrades = rbt.GetUpgrades();

        if (upgrades.getFood() != expectedFood)
        {
            errorString += string.Format("Food resource is incorrect for test {0}\nShould read {1}, actually reads {2}\n\n", testId, expectedFood, upgrades.food);
        }

        if (upgrades.getEnergy() != expectedEnergy)
        {
            errorString += string.Format("Energy resource is incorrect for test {testId}\nShould read {1}, actually reads {2}\n\n", testId, expectedEnergy, upgrades.energy);
        }

        if (upgrades.getOre() != expectedOre)
        {
            errorString += string.Format("Ore resource is incorrect for test {0}\nShould read {1}, actually reads {2}\n\n", testId, expectedOre, upgrades.ore);
        }
        return errorString;
    }
}
