// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AuctionManager
{
    private List<Auction> auctionListings;

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
    public void PutUpForAuction(ResourceGroup resources, Player player, int price)
    {
        ResourceGroup resourceDifference = player.GetResources() - resources;
        if ((resourceDifference.food < 0) || (resourceDifference.energy < 0) || (resourceDifference.ore < 0))
        {
            throw new ArgumentOutOfRangeException("Not enough resources");
        }
        else
        {
            auctionListings.Add(new Auction(resources, player, price));
        }
    }

    /// <summary>
    /// Returns an auction that the current player can bid on. For example, if player 1 is passed in, player 2's auction will be returned
    /// </summary>
    /// <param name="player">The player bidding</param>
    /// <returns>The auction the player can bid on</returns>
    public Auction RetrieveAuction(Player player)
    {
        foreach (Auction curAuction in auctionListings)
        {
            if (curAuction.Owner != player)
            {
                return curAuction;
            }
        }
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player">The player buying</param>
    public void AuctionBuy(Player player)
    {
        if (RetrieveAuction(player) != null)
        {
            Auction auction = RetrieveAuction(player);

            if (player.GetMoney() < auction.AuctionPrice)
            {
                throw new ArgumentOutOfRangeException("Not enough money");
            }
            else
            {
                player.SetResources(player.GetResources() + auction.AuctionResources);
                player.SetMoney(player.GetMoney() - auction.AuctionPrice);
                auction.Owner.SetResources(auction.Owner.GetResources() - auction.AuctionResources);
                auction.Owner.SetMoney(auction.Owner.GetMoney() + auction.AuctionPrice);
                auctionListings.Remove(auction);
            }
        }

    }

    /// <summary>
    /// Clears the list of auctions
    /// </summary>
    public void ClearAuctions()
    {
        auctionListings = new List<Auction>();
    }

    /// <summary>
    /// A simple class which contains a list of resources a player wants to put up for auction and how much it will cost
    /// </summary>
    public class Auction
    {
        public int AuctionPrice { get; private set; }
        public ResourceGroup AuctionResources { get; private set; }
        public Player Owner { get; private set; }

        /// <summary>
        /// Creating an auction lot with its price and resource type
        /// </summary>
        /// <param name="resourceType">Type of resource</param>
        /// <param name="startPrice">Price of auction</param>
        /// <param name="player">Player creating the auction</param>
        public Auction(ResourceGroup resourceQuantity, Player player, int price)
        {
            AuctionResources = resourceQuantity;
            Owner = player;
            AuctionPrice = price;
        }
    }
}