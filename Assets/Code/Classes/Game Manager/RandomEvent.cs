/* JBT changes to this file:
 * removed RandomEventFactory.cs, RandomEventStore.cs and moved all logic into the class as the system was 
 *     overcomplicated and there was nothing of use implemented anyway 
 * rewrote this file completely
 */

using System;
using System.Collections.Generic;

public class RandomEvent
{
    private static Random Random = new Random();
    private const int NumberOfResources = 3;

    /// <summary>
    /// The name of the event for display to the user
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// A detailed description of what the event does for display to the user
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// the duration of the event in turns
    /// </summary>
    public int Duration { get; private set; }

    /// <summary>
    /// the number of COMPLETE turns the event has been active
    /// </summary>
    public int CompleteTurnsElapsed { get; private set; }

    /// <summary>
    /// is the event finished
    /// </summary>
    public bool Finished
    {
        get
        {
            return Duration == CompleteTurnsElapsed;
        }
    }

    /// <summary>
    /// how many tiles should this event apply its effect to
    /// </summary>
    public int NumberOfTilesToAffect { get; private set; }

    /// A list of multipliers which should be applied to the resources a tile has produced
    /// Food Energy Ore
    /// </summary>
    private List<float> ResourceMultipliers = new List<float>(NumberOfResources);

    private List<Tile> AffectedTiles;

    // TODO add fields for something like the image that should be applied to the tile(s) to which this is applied()

    /// <summary>
    /// create a new type of random event
    /// </summary>
    /// <param name="name">The name of the event for display to the user</param>
    /// <param name="description">a detailed description of the event, for display to the user</param>
    /// <param name="duration">How many turns the event should last</param>
    /// <param name="numberOfTilesToAffect">how many tiles should the event affect</param>
    /// <param name="foodMult">the multiplier that should be applied to food production on affected tiles</param>
    /// <param name="energyMult">the multiplier that should be applied to energy production on affected tiles</param>
    /// <param name="oreMult">the multiplier that should be applied to ore production on affected tiles</param>
    public RandomEvent(string name, string description, int duration, int numberOfTilesToAffect, float foodMult, float energyMult, float oreMult)
    {
        if (name.Length == 0)
        {
            throw new ArgumentException("name cannot be empty");
        }
        if (description.Length == 0)
        {
            throw new ArgumentException("description cannot be empty");
        }
        if (duration <= 0)
        {
            throw new ArgumentOutOfRangeException("duration must be > 0");
        }
        if (foodMult < 0 || energyMult < 0 || oreMult < 0)
        {
            throw new ArgumentOutOfRangeException("resource multipliers cannot be < 0");
        }
        Name = name;
        Description = description;
        Duration = duration;
        NumberOfTilesToAffect = numberOfTilesToAffect;
        ResourceMultipliers.Add(foodMult);
        ResourceMultipliers.Add(energyMult);
        ResourceMultipliers.Add(oreMult);
        CompleteTurnsElapsed = 0;
    }

    public float GetFoodMultiplier()
    {
        return ResourceMultipliers[0];
    }

    public float GetEnergyMultiplier()
    {
        return ResourceMultipliers[1];
    }

    public float GetOreMultiplier()
    {
        return ResourceMultipliers[2];
    }

    /// <summary>
    /// should be called once whenever a turn is completed
    /// </summary>
    public void TurnCompleted()
    {
        if (Finished)
        {
            throw new InvalidOperationException("TurnCompleted should not be called if the event is finished");
        }

        CompleteTurnsElapsed++;
        if (Finished)
        {
            // The event is now over
            foreach (Tile CurrentTile in AffectedTiles)
            {
                CurrentTile.RemoveEvent();
            }
        }
    }

    /// <summary>
    /// Start the event
    /// </summary>
    public void Start(Map map)
    {
        CompleteTurnsElapsed = 0;

        // create a list of tiles with no event so that we can select tiles from them to apply an event to
        List<Tile> UnaffectedTiles = new List<Tile>();
        foreach (Tile CurrentTile in map.GetTiles())
        {
            if (CurrentTile.CurrentEvent == null)
            {
                UnaffectedTiles.Add(CurrentTile);
            }
        }

        // choose the tiles to affect
        while (AffectedTiles.Count < NumberOfTilesToAffect)
        {
            Tile ChosenTile = UnaffectedTiles[Random.Next(UnaffectedTiles.Count)];
            UnaffectedTiles.Remove(ChosenTile);
            AffectedTiles.Add(ChosenTile);
            ChosenTile.ApplyEvent(this);
        }
    }
}
