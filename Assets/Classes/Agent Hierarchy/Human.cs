using UnityEngine;
using System.Collections;

public class Human : Player
{
    public Human(ResourceGroup resources, int money)
    {
        this.resources = resources;
        this.money = money;
    }

    public override void Act()
    {
        //TODO - Interface with HumanGui to provide
        // Human actions.
    }
}
