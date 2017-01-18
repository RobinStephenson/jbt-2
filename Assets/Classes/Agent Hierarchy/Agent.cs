using UnityEngine;
using System.Collections;

public abstract class Agent
{
    protected int money;
    protected ResourceGroup resources;

    public ResourceGroup GetResources()
    {
        return resources;
    }

    public void SetResources(ResourceGroup resourcesToSet)
    {
        resources = resourcesToSet;
    }

    public int GetMoney()
    {
        return money;   
    }

    public void SetMoney(int moneyToSet)
    {
        money = moneyToSet;
    }
}
