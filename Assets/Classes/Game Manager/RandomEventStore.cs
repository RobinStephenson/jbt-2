using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventStore
{   
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
    //Regular Events
    public GameObject EARTHQUAKE;
    public GameObject VOLCANO;
    public GameObject METEORSHOWER;
    public GameObject METEORSTRIKE;
    public GameObject SOLARFLARE;
    public GameObject DROUGHT;
    public GameObject FLOOD;
    public GameObject CAVERNS;
    public GameObject CLEARSKIES;
    public GameObject DONATION;
    public GameObject DIAMONDS;
    public GameObject TECHUPGRADE;
    //Crazy Events
    public GameObject THATCHERSREVENGE;
    public GameObject ALIENABDUCTION;
    public GameObject ALIENGIFTS;
    public GameObject STORAGEHUNTER;
    public GameObject GOOSEINFESTATION;
    public GameObject HANGOVER;
    public GameObject BUSLATE;
    public GameObject LAUNDRYPRICE;
    public GameObject CENTRALHALL;
    public GameObject BADPUBLICITY;

    public List<GameObject> RegularEvents = new List<GameObject> {
        EARTHQUAKE, VOLCANO, METEORSHOWER, METEORSTRIKE, SOLARFLARE, DROUGHT, FLOOD, CAVERNS, CLEARSKIES, DONATION, DIAMONDS, TECHUPGRADE };
    public List<GameObject> CrazyEvents = new List<GameObject> {
        THATCHERSREVENGE, ALIENABDUCTION, ALIENGIFTS, STORAGEHUNTER, GOOSEINFESTATION, HANGOVER, BUSLATE, LAUNDRYPRICE, CENTRALHALL, BADPUBLICITY };

    int REGEVENTSLENGTH = RegularEvents.Count();
    int CRAZYEVENTSLENGTH = CrazyEvents.Count();

    public GameObject chooseEvent(int craziness)
    {
        Random rnd = new Random();
        if (craziness < 50)
        {
            return this.RegularEvents[rnd.NextInt(REGEVENTSLENGTH)];
        }
        else
        {
            return this.CrazyEvents[rnd.NextInt(CRAZYEVENTSLENGTH)];
        }
    }
}
