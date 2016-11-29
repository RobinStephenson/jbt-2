using UnityEngine;
using System.Collections;

public class ResourceGroup
{
    public int food;
    public int energy;
    public int ore;

    public ResourceGroup(int food = 0, int energy = 0, int ore = 0)
    {
        this.food = food;
        this.energy = energy;
        this.ore = ore;
    }

    public static ResourceGroup operator +(ResourceGroup c1, ResourceGroup c2)
    {
        //TODO - override + operator to represent element-wise
        // addition of resources.
        return new ResourceGroup();
    }

    public static ResourceGroup operator *(ResourceGroup c1, ResourceGroup c2)
    {
        //TODO - override * operator to represent element-wise
        // multiplication of resources.
        return new ResourceGroup();
    }
}
