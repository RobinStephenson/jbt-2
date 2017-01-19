using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventStore
{  
    public List<GameObject> RegularEvents = new List<GameObject>();    // TODO: Add GameObjects() for each regular event in Unity editor
    public List<GameObject> CrazyEvents = new List<GameObject>();      // TODO: Add GameObjects() for each crazy event in Unity editor

    int regEventsLength;
    int crazyEventsLength;

    public RandomEventStore()
    {
        regEventsLength = RegularEvents.Count;
        crazyEventsLength = CrazyEvents.Count;
    }

    public GameObject chooseEvent(int craziness)
    {
        if (craziness < 50)
        {
            return this.RegularEvents[UnityEngine.Random.Range(0, regEventsLength)];    // Choose a random event from the RegularEvents list
        }
        else
        {
            return this.CrazyEvents[UnityEngine.Random.Range(0, crazyEventsLength)];    // Choose a random event from the CrazyEvents list
        }
    }
}
