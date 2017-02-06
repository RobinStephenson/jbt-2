// Game Executable hosted at: http://www-users.york.ac.uk/~jwa509/alpha01BugFree.exe

using UnityEngine;
using System.Collections;

public class Casino : MonoBehaviour
{
    private int fairness;

    public Casino(int fairness)
    {
        this.fairness = fairness;
    }

    public int GambleMoney(int gambleAmount)
    {
        int netAmount = -gambleAmount; //Losing is default
        int roll = Random.Range(0, 101);

        if (roll < this.fairness)
        {
            netAmount = gambleAmount + 2 * (gambleAmount); // First gambleAmount makes netAmount back to zero
        }

        return netAmount;
    }
}
