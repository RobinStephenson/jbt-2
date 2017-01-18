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

    const int UPGRADEVALUE = 50; //TODO - Get correct valuation of an upgrade - Placeholder 50 per upgrade

    private ResourceGroup upgrades;

    public Roboticon(ResourceGroup upgrades = new ResourceGroup(0,0,0))
    {
        this.upgrades = upgrades;
    }

    public void Upgrade(RoboticonUpgrade upgradeType)
    {
        switch (upgradeType)
        {
            case RoboticonUpgrade.FOOD:
                this.upgrades = this.upgrades + new ResourceGroup(1, 0, 0);
            case RoboticonUpgrade.ENERGY:
                this.upgrades = this.upgrades + new ResourceGroup(0, 1, 0);
            case RoboticonUpgrade.ORE:
                this.upgrades = this.upgrades + new ResourceGroup(0, 0, 1);
        }
    }

    public void Downgrade(RoboticonUpgrade downGradeType)
    {
        switch (upgradeType)
        {
            case RoboticonUpgrade.FOOD:
                this.upgrades = this.upgrades + new ResourceGroup(-1, 0, 0);
            case RoboticonUpgrade.ENERGY:
                this.upgrades = this.upgrades + new ResourceGroup(0, -1, 0);
            case RoboticonUpgrade.ORE:
                this.upgrades = this.upgrades + new ResourceGroup(0, 0, -1);
        }
    }

    public int GetPrice()
    {
        return (this.upgrades.getFood()   * UPGRADEVALUE) +
               (this.upgrades.getEnergy() * UPGRADEVALUE) +
               (this.upgrades.getOre()    * UPGRADEVALUE);
    }

}
