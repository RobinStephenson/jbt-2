using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager
{
    public string gameName;
    private List<Player> players;

    public GameManager(string gameName)
    {
        //TODO - Constructor for loading game with name gameName
        // from save files. Exception if no game exists with name.
    }

    public GameManager(string gameName, List<Player> players)
    {
        //TODO - Constructor for creating new game with name
        // gameName and players players.
    }

    public void StartGame()
    {
        //TODO - Initialise game
    }

    public void BeginAcquisitionPhase()
    {
        //TODO - phase initialisation logic
    }

    public void BeginPurchasePhase()
    {
        //TODO - phase initialisation logic
    }

    public void BeginInstallationPhase()
    {
        //TODO - phase initialisation logic
    }

    public void BeginProductionPhase()
    {
        //TODO - phase initialisation logic
    }

    public void BeginAuctionPhase()
    {
        //TODO - phase initialisation logic. Low priority
    }

    public void SaveGame(string gameName)
    {
        //TODO - use GameSaveFiles to save game.
    }
}
