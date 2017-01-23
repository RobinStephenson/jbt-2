using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest{

    public string TestGameManager()
    {

        string errorString = "";

        //test GetWinnerIfGameHasEnded

        List<Player> playerList = new List<Player>();
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "dave", 100));
        playerList.Add(new Human(new ResourceGroup(10, 10, 10), "tim", 100));
        playerList[1].AcquireRoboticon(new Roboticon());    //A roboticon always adds an amount > 0 to player score so this player should always win.

        GameManager gameManager = new GameManager("test", playerList);

        Player winner = gameManager.GetWinnerIfGameHasEnded();
        if(winner != playerList[1])
        {
            errorString += "GetWinnerIfGameHasEnded selected the wrong winner for test 3.2.1. Selected player: {}, should have selected player: tim";
        }

        //test initial game setup (FormatPlayerList)

        List<Player> playerList2 = new List<Player>();
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10), "leo", 100));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10), "leo2", 100));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10), "leo3", 100));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10), "leo4", 100));
        playerList2.Add(new Human(new ResourceGroup(10, 10, 10), "dave", 100));
        playerList2.Add(new Human(new ResourceGroup(10, 10, 10), "dave2", 100));
        playerList2.Add(new Human(new ResourceGroup(10, 10, 10), "dave3", 100));
        playerList2.Add(new Human(new ResourceGroup(10, 10, 10), "dave4", 100));

        GameManager gameManager2 = new GameManager("test2", playerList2);

        if(playerList2[0].IsHuman() != true)
        {
            errorString += "FormatPlayerList does not work for test 3.2.2. First player is not a human";
        }

        return errorString;
    }
}
