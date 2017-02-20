﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tile
{
    public const float TILE_SIZE = 1.75f;
    public const int ROBOTICON_UPGRADE_WEIGHT = 1;  //Currently each roboticon upgrade adds this amount to the production of its resource
    public RandomEvent CurrentEvent { get; private set; }

    private int tileId;
    private ResourceGroup BaseResources; // JBT Renamed this from total resources as total resources better decribes resources inlcuding affects of events and roboticons
    private Player owner;
    private List<Roboticon> installedRoboticons = new List<Roboticon>();
    private TileObject tileObject;
    private bool tileIsSelected = false;

    public Tile(ResourceGroup resources, Vector2 mapDimensions, int tileId, Player owner = null)
    {
        this.BaseResources = resources;
        this.owner = owner;
        this.tileId = tileId;
        
        Vector2 tilePosition = new Vector2(tileId % mapDimensions.x, (int)(tileId / mapDimensions.y));
        this.tileObject = new TileObject(tileId, tilePosition, new Vector2(TILE_SIZE, TILE_SIZE));
    }

    /// <summary>
    /// Call when this tile is to be selected.
    /// </summary>
    public void TileSelected()
    {
        GameHandler.GetGameManager().GetHumanGui().DisplayTileInfo(this);
        tileObject.OnTileSelected();
    }

    public void TileHovered()
    {
        tileObject.OnTileHover();
    }

    /// <summary>
    /// Call to refresh a tile to its default colour based on ownership.
    /// </summary>
    public void TileNormal()
    {
        if (!tileIsSelected)
        {
            if (owner == null)
            {
                tileObject.OnTileNormal(TileObject.TILE_OWNER_TYPE.UNOWNED);
            }
            else if(owner == GameHandler.GetGameManager().GetCurrentPlayer())
            {
                tileObject.OnTileNormal(TileObject.TILE_OWNER_TYPE.CURRENT_PLAYER);
            }
            else
            {
                tileObject.OnTileNormal(TileObject.TILE_OWNER_TYPE.ENEMY);
            }
        }
    }

    //Edited by JBT to support the display on installed roboticons on tiles
    /// <summary>
    /// Throws System.Exception if the roboticon already exists on this tile.
    /// </summary>
    /// <param name="roboticon">The roboticon to install</param>
    public void InstallRoboticon(Roboticon roboticon)
    {
        if (installedRoboticons.Contains(roboticon))
        {
            throw new System.InvalidOperationException("Roboticon already exists on this tile");
        }
        installedRoboticons.Add(roboticon);
        tileObject.ShowInstalledRoboticon();
    }

    /// <summary>
    /// Throws System.Exception if the roboticon does not exist on this tile.
    /// </summary>
    /// <param name="roboticon">The roboticon to uninstall</param>
    public void UninstallRoboticon(Roboticon roboticon)
    {
        if (!installedRoboticons.Contains(roboticon))
        {
            throw new System.InvalidOperationException("Roboticon doesn't exist on this tile");
        }

        installedRoboticons.Remove(roboticon);

        //Added by JBT - remove the roboticon texture from a tile only if there are no roboticons left on it
        if (installedRoboticons.Count == 0)
        {
            tileObject.HideInstalledRoboticon();
        }
    }

    #region Install/Uninstall test functions
    //Added by JBT. The normal version will not work with tests as it references a tileobject, which NUnit cannot test
    /// <summary>
    /// Throws System.Exception if the roboticon already exists on this tile.
    /// </summary>
    /// <param name="roboticon">The roboticon to install</param>
    public void InstallRoboticonTest(Roboticon roboticon)
    {
        if (installedRoboticons.Contains(roboticon))
        {
            throw new System.InvalidOperationException("Roboticon already exists on this tile\n");
        }
        installedRoboticons.Add(roboticon);
    }

    //Added by JBT. The normal version will not work with tests as it references a tileobject, which NUnit cannot test
    /// <summary>
    /// Throws System.Exception if the roboticon does not exist on this tile.
    /// </summary>
    /// <param name="roboticon">The roboticon to uninstall</param>
    public void UninstallRoboticonTest(Roboticon roboticon)
    {
        if (!installedRoboticons.Contains(roboticon))
        {
            throw new System.InvalidOperationException("Roboticon doesn't exist on this tile\n");
        }

        installedRoboticons.Remove(roboticon);
    }
    #endregion

    public List<Roboticon> GetInstalledRoboticons()
    {
        return this.installedRoboticons;
    }

    public int GetId()
    {
        return this.tileId;
    }

    public int GetPrice()
    {
        return (this.BaseResources*(new ResourceGroup(10, 10, 10))).Sum();
    }

    /// <summary>
    /// Returns the total resources given by the tile
    /// takes into account roboticons and random events
    /// </summary>
    /// <returns>the total resources produced this turn</returns>
    public ResourceGroup GetTotalResourcesGenerated()
    {
        /* JBT Changes to this method:
         * once the resources have been calculated for the tile apply random events buffs/debuffs if there is a random event
         * fixed an error where the calulation modified the base resources
         */
        ResourceGroup totalResources = new ResourceGroup(BaseResources.food, BaseResources.energy, BaseResources.ore);

        //TODO - Diminishing returns for additional roboticons (currently linear)
        foreach(Roboticon roboticon in installedRoboticons)
        {
            totalResources += roboticon.GetUpgrades() * ROBOTICON_UPGRADE_WEIGHT;
        }

        if (CurrentEvent != null)
        {
            Debug.Log(String.Format("totalResources produced before event {0} food {1} energy {2} ore", totalResources.food, totalResources.energy, totalResources.ore));
        }

        // apply RandomEvent resource multipliers if one is applied
        if (CurrentEvent != null && !CurrentEvent.Finished)
        {
            totalResources.food = (int) (totalResources.food * CurrentEvent.GetFoodMultiplier());
            totalResources.energy = (int) (totalResources.energy * CurrentEvent.GetFoodMultiplier());
            totalResources.ore = (int) (totalResources.ore * CurrentEvent.GetOreMultiplier());
        }

        if (CurrentEvent != null)
        {
            Debug.Log(String.Format("totalResources produced after event {0} food {1} energy {2} ore", totalResources.food, totalResources.energy, totalResources.ore));
        }

        return totalResources;
    }

    /// <summary>
    /// Returns the base resources of this tile, not including roboticon yield.
    /// </summary>
    /// <returns></returns>
    public ResourceGroup GetBaseResourcesGenerated()
    {
        return BaseResources;
    }

    /// <summary>
    /// Instantiate the tile in the current scene.
    /// </summary>
    public void Instantiate(Vector3 mapCenterPosition)
    {
        tileObject.Instantiate(mapCenterPosition);
    }
    
    public void SetOwner(Player player)
    {
        this.owner = player;
        TileNormal();
    }

    public Player GetOwner()
    {
        return owner;
    }

    public TileObject GetTileObject()
    {
        return tileObject;
    }

    // Added by JBT
    public override int GetHashCode()
    {
        return tileId;
    }

    // Added by JBT to test tile equality
    public override bool Equals(object obj)
    {
        return ((Tile)obj).tileId == tileId;
    }

    // jbt created this method
    /// <summary>
    /// Apply a new random event to the tile
    /// </summary>
    /// <param name="newEvent">the event</param>
    public void ApplyEvent(RandomEvent newEvent)
    {
        if (CurrentEvent != null)
        {
            throw new InvalidOperationException("Event already applied to this tile");
        }
        CurrentEvent = newEvent;
    }

    // jbt created this method
    /// <summary>
    /// remove the event currently applied
    /// </summary>
    public void RemoveEvent()
    {
        if (CurrentEvent == null)
        {
            // throw an error as something might be going wrong if were trying to remove events from tiles without an event
            throw new InvalidOperationException("No event applied to this tile");
        }
        CurrentEvent = null;
    }
}
