using UnityEngine;
using System.Collections;

public class Casino : MonoBehaviour
{
    private int fairness;

    public Casino(int fairness)
    {
        this.fairness = fairness
    }


    public int GambleMoney(int gambleAmount)
    {
        //TODO - Return integer = amount of money gained
        // or lost from the gamble. Use fairness variable.
        Random rnd = new Random();
        int netAmount = -gambleAmount; //Losing is default
        double roll = rnd.Next(10) / 10;
        if (roll < this.fairness)
        {
            netAmount = gambleAmount + 2 * (gambleAmount); // First gambleAmount makes netAmount back to zero
        }

        return netAmount;
    }
}
