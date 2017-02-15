/* JBT changes to this file:
 * removed RandomEventFactory.cs, RandomEventStore.cs and moved all logic into the class as the system was 
 *     overcomplicated and there was nothing of use implemented anyway 
 * rewrote this file completely
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent
{
    private static System.Random Random = new System.Random();

    /// <summary>
    /// the number of resources in the game
    /// </summary>
    private const int NumberOfResources = 3;

    /// <summary>
    /// The title of the event for display to the user
    /// </summary>
    public string Title { get; private set; }

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

    /// <summary>
    /// should the event only be applied to tiles which are connected
    /// </summary>
    public bool AffectConnectedTilesOnly { get; private set; }

    /// <summary>
    /// should the event be applied to tiles which have a roboticon installed
    /// </summary>
    public bool AffectTilesWithRoboticonInstalled { get; private set; }

    /// <summary>
    /// should the event be applied to tiles which have no roboticon installed
    /// </summary>
    public bool AffectTilesWithoutRoboticonInstalled { get; private set; }

    /// A list of multipliers which should be applied to the resources a tile has produced
    /// Food Energy Ore
    /// </summary>
    private List<float> ResourceMultipliers = new List<float>(NumberOfResources);

    /// <summary>
    /// Tiles this event is currently affecting
    /// </summary>
    private List<Tile> AffectedTiles = new List<Tile>();

    // TODO add fields for something like the image that should be applied to the tile(s) to which this is applied()

    /// <summary>
    /// create a new type of random event
    /// </summary>
    /// <param name="tilte">The title of the event for display to the user</param>
    /// <param name="description">a detailed description of the event, for display to the user</param>
    /// <param name="duration">How many turns the event should last</param>
    /// <param name="numberOfTilesToAffect">how many tiles should the event affect</param>
    /// <param name="connectedOnly">Should the event affect only connected tiles</param>
    /// <param name="roboticonInstalled">Should the event affect tiles with roboticons installed</param>
    /// <param name="noRoboticonInstalled">Should the event affect tiles without a roboticon installed</param>
    /// <param name="foodMult">the multiplier that should be applied to food production on affected tiles</param>
    /// <param name="energyMult">the multiplier that should be applied to energy production on affected tiles</param>
    /// <param name="oreMult">the multiplier that should be applied to ore production on affected tiles</param>
    public RandomEvent(string tilte, string description, int duration, int numberOfTilesToAffect, bool connectedOnly, bool roboticonInstalled, bool noRoboticonInstalled, float foodMult, float energyMult, float oreMult)
    {
        if (tilte.Length == 0)
        {
            throw new ArgumentException("title cannot be empty");
        }
        if (description.Length == 0)
        {
            throw new ArgumentException("description cannot be empty");
        }
        if (duration <= 0)
        {
            throw new ArgumentOutOfRangeException("duration must be > 0");
        }
        if (numberOfTilesToAffect <- 0)
        {
            throw new ArgumentOutOfRangeException("must affect at least 1 tiles");
        }
        if (!roboticonInstalled && !noRoboticonInstalled)
        {
            // this leaves no tiles which can be affected
            throw new ArgumentException("no tiles can be affected in this configuration");
        }
        if (foodMult < 0 || energyMult < 0 || oreMult < 0)
        {
            throw new ArgumentOutOfRangeException("resource multipliers cannot be < 0");
        }
        Title = tilte;
        Description = description;
        Duration = duration;
        NumberOfTilesToAffect = numberOfTilesToAffect;
        AffectConnectedTilesOnly = connectedOnly;
        AffectTilesWithRoboticonInstalled = roboticonInstalled;
        AffectTilesWithoutRoboticonInstalled = noRoboticonInstalled;
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
            AffectedTiles.ForEach(tile => tile.RemoveEvent());
        }
    }

    /// <summary>
    /// Start the event
    /// </summary>
    public void Start(Map map)
    {
        // Set this events state to its default
        Reset();
 
        // get a list of all the tiles this event COULD be applied to
        List<Tile> PossibleTiles = GetPossibleTiles(map);
        if (PossibleTiles.Count < NumberOfTilesToAffect)
        {
            Debug.Log(String.Format("Failed to start event: {0}", Title));
            throw new InvalidOperationException("Not enough suitable tiles to start this event");
        }

        // choose the tiles to apply it to
        List<Tile> ChosenTiles = new List<Tile>();
        if (AffectConnectedTilesOnly)
        {
            ChosenTiles = GetRandomConnectedTiles(PossibleTiles, map);
        }
        else
        {
            ChosenTiles = GetRandomTiles(PossibleTiles);
        }

        // apply the event to each chosen tile
        foreach (Tile tile in ChosenTiles)
        {
            tile.ApplyEvent(this);
            AffectedTiles.Add(tile);
        }

        Debug.Log(String.Format("Event: {0} applied to how many tiles: {1}", Title, ChosenTiles.Count));
    }

    private List<Tile> GetPossibleTiles(Map map)
    {
        // create a list of tiles with no event so that we can select tiles from them to apply an event to
        List<Tile> PossibleTiles = new List<Tile>();
        foreach (Tile CurrentTile in map.GetTiles())
        {
            if (CurrentTile.CurrentEvent == null)
            {
                bool TileHasRoboticon = CurrentTile.GetInstalledRoboticons().Count > 0;
                if ((TileHasRoboticon && AffectTilesWithRoboticonInstalled) || (!TileHasRoboticon && AffectTilesWithoutRoboticonInstalled))
                {
                    PossibleTiles.Add(CurrentTile);
                }
            }

        }
        return PossibleTiles;
    }

    /// <summary>
    /// Get a random selection of connected (next to each other) tiles. Quantity returned is the number of tiles this event should affect
    /// </summary>
    /// <param name="possibleTiles">a list of tiles this event can be applied to. ie no tiles with events already applied</param>
    /// <param name="map">the game map</param>
    /// <exception cref="ArgumentException">Thrown when no solution is possible</exception>
    /// <returns>n random tiles where n = NumberOfTilesToAffect, and the tiles are a subset of possible tiles. And the tiles are all connected to at least one of the others.</returns>
    private List<Tile> GetRandomConnectedTiles(List<Tile> possibleTiles, Map map)
    {
        // A copy of the paramater so we dont modify the original
        List<Tile> PossibleTiles = new List<Tile>(possibleTiles);

        // Contains the current solution (can be partial)
        List<Tile> ChosenTiles = new List<Tile>(NumberOfTilesToAffect);

        // Contains all neighbours of chosentiles not in chosen tiles
        List<Tile> Neighbours = new List<Tile>();

        // because selection is random (could get unlucky and never pick tiles successfully) we limit how many attempts at finding connected tiles we make
        int Attempts = 0;
        const int MaxAttempts = 25;
        do
        {
            // pick a random tile from possible tiles, add to chosen tiles, remove from possible, update neigbours
            int RandomIndex = Random.Next(PossibleTiles.Count);
            Tile ChosenTile = PossibleTiles[RandomIndex];
            ChosenTiles.Add(ChosenTile);
            PossibleTiles.Remove(ChosenTile);
            Neighbours = UpdatePossibleNeighbours(Neighbours, ChosenTile, PossibleTiles, map);

            // try to grow the selection
            while (ChosenTiles.Count < NumberOfTilesToAffect)
            {
                // if there are no neighbours start the search again
                if (Neighbours.Count == 0)
                {
                    break;
                }

                // choose a new random neighbour
                RandomIndex = Random.Next(Neighbours.Count);
                ChosenTile = Neighbours[RandomIndex];
                ChosenTiles.Add(ChosenTile);
                Neighbours.Remove(ChosenTile);
                PossibleTiles.Remove(ChosenTile);
                Neighbours = UpdatePossibleNeighbours(Neighbours, ChosenTile, PossibleTiles, map);
            }

            if (ChosenTiles.Count == NumberOfTilesToAffect)
            {
                return ChosenTiles;
            }
            else
            {
                Attempts++;
            }
        } while (Attempts < MaxAttempts);
        throw new InvalidOperationException("Solution took to long to find, or does not exist");    
    }

    /// <summary>
    /// Get an Updated list of neighbours, all of which are possible tiles
    /// </summary>
    /// <param name="ExistingNeighbours">List of all currently found neighbours</param>
    /// <param name="NewTile">The new tile whos neighbours are being added</param>
    /// <param name="PossibleTiles">All new neigbours must also be in this list</param>
    /// <returns>Existing Neighbours + (NewTile's neighbours that are also in PossibleTiles)</returns>
    private List<Tile> UpdatePossibleNeighbours(List<Tile> ExistingNeighbours, Tile NewTile, List<Tile> PossibleTiles, Map map)
    {
        List<Tile> UpdatedNeighbours = new List<Tile>(ExistingNeighbours);
        foreach (Tile NewNeighbour in map.GetTileNeighbours(NewTile))
        {
            if (PossibleTiles.Contains(NewNeighbour))
            {
                UpdatedNeighbours.Add(NewNeighbour);
            }
        }
        return UpdatedNeighbours;
    }
    
    /// <summary>
    /// Get a random selection of tiles from those given. Quantity returned is the number of tiles this event should affect
    /// </summary>
    /// <param name="possibleTiles">a list of tiles this event can be applied to. ie no tiles with events already applied</param>
    /// <returns>n random tiles where n = NumberOfTilesToAffect, and the tiles are a subset of possible tiles.</returns>
    private List<Tile> GetRandomTiles(List<Tile> possibleTiles)
    {
        List<Tile> ChosenTiles = new List<Tile>(NumberOfTilesToAffect);
        while (ChosenTiles.Count < NumberOfTilesToAffect)
        {
            int RandomIndex = Random.Next(possibleTiles.Count);
            ChosenTiles.Add(possibleTiles[RandomIndex]);
            possibleTiles.RemoveAt(RandomIndex);
        }
        return ChosenTiles;
    }

    /// <summary>
    /// reset this event to its default values
    /// </summary>
    private void Reset()
    { 
        CompleteTurnsElapsed = 0;
        AffectedTiles.Clear();
    }
}
