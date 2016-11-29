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
        return 0;
        //TODO - Calculate score of player based on owned
        // tiles, roboticons and resources and money.
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
        //TODO 
    }

    public void InstallRoboticon(Roboticon roboticon, Tile tile)
    {
        //TODO - Install roboticon to tile
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
