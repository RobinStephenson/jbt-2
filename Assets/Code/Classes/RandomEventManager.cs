// This file was created by JBT

using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public static class RandomEventManager
{
    private static System.Random Random = new System.Random();

    private static List<RandomEvent> InactiveEvents = new List<RandomEvent>();

    private static List<RandomEvent> ActiveEvents = new List<RandomEvent>();

    /// <summary>
    /// Number between 0 and 1 (exclusive) for how often a random event should happen 
    /// </summary>
    public static float EventFrequency { get; private set; }

    /// <summary>
    /// Maximum number of events that can be active at the same time
    /// min = 1
    /// </summary>
    public static int MaxSimultaneousEvents { get; private set; }

    /// <summary>
    /// Initialise the randomevents available in the game
    /// </summary>
    public static void InitialiseEvents()
    {
        if (InactiveEvents.Count + ActiveEvents.Count > 0)
        {
            throw new InvalidOperationException("Events have already been initialised");
        }

        MaxSimultaneousEvents = 2;
        EventFrequency = 0.15f;

        TextAsset LoadedJson = (TextAsset)Resources.Load("Events");
        var ConfigurationsFile = JSON.Parse(LoadedJson.text);
        for (int i = 0; i < ConfigurationsFile["configurations"].Count; i++)
        {
            var EventConfiguration = ConfigurationsFile["configurations"][i];
            InactiveEvents.Add(
                new RandomEvent(
                    EventConfiguration["title"].Value,
                    EventConfiguration["description"].Value,
                    EventConfiguration["duration"].AsInt,
                    EventConfiguration["numberOfTilesToAffect"].AsInt,
                    EventConfiguration["connectedTilesOnly"].AsBool,
                    EventConfiguration["roboticonInstalled"].AsBool,
                    EventConfiguration["noRoboticonInstalled"].AsBool,
                    EventConfiguration["foodMultiplier"].AsFloat,
                    EventConfiguration["energyMultiplier"].AsFloat,
                    EventConfiguration["oreMultiplier"].AsFloat
                ));
        }

        Debug.Log(String.Format("{0} Events added to the game.", InactiveEvents.Count));

    }

    /// <summary>
    /// Manage current random events and trigger new ones
    /// Should only be called at the of both players production phase so that events are applied to both
    /// </summary>
    public static void ManageAndTriggerEvents()
    {
        Debug.Log("ManageAndTriggerEvents called");

        // Update currently active events
        foreach (RandomEvent activeEvent in ActiveEvents)
        {
            activeEvent.TurnCompleted();

            if (activeEvent.Finished)
            {
                Debug.Log(String.Format("Removing event {0}", activeEvent.Title));
                // the event is finished, move it to the inactive event list
                ActiveEvents.Remove(activeEvent);
                InactiveEvents.Add(activeEvent);

                // TODO Check if we need to update the map
            }
        }
        
        // Trigger new events
        if (ActiveEvents.Count < MaxSimultaneousEvents)
        {
            // 0 <= Random.NextDouble() < 1
            // so the higher EventFrequency is the more chance next double has of being < EventFrequency
            if (Random.NextDouble() < EventFrequency)
            {
                Debug.Log("Triggering a new event");
                TriggerNewEvent();
            }
            else
            {
                Debug.Log("Not triggering a new event this turn");
            }
        }
    }

    /// <summary>
    /// Trigger a randomly selected event
    /// </summary>
    private static void TriggerNewEvent()
    {
        RandomEvent NewEvent = InactiveEvents[Random.Next(InactiveEvents.Count)];
        InactiveEvents.Remove(NewEvent);
        ActiveEvents.Add(NewEvent); // TODO, this still adds events to active even if the start failed
        NewEvent.Start(GameHandler.GetGameManager().GetMap());
        Debug.Log(String.Format("Triggered a new event: {0}", NewEvent.Title));
    }
}
