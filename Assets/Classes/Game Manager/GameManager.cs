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
    }

    public void GameLoop()
    {
        // This boolean will be set to false upon a player winning or a player
        // ending the game prematurely.
        bool running = true;
        int state = 0;

        while (running)
        {
            switch (state)
            {
                case (int)States.ACQUISITION:
                    BeginAcquisitionPhase();
                    while (!PlayerReadyCheck())
                    {
                        //Need to wait or something here.
                    }
                    state++;
                    break;
                case (int)States.PURCHASE:
                    BeginPurchasePhase();
                    while (!PlayerReadyCheck())
                    {
                        //Need to wait or something here.
                    }
                    state++;
                    break;
                case (int)States.INSTALLATION:
                    BeginInstallationPhase();
                    while (!PlayerReadyCheck())
                    {
                        //Need to wait or something here.
                    }
                    state++;
                    break;
                case (int)States.PRODUCTION:
                    BeginProductionPhase();
                    while (!PlayerReadyCheck())
                    {
                        //Need to wait or something here.
                    }
                    state++;
                    break;
                case (int)States.AUCTION:
                    BeginAuctionPhase();
                    while (!PlayerReadyCheck())
                    {
                        //Need to wait or something here.
                    }
                    state = 0;
                    break;
            }
        }
    }

    bool PlayerReadyCheck()
    {
        // Check that all players have finished their tasks, or time has
        // elapsed. Return true if ok to proceed.
        return false;
    }

    public void StartGame()
    {
        //TODO - Initialise game
    }

    public void BeginAcquisitionPhase()
    {
        //each player needs an opportunity to act in this turn, by calling their
        //Act() method.
        for(int i = 0; i < players.Count; i++){
            //potentially display a message stating which players turn it is
            player.Act()
        }
    }

    public void BeginPurchasePhase()
    {
        //TODO - phase initialisation logic
        //each player needs an opportunity to act in this turn, by calling their
        //Act() method.
        for(int i = 0; i < players.Count; i++){
            //potentially display a message stating which players turn it is
            player.Act()
        }
    }

    public void BeginInstallationPhase()
    {
        //TODO - phase initialisation logic
        //each player needs an opportunity to act in this turn, by calling their
        //Act() method.
        for(int i = 0; i < players.Count; i++){
            //potentially display a message stating which players turn it is
            player.Act()
        }
    }

    public void BeginProductionPhase()
    {
        //TODO - phase initialisation logic
    }

    public void BeginAuctionPhase()
    {
        //TODO - phase initialisation logic. Low priority
        //each player needs an opportunity to act in this turn, by calling their
        //Act() method.
        for(int i = 0; i < players.Count; i++){
            //potentially display a message stating which players turn it is
            player.Act()
        }
    }
}
