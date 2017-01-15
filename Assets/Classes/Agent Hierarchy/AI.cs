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
    private ResourceGroup optimalResourceFractions;

    public AI(ResourceGroup resources)
    {
        this.resources = resources;
        optimalResourceFractions = new ResourceGroup(33, 33, 34);   //Initialise to even resource weighting.

        //TEMP
        ResourceGroup testr = GetResourceNecessityWeights();
        MonoBehaviour.print("Necessities: " + testr.food + " , " + testr.energy + " , " + testr.ore);
    }

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
        // value corresponds to 0:100 representing necessity,
        // where 0 is not necessary at all.
        int totalResources = resources.food + resources.energy + resources.ore;
        ResourceGroup necessityWeights = new ResourceGroup();
        necessityWeights.food   = 50 + optimalResourceFractions.food - (int)(100 * resources.food / totalResources);
        necessityWeights.energy = 50 + optimalResourceFractions.energy - (int)(100 * resources.energy / totalResources);
        necessityWeights.ore    = 50 + optimalResourceFractions.ore - (int)(100 * resources.ore / totalResources);

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
        return new Tile(new ResourceGroup());
    }
}
