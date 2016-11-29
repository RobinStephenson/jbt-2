using UnityEngine;
using System.Collections;

public class RandomEvent
{
    private GameObject eventGameObject;

    public RandomEvent(int craziness)
    {
        eventGameObject = new RandomEventFactory().create(craziness);
    }

    public void Instantiate()
    {
        GameObject.Instantiate(eventGameObject, Vector3.zero, Quaternion.identity);
    }
}
