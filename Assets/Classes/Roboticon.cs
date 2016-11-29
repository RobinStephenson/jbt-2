using UnityEngine;
using System.Collections;

public class Roboticon
{
    public enum RoboticonUpgrade
    {
        FOOD,
        ENERGY,
        ORE
    }

    private ResourceGroup upgrades;

    public void Upgrade(RoboticonUpgrade upgradeType)
    {
        //TODO 
    }

    public void Downgrade(RoboticonUpgrade downGradeType)
    {
        //TODO 
    }

    public int GetPrice()
    {
        //TODO - Calculate price of roboticon based
        // on current upgrades
        return 0;
    }

}
