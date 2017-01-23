using UnityEngine;
using System.Collections;

public class Roboticon
{
    public const int UPGRADEVALUE = 50; //TODO - Get correct valuation of an upgrade - Placeholder 50 per upgrade

    private ResourceGroup upgrades;
    private string name;
    private bool isInstalledToTile = false;

    public Roboticon()
    {
        this.name = "RBN#" + (Random.Range(1000, 9999)).ToString();
        this.upgrades = new ResourceGroup(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
    }

    public Roboticon(ResourceGroup upgrades, string name = "")
    {
        this.name = name;
        this.upgrades = upgrades;
    }

    public string GetName()
    {
        if(name == null)
        {
            throw new System.ArgumentNullException("Name not set in roboticon.");
        }

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

    public void InstallRoboticonToTile()
    {
        isInstalledToTile = true;
    }

    public void UninstallRoboticonToTile()
    {
        isInstalledToTile = false;
    }
    
    public bool IsInstalledToTile()
    {
        return isInstalledToTile;
    }
}
