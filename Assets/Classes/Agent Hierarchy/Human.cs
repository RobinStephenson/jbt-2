using UnityEngine;
using System.Collections;

public class Human : Player
{
    private HumanGui humanGui;

    public Human(ResourceGroup resources, int money)
    {
        this.resources = resources;
        this.money = money;
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
