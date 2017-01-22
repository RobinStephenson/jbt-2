using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTests : Map {

    public string TestMap()
    {
        return MapTest() + TileTest();
    }

    public string MapTest()
    {
        Map map = new Map();
        string errorString = "";

        //Test initialisation values
        if (map.tiles.Count != 100)
        {
            errorString += ($"Tile amount for test 2.3.0.1\nShould read {100}, actually reads {map.tiles.Count}\n\n");
        }

        //test GetTile()
        try
        {
            Tile testTile = map.GetTile(1000);
        }
        catch(System.IndexOutOfRangeException e)
        {
            bool caught = true;
        }
        finally
        {
            if(caught != true)
            {
                errorString += ($"Exception should have been thrown for test 2.3.1.1 for out of bounds values\n\n");
            }
        }

        //test GetNumUnownedTilesRemaining()
        //at this point no one should have yet acquired a tile and so the result of this should be 100
        int result = map.GetNumUnownedTilesRemaining();
        if (result != 100)
        {
            errorString += ($"Tile amount for test 2.3.2.1\nShould read {100}, actually reads {result}\n\n");
        }

        //test GetRandomResourceAmount()
        int result2 = GetRandomResourceAmount();
        if(!(result2 > 0 && result2 < MAX_TILE_RESOURCE_PRODUCTION + 1))
        {
            errorString += ($"RandomResourceAmount should be within the range 0-MAX_TILE_RESOURCE_PRODUCTION for test 2.3.3.1\n Actually reads {result2}\n\n");
        }

        //test GetRandomTileResources()
        ResourceGroup res = GetRandomTileResources();
        if (!(res.food > 0 && res.food < MAX_TILE_RESOURCE_PRODUCTION + 1))
        {
            if (!(res.ore > 0 && res.ore < MAX_TILE_RESOURCE_PRODUCTION + 1))
            {
                if (!(res.energy > 0 && res.energy < MAX_TILE_RESOURCE_PRODUCTION + 1))
                {
                    errorString += ($"ResourceGroup from GetRandomTileResources() should be within the range 0-MAX_TILE_RESOURCE_PRODUCTION for test 2.3.4.1\n Actually reads {res.food}, {res.ore}, {res.energy}\n\n");
                }
            }
        }

        return errorString;
    }

    public string TileTest()
    {
        Tile tile = new Tile(new ResourceGroup(10, 10, 10), new Vector2(3, 3), 7);

        //test constructor
        Vector2 pos = tile.tileObject.GetTilePosition();
        if(pos.x != 1 || pos.y != 2)
        {
            errorString += ($"Tile position calculated improperly for test 2.1.0.1\nShould have been (1,1) was actually {pos.x}, {pos.y}\n\n");
        }

        //test InstallRoboticon
        Roboticon roboticon = new Roboticon();
        tile.InstallRoboticon(roboticon);
        try
        {
            tile.InstallRoboticon(roboticon);
        }
        catch(System.Exception)
        {
            bool caught = true;
        }
        finally
        {
            if (!caught)
            {
                errorString += ($"No exception caught for test 2.1.1.1\nShould have thrown SystemException when trying to place the same Roboticon on the same tile\n\n");
            }
        }

        //test InstallRoboticon
        Roboticon roboticon2 = new Roboticon();
        try
        {
            tile.UninstallRoboticon(roboticon2);
        }
        catch (System.Exception)
        {
            bool caught = true;
        }
        finally
        {
            if (!caught)
            {
                errorString += ($"No exception caught for test 2.1.3.1\nShould have thrown SystemException when trying to place the same Roboticon on the same tile\n\n");
            }
        }

        //test GetPrice
        Tile tile2 = new Tile(new ResourceGroup(10, 10, 10), new Vector2(3, 3), 7);
        price = tile2.GetPrice();
        if(price != 300)
        {
            errorString += ($"Tile price for test 2.1.4.1\nShould read {300}, actually reads {price}\n\n");
        }

        //test GetTotalResourcesGenerated
        tile2.InstallRoboticon(new Roboticon());
        resources = tile2.GetTotalResourcesGenerated;
        actualResources = tile2.GetBaseResourcesGenerated() + tile2.installedRoboticons[0].GetUpgrades() * tile2.ROBOTICON_UPGRADE_WEIGHT;
        if(resources != actualResources)
        {
            errorString += ($"Amount of resources for 2.1.5.1\nShould read {actualResources}, actually reads {resources}\n\n");
        }

        return errorString;
    }
}
