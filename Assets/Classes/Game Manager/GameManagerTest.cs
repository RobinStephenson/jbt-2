using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : GameManager {

    public string TestGameManager()
    {

        string errorString = "";

        //test GetWinnerIfGameHasEnded

        List<Player> playerList = new List<Player>();
        playerList.Add(new Player(new ResourceGroup(10, 10, 10), "dave", 100));
        playerList.Add(new Player(new ResourceGroup(10, 10, 10), "tim", 100));
        playerList[1].AcquireRoboticon(new Roboticon());

        GameManager gameManager = new GameManager("test", playerList);

        winner = GetWinnerIfGameHasEnded();
        if(winner != playerList[1])
        {
            errorString += "Winning player not accurately found";
        }

        //test FormatPlayerList

        List<Player> playerList2 = new List<Player>();
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10, "leo", 100)));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10, "leo2", 100)));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10, "leo3", 100)));
        playerList2.Add(new AI(new ResourceGroup(10, 10, 10, "leo4", 100)));
        playerList2.Add(new Player(new ResourceGroup(10, 10, 10, "dave", 100)));
        playerList2.Add(new Player(new ResourceGroup(10, 10, 10, "dave2", 100)));
        playerList2.Add(new Player(new ResourceGroup(10, 10, 10, "dave3", 100)));
        playerList2.Add(new Player(new ResourceGroup(10, 10, 10, "dave4", 100)));

        GameManager gameManager2 = new GameManager("test2", playerList2);

        gameManager2.FormatPlayerList(playerList2);

        if(playerList2[0].IsHuman() != true)
        {
            errorString += "That sorting thing doesn't work";
        }

    }
	
}
