/* JBT Changes to this file:
 * replaced regular and crazyEventLength with Regular CrazyEvents.Count
 * throw an exception when there are no events rather than returning null
 * made crazy a bool rather than int as thats how it was being used
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventStore
{  
    public List<GameObject> RegularEvents = new List<GameObject>();    // TODO: Add GameObjects() for each regular event in Unity editor
    public List<GameObject> CrazyEvents = new List<GameObject>();      // TODO: Add GameObjects() for each crazy event in Unity editor

    public RandomEventStore()
    {
    }

    public GameObject chooseEvent(bool crazy)
    {
        if(RegularEvents.Count == 0 && CrazyEvents.Count == 0)
        {
            throw new InvalidOperationException("No events in the store");
        }

        if (crazy)
        {
            if (CrazyEvents.Count > 0)
            {
                return this.CrazyEvents[UnityEngine.Random.Range(0, CrazyEvents.Count)];    // Choose a random event from the CrazyEvents list
            }
            else
            {
                return this.CrazyEvents[UnityEngine.Random.Range(0, RegularEvents.Count)];    // No crazy events have been set. Use a regular event.
            }
        }
        else
        {
            if (RegularEvents.Count > 0)
            {
                return this.RegularEvents[UnityEngine.Random.Range(0, RegularEvents.Count)];    // Choose a random event from the RegularEvents list
            }
            else
            {
                return this.CrazyEvents[UnityEngine.Random.Range(0, CrazyEvents.Count)];    // No regular events have been set. Use a crazy event.
            }
        }
    }
}
