using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Player : Agent
{
    protected string name;
    protected int score;
    protected List<Roboticon> ownedRoboticons;
    protected List<Tile> ownedTiles;

    public int CalculateScore()
    {
        int scoreFromTiles = 0;
        foreach(Tile tile in ownedTiles)
        {
            ResourceGroup tileResources = tile.GetResourcesGenerated();
            scoreFromTiles += tileResources.energy + tileResources.food + tileResources.ore;
        }

        int scoreFromRoboticons = 0;
        foreach (Roboticon roboticon in ownedRoboticons)
        {
            scoreFromRoboticons += roboticon.GetPrice();
        }

        return scoreFromRoboticons + scoreFromTiles;
    }

    public ResourceGroup CalculateTotalResourcesGenerated()
    {
        //TODO
        return new ResourceGroup(5, 67, -69);
    }

    public void AcquireTile(Tile tile)
    {
        ownedTiles.Add(tile);
    }

    public void AcquireRoboticon(Roboticon roboticon)
    {
        ownedRoboticons.Add(roboticon);
    }

    public void UpgradeRoboticon(Roboticon roboticon, Roboticon.RoboticonUpgrade upgrade)
    {
        roboticon.Upgrade(upgrade);
    }

    public void InstallRoboticon(Roboticon roboticon, Tile tile)
    {
        tile.InstallRoboticon(roboticon);
    }

    public void PutItemUpForAuction()
    {
        //TODO - interface with auction. Not a priority.
    }

    public bool PlaceBidOnCurrentAuctionItem(int bidAmount)
    {
        //TODO - interface with auction. Not a priority.
        return true;
    }

    public abstract void Act();
}
