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
}
