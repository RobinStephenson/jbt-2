using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class RandomEventStore
{   //Regular Events
    public GameObject EARTHQUAKE = new GameObject();
    public GameObject VOLCANO = new GameObject();
    public GameObject METEORSHOWER = new GameObject();
    public GameObject METEORSTRIKE = new GameObject();
    public GameObject SOLARFLARE = new GameObject();
    public GameObject DROUGHT = new GameObject();
    public GameObject FLOOD = new GameObject();
    public GameObject CAVERNS = new GameObject();
    public GameObject CLEARSKIES = new GameObject();
    public GameObject DONATION = new GameObject();
    public GameObject DIAMONDS = new GameObject();
    public GameObject TECHUPGRADE = new GameObject();
    //Crazy Events
    public GameObject THATCHERSREVENGE = new GameObject();
    public GameObject ALIENABDUCTION = new GameObject();
    public GameObject ALIENGIFTS = new GameObject();
    public GameObject STORAGEHUNTER = new GameObject();
    public GameObject GOOSEINFESTATION = new GameObject();
    public GameObject HANGOVER = new GameObject();
    public GameObject BUSLATE = new GameObject();
    public GameObject LAUNDRYPRICE = new GameObject();
    public GameObject CENTRALHALL = new GameObject();
    public GameObject BADPUBLICITY = new GameObject();

    int REGEVENTSLENGTH = Enum.GetNames(typeof(regEvents)).Length;
    int CRAZYEVENTSLENGTH = Enum.GetNames(typeof(crazyEvents)).Length;

    public GameObject chooseEvent(int craziness)
    {
        Random rnd = new Random();
        if (craziness < 50)
        {
            int eventChoice = rnd.NextInt(REGEVENTSLENGTH);
            switch (eventChoice)
            {
                case regEvents.EARTHQUAKE:
                    return this.EARTHQUAKE;
                case regEvents.VOLCANO:
                    return this.VOLCANO;
                case regEvents.METEORSHOWER:
                    return this.METEORSHOWER;
                case regEvents.METEORSTRIKE:
                    return this.METEORSTRIKE;
                case regEvents.SOLARFLARE:
                    return this.SOLARFLARE;
                case regEvents.DROUGHT:
                    return this.DROUGHT;
                case regEvents.FLOOD:
                    return this.FLOOD;
                case regEvents.CAVERNS:
                    return this.CAVERNS;
                case regEvents.CLEARSKIES:
                    return this.CLEARSKIES;
                case regEvents.DONATION:
                    return this.DONATION;
                case regEvents.DIAMONDS:
                    return this.DIAMONDS;
                case regEvents.TECHUPGRADE:
                    return this.TECHUPGRADE;
            }
        }
        else
        {
            int eventChoice = rnd.NextInt(CRAZYEVENTSLENGTH);
            switch (eventChoice)
            {
                case crazyEvents.THATCHERSREVENGE:
                    return this.THATCHERSREVENGE;
                case crazyEvents.ALIENABDUCTION:
                    return this.ALIENABDUCTION;
                case crazyEvents.ALIENGIFTS:
                    return this.ALIENGIFTS;
                case crazyEvents.STORAGEHUNTER:
                    return this.STORAGEHUNTER;
                case crazyEvents.GOOSEINFESTATION:
                    return this.GOOSEINFESTATION;
                case crazyEvents.HANGOVER:
                    return this.HANGOVER;
                case crazyEvents.BUSLATE:
                    return this.BUSLATE;
                case crazyEvents.LAUNDRYPRICE:
                    return this.LAUNDRYPRICE;
                case crazyEvents.CENTRALHALL:
                    return this.CENTRALHALL;
                case crazyEvents.BADPUBLICITY:
                    return this.BADPUBLICITY;
            }
        }
    }
}
