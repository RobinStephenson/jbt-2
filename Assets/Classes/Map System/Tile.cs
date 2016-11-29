using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
    private int tileId;
    private ResourceGroup resourcesGenerated;
    private Player owner;
    private List<Roboticon> installedRoboticons;

    public Tile(ResourceGroup resources, Player owner = null, int tileId = 0)
    {
        //TODO - implement constructor for tile
    }

    public void InstallRoboticon(Roboticon roboticon)
    {
        //TODO - check for already installed, if not install,
        // else raise exception
    }

    public void UninstallRoboticon(Roboticon roboticon)
    {
        //TODO - check for not installed, if not uninstall,
        // else raise exception
    }

    public List<Roboticon> GetInstalledRoboticons()
    {
        return installedRoboticons;
    }

    public int GetId()
    {
        return tileId;
    }

    public int GetPrice()
    {
        //TODO - Calculate price of tile based on resources
        // generated
        return 0;
    }

    public ResourceGroup GetResourcesGenerated()
    {
        //TODO - Calculate resources generated based on
        // installed roboticons and base resources.
        return new ResourceGroup();
    }


    public void SetOwner(Player player)
    {
        //TODO 
    }

    public Player GetOwner()
    {
        return owner;
    }
}
