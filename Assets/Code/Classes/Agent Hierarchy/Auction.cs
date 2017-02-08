// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Auction
{

    private float commission;
    private int currentPrice;
    private string resourceType;
    private List<ResourceGroup> auctionLots = new List<ResourceGroup>();
    

    public void PlaceBid(string playerName, int bidAmount)
    {
        //TODO - Place a bid on the current item to be auctioned.
    }

    public void PutUpForAuction(ResourceGroup resources, int setPrice)
    {
        resourceType = GetType(resources);
    }

    public void PutUpForAuction(Roboticon roboticon)
    {
        //TODO - Set up a new auction for a Tile
    }

    public void PutUpForAuction(Tile tile)
    {
        //TODO - Set up a new auction for a Tile
    }

    public string GetType(ResourceGroup resources)
    {
        if ((resources.food > 0) & (resources.energy == 0) & (resources.ore == 0))
        {
            return "ore";
        }
        else if ((resources.food == 0) & (resources.energy > 0) & (resources.ore == 0))
        {
            return "energy";
        }
        else if ((resources.food == 0) & (resources.energy == 0) & (resources.ore > 0))
        {
            return "power";
        }
        else { throw new ArgumentException("Exactly one type of resource must have apositive value"); }
    }
}
