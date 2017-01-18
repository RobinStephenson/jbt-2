using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventStore
{  
    public List<GameObject> RegularEvents = new List<GameObject> {};    // TODO: Add GameObjects() for each regular event in Unity editor
    public List<GameObject> CrazyEvents = new List<GameObject> {};      // TODO: Add GameObjects() for each crazy event in Unity editor

    int regEventsLength = RegularEvents.Count();
    int crazyEventsLength = CrazyEvents.Count();

    public GameObject chooseEvent(int craziness)
    {
        Random rnd = new Random();
        if (craziness < 50)
        {
            return this.RegularEvents[rnd.NextInt(REGEVENTSLENGTH)];    // Choose a random event from the RegularEvents list
        }
        else
        {
            return this.CrazyEvents[rnd.NextInt(CRAZYEVENTSLENGTH)];    // Choose a random event from the CrazyEvents list
        }
    }
}
