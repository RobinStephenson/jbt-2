using UnityEngine;
using System.Collections;

public class Human : Player
{
    public Human(ResourceGroup resources)
    {
        this.resources = resources;
    }

    public override void Act()
    {
        //TODO - Interface with HumanGui to provide
        // Human actions.
    }
}
