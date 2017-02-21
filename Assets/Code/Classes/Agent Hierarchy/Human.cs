//Game executable hosted by JBT at: http://robins.tech/jbt/documents/assthree/GameExecutable.zip

using UnityEngine;
using System.Collections;

/// <summary>
/// Handles interaction with Player data
/// </summary>
public class Human : Player
{
    private HumanGui humanGui;

    public Human(ResourceGroup resources, string name, int money)
    {
        this.resources = resources;
        this.money = money;
        this.name = name;
    }

    public void SetHumanGui(HumanGui gui)
    {
        humanGui = gui;
    }

    public override void Act(GameManager.States state)
    {
        humanGui.DisplayGui(this, state);
    }
}
