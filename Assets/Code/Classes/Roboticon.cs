using UnityEngine;
using System.Collections;

public class Roboticon
{
    public static int TotalRoboticons = 0;
    public const int UPGRADEVALUE = 50; //TODO - Get correct valuation of an upgrade - Placeholder 50 per upgrade
    public Tile InstalledTile;  //Added by JBT

    private ResourceGroup upgrades;
    private string name;

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

    //Tile reference added by JBT
    public void InstallRoboticonToTile(Tile t)
    {
        InstalledTile = t;
    }

    //Tile reference added by JBT
    public void UninstallRoboticonFromTile()
    {
        InstalledTile = null;
    }
    
    //Changed by JBT to use the tile reference for this instance instead
    public bool IsInstalledToTile()
    {
        return InstalledTile != null;
    }
}
