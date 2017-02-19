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
    public void PutUpForAuction(ResourceGroup resources, Player player)
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
            if (curAuction.Owner != player)
            {
                return curAuction;
            }
        }
        return null;
    }

    public void PlaceOffer(Player player, ResourceGroup price, ResourceGroup requestedResources)
    {
        if (RetrieveAuction(player) != null)
        {
            Auction auction = RetrieveAuction(player);
            ResourceGroup resourceDifference = auction.AuctionResources - requestedResources;
            if ((resourceDifference.food < 0) || (resourceDifference.energy < 0) || (resourceDifference.ore < 0))
            {
                throw new ArgumentOutOfRangeException("Not enough resources in auction");
            }
            else if (player.GetMoney() < (requestedResources * price).Sum())
            {
                throw new ArgumentOutOfRangeException("Not enough money");
            }
            else
            {
                auction.SetOffer(price, requestedResources);
            }
        }

    }

    /// <summary>
    /// Updates players resources and money if the offer is accepted.
    /// </summary>
    /// <param name="auction">The auction being accepted</param>
    /// <param name="playerBuy">The pleyer buying the resources</param>
    public void AcceptOffer(Auction auction, Player playerBuy)
    {
        playerBuy.SetResources(playerBuy.GetResources() + auction.OfferQuantity);
        playerBuy.SetMoney(playerBuy.GetMoney() - auction.OfferPrice.Sum());
        auction.Owner.SetResources(auction.Owner.GetResources() - auction.OfferQuantity);
        auction.Owner.SetMoney(auction.Owner.GetMoney() + auction.OfferPrice.Sum());
    }

    public void ClearAuctions()
    {
        auctionListings = new List<Auction>();
    }

    public class Auction
    {
        public ResourceGroup OfferPrice { get; private set; }
        public ResourceGroup OfferQuantity { get; private set; }
        public ResourceGroup AuctionResources { get; private set; }
        public Player Owner { get; private set; }

        /// <summary>
        /// Creating an auction lot with its price and resource type
        /// </summary>
        /// <param name="resourceType">Type of resource</param>
        /// <param name="startPrice">Price of auction</param>
        /// <param name="player">Player creating the auction</param>
        public Auction(ResourceGroup resourceQuantity, Player player)
        {
            AuctionResources = resourceQuantity;
            Owner = player;
        }

        public void SetOffer(ResourceGroup price, ResourceGroup quantity)
        {
            OfferPrice = price;
            OfferQuantity = quantity;
        }
    }

}