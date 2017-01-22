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
    private bool tileIsSelected = false;

    public const float TILE_SIZE = 1.75f;
    public const int ROBOTICON_UPGRADE_WEIGHT = 5;  //Currently each roboticon upgrade adds this amount to the production of its resource

    public Tile(ResourceGroup resources, Map map, int tileId, Player owner = null)
    {
        this.resourcesGenerated = resources;
        this.owner = owner;
        this.tileId = tileId;
        this.map = map;
        
        Vector2 tilePosition = new Vector2(tileId % map.MAP_DIMENSIONS.x, (int)(tileId / map.MAP_DIMENSIONS.y));
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

    /// <summary>
    /// Returns the base resources of this tile, not including roboticon yield.
    /// </summary>
    /// <returns></returns>
    public ResourceGroup GetBaseResourcesGenerated()
    {
        return resourcesGenerated;
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
}
