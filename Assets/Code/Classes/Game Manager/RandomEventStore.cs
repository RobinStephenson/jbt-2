using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventStore
{  
    public List<GameObject> regularEvents = new List<GameObject>();    // TODO: Add GameObjects() for each regular event in Unity editor
    public List<GameObject> crazyEvents = new List<GameObject>();      // TODO: Add GameObjects() for each crazy event in Unity editor

    int regEventsLength;
    int crazyEventsLength;

    public RandomEventStore()
    {
        regEventsLength = regularEvents.Count;
        crazyEventsLength = crazyEvents.Count;
    }

    public GameObject chooseEvent(int craziness)
    {
        if(regularEvents.Count == 0 && crazyEvents.Count == 0)
        {
            Debug.LogWarning("No random events to instantiate.");
            return null;
        }

        if (craziness < 50)
        {
            if (regularEvents.Count > 0)
            {
                return this.regularEvents[UnityEngine.Random.Range(0, regEventsLength)];    // Choose a random event from the RegularEvents list
            }
            else
            {
                return this.crazyEvents[UnityEngine.Random.Range(0, crazyEventsLength)];    // No regular events have been set. Use a crazy event.
            }
        }
        else
        {
            if (crazyEvents.Count > 0)
            {
                return this.crazyEvents[UnityEngine.Random.Range(0, crazyEventsLength)];    // Choose a random event from the CrazyEvents list
            }
            else
            {
                return this.crazyEvents[UnityEngine.Random.Range(0, crazyEventsLength)];    // No crazy events have been set. Use a regular event.
            }
        }
    }
}
