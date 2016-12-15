using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameManager : System.Object
{
    public string gameName;
    private List<Player> players;

    enum States : int { Acquisition, Purchase, Installation,
       Production, Auction };

    public GameManager(string gameName, List<Player> players)
    {
        this.gameName = gameName;
        this.players = players;

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
}
