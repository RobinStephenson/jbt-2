using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class MapUnitTests
{
    [Test]
    public void InitialiseMap()
    {
        Map m = new Map();

        Assert.AreEqual(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y, m.GetTiles().Count);
    }

    [Test]
    public void OutOfBoundsTest()
    {
        Map m = new Map();

        Assert.Throws<System.IndexOutOfRangeException>(() => m.GetTile((int)(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y) + 2));
    }

    [Test]
    public void UnownedTilesRemainingTest()
    {
        Map m = new Map();

        Assert.AreEqual(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y, m.GetNumUnownedTilesRemaining());
    }

    
    private string TileTest()
    {
        Tile tile = new Tile(new ResourceGroup(10, 10, 10), new Vector2(3, 3), 7);
        string errorString = "";

        //test constructor
        Vector2 pos = tile.GetTileObject().GetTilePosition();
        if(pos.x != 1 || pos.y != 2)
        {
            errorString += (string.Format("Tile position calculated improperly for test 2.1.0.1\nShould have been (1,2) was actually {0}, {1}\n\n", pos.x, pos.y));
        }

        //test InstallRoboticon
        Roboticon roboticon = new Roboticon();
        tile.InstallRoboticon(roboticon);

        bool caught = false;
        try
        {
            tile.InstallRoboticon(roboticon);
        }
        catch(System.Exception)
        {
            caught = true;
        }
        finally
        {
            if (!caught)
            {
                errorString += (string.Format("No exception caught for test 2.1.1.1\nShould have thrown SystemException when trying to place the same Roboticon on the same tile\n\n"));
            }
        }

        //test InstallRoboticon
        Roboticon roboticon2 = new Roboticon();
        caught = false;

        try
        {
            tile.UninstallRoboticon(roboticon2);
        }
        catch (System.Exception)
        {
            caught = true;
        }
        finally
        {
            if (!caught)
            {
                errorString += ("No exception caught for test 2.1.3.1\nShould have thrown SystemException when trying to place the same Roboticon on the same tile\n\n");
            }
        }

        //test GetPrice
        Tile tile2 = new Tile(new ResourceGroup(10, 10, 10), new Vector2(3, 3), 7);
        int price = tile2.GetPrice();
        if(price != 300)
        {
            errorString += (string.Format("Tile price for test 2.1.4.1\nShould read 300, actually reads {0}\n\n", price));
        }

        //test GetTotalResourcesGenerated
        tile2.InstallRoboticon(new Roboticon());
        ResourceGroup resources = tile2.GetTotalResourcesGenerated();
        ResourceGroup actualResources = tile2.GetBaseResourcesGenerated() + tile2.GetInstalledRoboticons()[0].GetUpgrades() * Tile.ROBOTICON_UPGRADE_WEIGHT;
        if(!resources.Equals(actualResources))
        {
            errorString += (string.Format("Amount of resources for 2.1.5.1\nShould read {0}, actually reads {1}\n\n", actualResources.ToString(), resources.ToString()));
        }

        return errorString;
    }
}
