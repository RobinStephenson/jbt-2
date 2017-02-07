using System.Collections.Generic;
using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class GameManagerUnitTests
{
    [Test]
    public void GameWinTest()
    {
        List<Player> playerList = new List<Player>();
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave", 100));
        playerList.Add(new AI(new ResourceGroup(10, 10, 10), "tim", 100));
        playerList[1].AcquireRoboticon(new Roboticon(new ResourceGroup(1,1,1)));    //A roboticon always adds an amount > 0 to player score so this player should always win.

        GameHandler.CreateNew("test", playerList);
        GameManager gameManager = GameHandler.GetGameManager();

        foreach (Tile tile in gameManager.GetMap().GetTiles())
        {
            tile.SetOwner(playerList[1]);       //Set all tiles to owned so that the game ends
        }

        Player winner = gameManager.GetWinnerIfGameHasEnded();

        Assert.AreEqual(winner, playerList[1]);
    }
        
    [Test]
    public void HumanPlayersGoFirstTest()
    { 
        List<Player> playerList = new List<Player>();
        playerList.Add(new AI(new ResourceGroup(10, 10, 10), "leo", 100));
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave", 100));
        playerList.Add(new AI(new ResourceGroup(10, 10, 10), "leo2", 100));
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave2", 100));
        playerList.Add(new AI(new ResourceGroup(10, 10, 10), "leo3", 100));
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave3", 100));
        playerList.Add(new AI(new ResourceGroup(10, 10, 10), "leo4", 100));
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave4", 100));

        GameManager gameManager2 = new GameManager("test", playerList);

        Assert.IsTrue(playerList[0].IsHuman());
        Assert.IsTrue(playerList[1].IsHuman());
        Assert.IsTrue(playerList[2].IsHuman());
        Assert.IsTrue(playerList[3].IsHuman());
        Assert.IsFalse(playerList[4].IsHuman());
        Assert.IsFalse(playerList[5].IsHuman());
        Assert.IsFalse(playerList[6].IsHuman());
        Assert.IsFalse(playerList[7].IsHuman());
    }
}
