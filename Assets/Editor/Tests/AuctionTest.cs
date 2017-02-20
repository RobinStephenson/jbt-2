using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class AuctionTest
{
    [Test]
    public void PutUpForAuctionSuccess()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);
        A.PutUpForAuction(new ResourceGroup(5, 0, 3), player1, 50);

        //Checks that the auction has been added to auctionListings correctly
        Assert.AreEqual(A.auctionListings[0].AuctionResources, new ResourceGroup(5, 0, 3));
        Assert.AreEqual(A.auctionListings[0].Owner, player1);
        Assert.AreEqual(A.auctionListings[0].AuctionPrice, 50);
    }

    [Test]
    public void PutUpForAuctionFail()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);

        //Checks that an exception is thrown if the player does not have enough resources to list the auction
        Assert.Throws<System.ArgumentOutOfRangeException>(() => A.PutUpForAuction(new ResourceGroup(6, 0, 3), player1, 50));
    }

    [Test]
    public void RetrieveAuction()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);
        Player player2 = new Human(new ResourceGroup(5, 5, 5), "name2", 500);
        //Checks that RetrieveAuction works properly on an empty list of auctions
        Assert.AreEqual(A.RetrieveAuction(player2), null);

        A.PutUpForAuction(new ResourceGroup(5, 0, 3), player1, 50);

        //Checks that it only retrieves the auction for the correct player
        Assert.AreEqual(A.RetrieveAuction(player1), null);
        Assert.AreEqual(A.RetrieveAuction(player2).AuctionResources, new ResourceGroup(5, 0, 3));
        Assert.AreEqual(A.RetrieveAuction(player2).Owner, player1);
    }

    [Test]
    public void AuctionBuySuccess()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);
        Player player2 = new Human(new ResourceGroup(5, 5, 5), "name2", 500);
        A.PutUpForAuction(new ResourceGroup(5, 0, 3), player1, 50);
        A.AuctionBuy(player2);

        //Checks that the resources and money have been transferred correctly
        Assert.AreEqual(player1.GetResources(), new ResourceGroup(0, 5, 2));
        Assert.AreEqual(player1.GetMoney(), 550);
        Assert.AreEqual(player2.GetResources(), new ResourceGroup(10, 5, 8));
        Assert.AreEqual(player2.GetMoney(), 450);

        //Checks that the auction has been removed from the list
        Assert.AreEqual(A.auctionListings.Count, 0);
    }

    [Test]
    public void AuctionBuyFail()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);
        Player player2 = new Human(new ResourceGroup(5, 5, 5), "name2", 500);
        A.PutUpForAuction(new ResourceGroup(5, 0, 3), player1, 550);

        //Checks that an exception is thrown if the player does not have enough money to buy the auction
        Assert.Throws<System.ArgumentOutOfRangeException>(() => A.AuctionBuy(player2));
    }

    [Test]
    public void ClearAuctions()
    {
        AuctionManager A = new AuctionManager();
        Player player1 = new Human(new ResourceGroup(5, 5, 5), "name", 500);
        A.PutUpForAuction(new ResourceGroup(5, 0, 3), player1, 550);
        A.ClearAuctions();

        //Checks that auctionListings is emptied after ClearAuctions is called
        Assert.AreEqual(A.auctionListings.Count, 0);
    }
}
