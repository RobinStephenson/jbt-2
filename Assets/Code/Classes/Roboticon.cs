using UnityEngine;
using System.Collections;

public class Roboticon
{
    public static int TotalRoboticons = 0;
    public const int UPGRADEVALUE = 50; 

    private ResourceGroup upgrades;
    private string name;
    private bool isInstalledToTile = false;

    public Roboticon()
    {
        this.name = "RBN#" + (++TotalRoboticons).ToString("0000");
        this.upgrades = new ResourceGroup(0,0,0);
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
