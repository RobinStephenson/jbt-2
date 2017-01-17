using UnityEngine;
using System.Collections;
using System;

enum regEvents
{
    EARTHQUAKE,
    VOLCANO,
    METEORSHOWER,
    METEORSTRIKE,
    SOLARFLARE,
    DROUGHT,
    FLOOD,
    CAVERNS,
    CLEARSKIES,
    DONATION,
    DIAMONDS,
    TECHUPGRADE
}
enum crazyEvents
{ 
    THATCHERSREVENGE,
    ALIENABDUCTION,
    ALIENGIFTS,
    STORAGEHUNTER,
    GOOSEINFESTATION,
    HANGOVER,
    BUSLATE,
    LAUNDRYPRICE,
    CENTRALHALL,
    BADPUBLICITY
};

public class RandomEventFactory
{
    int regEventsLength = Enum.GetNames(typeof(regEvents)).Length;
    int crazyEventsLength = Enum.GetNames(typeof(crazyEvents)).Length;

    public GameObject create(int craziness)
    {
        Random rnd = new Random();
        if (rnd.Next(2) == 0)           //TODO correct percentage chance of event occuring - 50% currently
        {
            return new GameObject();    // Return empty gameobject indicating no event should take place
        }
        if (craziness < 50)
        {
            int eventChoice = rnd.NextInt(regEventsLength); 
            switch (eventChoice)
            {
                case regEvents.EARTHQUAKE:
                    break;
                case regEvents.VOLCANO:
                    break;
                case regEvents.METEORSHOWER:
                    break;
                case regEvents.METEORSTRIKE:
                    break;
                case regEvents.SOLARFLARE:
                    break;
                case regEvents.DROUGHT:
                    break;
                case regEvents.FLOOD:
                    break;
                case regEvents.CAVERNS:
                    break;
                case regEvents.CLEARSKIES:
                    break;
                case regEvents.DONATION:
                    break;
                case regEvents.DIAMONDS:
                    break;
                case regEvents.TECHUPGRADE:
                    break;
            }
        } else
        {
            int eventChoice = rnd.NextInt(crazyEventsLength);
            switch (eventChoice)
            {
                case crazyEvents.THATCHERSREVENGE:
                    break;
                case crazyEvents.ALIENABDUCTION:
                    break;
                case crazyEvents.ALIENGIFTS:
                    break;
                case crazyEvents.STORAGEHUNTER:
                    break;
                case crazyEvents.GOOSEINFESTATION:
                    break;
                case crazyEvents.HANGOVER:
                    break;
                case crazyEvents.BUSLATE:
                    break;
                case crazyEvents.LAUNDRYPRICE:
                    break;
                case crazyEvents.CENTRALHALL:
                    break;
                case crazyEvents.BADPUBLICITY:
                    break;
            }
        }
    }
}
