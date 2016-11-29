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

    public override void Act()
    {
        //TODO - AI action
    }

    private Tile ChooseTileToAcquire()
    {
        //TODO - intelligent decision of best tile in map.
        return new Tile(new ResourceGroup());
    }

    private Roboticon.RoboticonUpgrade ChooseBestRoboticonUpgrade(Roboticon roboticon)
    {
        //TODO - intelligent decision of best upgrade.
        return Roboticon.RoboticonUpgrade.ENERGY;
    }

    private ResourceGroup GetResourceNecessityWeights()
    {
        //TODO - generate resource group where each resource
        // value corresponds to 0:1 representing necessity,
        // where 0 is not necessary at all.
        return new ResourceGroup();
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
        return new Tile(new ResourceGroup());
    }
}
