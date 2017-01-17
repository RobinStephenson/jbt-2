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
        return new ResourceGroup(c1.getFood() + c2.getFood(),
                                 c1.getEnergy() + c2.getEnergy(),
                                 c1.getOre() + c2.getOre());
    }

    public static ResourceGroup operator *(ResourceGroup c1, ResourceGroup c2)
    {
        return new ResourceGroup(c1.getFood() * c2.getFood(),
                                 c1.getEnergy() * c2.getEnergy(),
                                 c1.getOre() * c2.getOre());
    }

    public int getFood()
    {
        return this.food();
    }

    public int getEnergy()
    {
        return this.energy();
    }

    public int getOre()
    {
        return this.ore();
    }
    public static void Main()
    {
        ResourceGroup r1 = new ResourceGroup(50, 50, 50);
        ResourceGroup r2 = new ResourceGroup(30, 30, 50);
        r1 = r1 + r2;
        Console.WriteLine(r1.food, r1.energy, r1.ore);
    }
}


