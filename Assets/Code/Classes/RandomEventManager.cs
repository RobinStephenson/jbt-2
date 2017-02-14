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
        var Configurations = JSON.Parse(LoadedJson.text);

    }

    /// <summary>
    /// Manage current random events and trigger new ones
    /// Should only be called at the start of the first players production phase so that events are applied to 
    /// </summary>
    /// <param name="State">The current state of the game</param>
    /// <param name="currentPlayer">0 for first player, 1 for second player etc.</param>
    public static void ManageAndTriggerEvents(GameManager.States state, int currentPlayer)
    {
        // paramaters are only used to try and ensure this is called at the right time
        // this method should only be called at the start of the first players production phase so that if effects both players production
        // if its called at the end of the first players for example, the event wont effect their tiles but it would effect player 2 which is unfair

        if (state != GameManager.States.PRODUCTION || currentPlayer != 0)
        {
            throw new InvalidOperationException("Must be called ONLY at the start production phase of the first player");
        }

        // Update currently active events
        foreach (RandomEvent activeEvent in ActiveEvents)
        {
            activeEvent.TurnCompleted();

            if (activeEvent.Finished)
            {
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
                TriggerNewEvent();
            }
        }
    }

    private static void TriggerNewEvent()
    {
        RandomEvent NewEvent = InactiveEvents[Random.Next(InactiveEvents.Count)];
        InactiveEvents.Remove(NewEvent);
        ActiveEvents.Add(NewEvent);
        NewEvent.Start(GameHandler.GetGameManager().GetMap());
    }
}
