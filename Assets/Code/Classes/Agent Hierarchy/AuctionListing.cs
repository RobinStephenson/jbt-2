using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AuctionListing
{
    private int auctionID;
    private int price;
    private ResourceGroup resources;
    private Player owner;

    /// <summary>
    /// Creating an auction lot with its price and resource type
    /// </summary>
    /// <param name="resourceType">Type of resource</param>
    /// <param name="startPrice">Price of auction</param>
    /// <param name="player">Player creating the auction</param>
    public AuctionListing(int id, int startPrice, ResourceGroup resourceQuantity, Player player)
    {
        auctionID = id;
        resources = resourceQuantity;
        price = startPrice;
        owner = player;
    }


}
