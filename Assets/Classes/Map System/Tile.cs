using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
    private int tileId;
    private ResourceGroup resourcesGenerated;
    private Player owner;
    private List<Roboticon> installedRoboticons;
    private TileObject tileObject;
    private Map map;

    public const TILE_SIZE = 10;

    public Tile(ResourceGroup resources, Map map, Player owner = null, int tileId = 0)
    {
        this.resourcesGenerated = resources;
        this.owner = owner;
        this.tileId = tileId;
        this.map = map;
        
        Vector2 tilePosition = new Vector2(tileId % map.MAP_DIMENSIONS.x, (int)(tileId / map.MAP_DIMENSIONS.y));
        this.tileObject = new TileObject(tilePosition, new Vector2(TILE_SIZE, TILE_SIZE));
    }

    public void InstallRoboticon(Roboticon roboticon)
    {
        for(int i = 0; i < roboticon.Count; i++)
        {
            if(this.installedRoboticons[i] == roboticon)
            {
                throw new Exception("Roboticon already exists on this tile\n");
            }
            else
            {
                installedRoboticons.Add(roboticon);
            }
        }
    }

    public void UninstallRoboticon(Roboticon roboticon)
    {
      for(int i = 0; i < roboticon.Count; i++)
      {
          if(this.installedRoboticons[i] == roboticon)
          {
              this.installedRoboticons.Remove(roboticon);
          }
          else
          {
              throw new Exception("Roboticon doesn't exist on this tile\n");
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

    public ResourceGroup GetResourcesGenerated()
    {
      for(int i = 0; i < roboticon.Count; i++)
      {
          return this.resourcesGenerated*(ithis.nstalledRoboticons[i].GetUpgrades());
      }
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
