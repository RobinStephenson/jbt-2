/* JBT Changes to this file
 * Updated constructor to use crazy as a bool not an int
 */

using UnityEngine;
using System.Collections;

public class RandomEvent
{
    private GameObject eventGameObject;

    public RandomEvent(bool crazy)
    {
        eventGameObject = new RandomEventFactory().Create(crazy);
    }

    public void Instantiate()
    {
        GameObject.Instantiate(eventGameObject, Vector3.zero, Quaternion.identity);
    }
}
