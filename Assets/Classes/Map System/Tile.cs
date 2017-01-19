using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
    private int tileId;
    private ResourceGroup resourcesGenerated;
    private Player owner;
    private List<Roboticon> installedRoboticons = new List<Roboticon>();
    private TileObject tileObject;
    private Map map;

    public const int TILE_SIZE = 10;
    public const int ROBOTICON_UPGRADE_WEIGHT = 5;  //Currently each roboticon upgrade adds this amount to the production of its resource

    public Tile(ResourceGroup resources, Map map, Player owner = null, int tileId = 0)
    {
        this.resourcesGenerated = resources;
        this.owner = owner;
        this.tileId = tileId;
        this.map = map;
        
        Vector2 tilePosition = new Vector2(tileId % map.MAP_DIMENSIONS.x, (int)(tileId / map.MAP_DIMENSIONS.y));
        this.tileObject = new TileObject(tilePosition, new Vector2(TILE_SIZE, TILE_SIZE));
    }

    /// <summary>
    /// Throws System.Exception if the roboticon already exists on this tile.
    /// </summary>
    /// <param name="roboticon"></param>
    public void InstallRoboticon(Roboticon roboticon)
    {
        for(int i = 0; i < installedRoboticons.Count; i++)
        {
            if(this.installedRoboticons[i] == roboticon)
            {
                throw new System.Exception("Roboticon already exists on this tile\n");
            }
            else
            {
                installedRoboticons.Add(roboticon);
            }
        }
    }

    /// <summary>
    /// Throws System.Exception if the roboticon does not exist on this tile.
    /// </summary>
    /// <param name="roboticon"></param>
    public void UninstallRoboticon(Roboticon roboticon)
    {
      for(int i = 0; i < installedRoboticons.Count; i++)
      {
          if(this.installedRoboticons[i] == roboticon)
          {
              this.installedRoboticons.Remove(roboticon);
          }
          else
          {
              throw new System.Exception("Roboticon doesn't exist on this tile\n");
          }
      }
    }

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
        return (this.resourcesGenerated*(new ResourceGroup(10, 10, 10))).Sum();
    }

    /// <summary>
    /// Returns the total resources given by the tile plus any additional yield from roboticons.
    /// </summary>
    /// <returns></returns>
    public ResourceGroup GetTotalResourcesGenerated()
    {
        ResourceGroup totalResources = resourcesGenerated;

        //TODO - Diminishing returns for additional roboticons (currently linear)
        foreach(Roboticon roboticon in installedRoboticons)
        {
            totalResources += roboticon.GetUpgrades() * ROBOTICON_UPGRADE_WEIGHT;
        }

        return totalResources;
    }


    public void SetOwner(Player player)
    {
        this.owner = player;
    }

    public Player GetOwner()
    {
        return owner;
    }
}
