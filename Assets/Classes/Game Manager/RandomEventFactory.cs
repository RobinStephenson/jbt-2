using UnityEngine;
using System.Collections;
using System;



public class RandomEventFactory
{
    private RandomEventStore Store = new RandomEventStore();
    public GameObject create(int craziness)
    {
        Random rnd = new Random();
        if (rnd.Next(2) == 0)           //TODO correct percentage chance of event occuring - 50% currently
        {
            return Store.chooseEvent(craziness);    
        } else
        {
            return new GameObject();    // Return empty gameobject indicating no event should take place
        }
    }
}
