// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuctionManager
{

    private List<Auction> auctionListings ;
    
    public AuctionManager()
    {
        auctionListings = new List<Auction>();
    }

    /// <summary>
    /// Put resources up for auction
    /// </summary>
    /// <param name="resources">The resources to be auctioned</param>
    /// <param name="setPrice">The price the player has set</param>
    /// <param name="player">The player setting the auction</param>
    public void PutUpForAuction(ResourceGroup resources, List<int> setPrice, Player player, int turnLimit)
    {
        ResourceGroup ownedResources = player.GetResources();

        if (0 > (ownedResources - resources).Sum())
        {
            throw new ArgumentOutOfRangeException("Not enough resources");
        }
        else
        {
            auctionListings.Add(new Auction(resources, player));
        }
    }

    public Auction RetrieveAuction(Player player)
    {
        foreach (Auction curAuction in auctionListings)
        {
            if (curAuction.owner != player)
            {
                return curAuction;
            }
        }
        return null;    
    }

    public void PlaceOffer(Player player, ResourceGroup price, ResourceGroup resources)
    {
        if(RetrieveAuction(player) != null)
        {
            Auction auction = RetrieveAuction(player);
            if (0 > (auction.resources-resources).Sum())
            {
                throw new ArgumentOutOfRangeException("Not enough resources in auction");
            }
            else if (player.GetMoney() < (resources * price).Sum())
            {
                throw new ArgumentOutOfRangeException("Not enough money");
            }
            else
            {
                auction.Offer = price;
            }
        }
        
    }

    public void ClearAuctions()
    {
        auctionListings = new List<Auction>();
    }

    public void AcceptOffer()
    {

    }

    public class Auction
    {
        public ResourceGroup Offer;
        public ResourceGroup resources { get; private set; }
        public Player owner { get; private set; }

        /// <summary>
        /// Creating an auction lot with its price and resource type
        /// </summary>
        /// <param name="resourceType">Type of resource</param>
        /// <param name="startPrice">Price of auction</param>
        /// <param name="player">Player creating the auction</param>
        public Auction(ResourceGroup resourceQuantity, Player player)
        {
            resources = resourceQuantity;
            owner = player;
        }


    }

}
