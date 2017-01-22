using UnityEngine;
using System.Collections;

public class Roboticon
{
    private const int UPGRADEVALUE = 50; //TODO - Get correct valuation of an upgrade - Placeholder 50 per upgrade

    private ResourceGroup upgrades;
    private string name;

    public Roboticon()
    {
        this.name = "RBN#" + (Random.Range(1000, 9999)).ToString();
        this.upgrades = new ResourceGroup(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
    }

    public string GetName()
    {
        return name;
    }

    public void Upgrade(ResourceGroup upgrades)
    {
        this.upgrades += upgrades;
    }

    public void Downgrade(ResourceGroup downgrades)
    {
        this.upgrades -= downgrades;
    }

    public int GetPrice()
    {
        return (this.upgrades * UPGRADEVALUE).Sum();
    }

    public ResourceGroup GetUpgrades()
    {
        return upgrades;
    }
}
