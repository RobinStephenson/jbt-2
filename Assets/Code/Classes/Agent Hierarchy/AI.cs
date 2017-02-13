using UnityEngine;
using System.Collections;
using System;

public class AI : Player
{
    private enum DifficultyLevel
    {
        EASY,
        MEDIUM,
        HARD
    }

    private DifficultyLevel difficulty;
    private ResourceGroup optimalResourceFractions = new ResourceGroup(33, 33, 34);     //The AI will attempt to meet this resource distribution.

    public AI(ResourceGroup resources, string name, int money)
    {
        this.name = name;
        this.resources = resources;
    }

    public override void Act(GameManager.States state)
    {
        //TODO - AI action
        switch(state)
        {
            case GameManager.States.ACQUISITION:
                Tile tileToAcquire = ChooseTileToAcquire();

                if (tileToAcquire.GetOwner() == null)
                {
                    AcquireTile(tileToAcquire);
                }
                break;
        }

        GameHandler.GetGameManager().CurrentPlayerEndTurn();     //This must be done to signify the end of the AI turn.
    }

    private Tile ChooseTileToAcquire()
    {
        //TODO - intelligent decision of best tile in map.
        Map map = GameHandler.GetGameManager().GetMap();
        int numTiles = (int)(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y);

        return map.GetTile(UnityEngine.Random.Range(0, numTiles));
    }

    private ResourceGroup ChooseBestRoboticonUpgrade(Roboticon roboticon)
    {
        //TODO - intelligent decision of best upgrade.
        return new ResourceGroup(1, 0, 0);
    }

    /// <summary>
    /// Returns a resource group in which each resource value signifies
    /// the necessity of that resource from 0 to 100, where 0 is not 
    /// necessary at all and 100 is absolutely necessary.
    /// </summary>
    /// <returns></returns>
    private ResourceGroup GetResourceNecessityWeights()
    {
        int totalResources = resources.food + resources.energy + resources.ore;
        ResourceGroup necessityWeights;

        if (totalResources != 0)
        {
            necessityWeights = new ResourceGroup();
            necessityWeights.food   = 50 + optimalResourceFractions.food   - (int)(100 * resources.food   / totalResources);
            necessityWeights.energy = 50 + optimalResourceFractions.energy - (int)(100 * resources.energy / totalResources);
            necessityWeights.ore    = 50 + optimalResourceFractions.ore    - (int)(100 * resources.ore    / totalResources);
        }
        else
        {
            necessityWeights = optimalResourceFractions;
        }

        return necessityWeights;
    }

    private bool ShouldPurchaseRoboticon()
    {
        //TODO - decide if new roboticon purchase is 
        // justified.
        return false;
    }

    public Tile GetOptimalTileForRoboticon(Roboticon roboticon)
    {
        //TODO - decide best tile for supplied roboticon.
        return null;
    }
}
