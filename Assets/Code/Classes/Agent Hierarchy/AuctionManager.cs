// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuctionManager
{

    private List<AuctionListing> auctionListings = new List<AuctionListing>();
    private int totalAuctions = 0;
    

    public void PlaceBid(string playerName, int bidAmount)
    {
        //TODO - Place a bid on the current item to be auctioned.
    }

    /// <summary>
    /// Put resources up for auction
    /// </summary>
    /// <param name="resources">The resources to be auctioned</param>
    /// <param name="setPrice">The price the player has set</param>
    /// <param name="player">The player setting the auction</param>
    public void PutUpForAuction(ResourceGroup resources, int setPrice, Player player)
    {
        ResourceGroup ownedResources = player.GetResources();
        totalAuctions++;
        
        if (resources.food > ownedResources.food || resources.energy > ownedResources.energy || resources.ore > ownedResources.ore)
        {
            throw new ArgumentOutOfRangeException("Not enough resources");
        }
        else
        {
            auctionListings.Add(new AuctionListing(totalAuctions, setPrice, resources, player));
        }
    }

    public void PutUpForAuction(Roboticon roboticon)
    {
        //TODO - Set up a new auction for a Tile
    }

    public void PutUpForAuction(Tile tile)
    {
        //TODO - Set up a new auction for a Tile
    }
}
