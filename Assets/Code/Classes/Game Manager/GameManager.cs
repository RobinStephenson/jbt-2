﻿/* JBT Changes to this file
 * removed references to old events system
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum States : int
    {
        ACQUISITION, PURCHASE, INSTALLATION, PRODUCTION, AUCTION
    };
    public static string[] stateNames = new string[5] {
        "Acquisition",
        "Purchase",
        "Installation",
        "Production",
        "Auction"
        };

    public GameObject humanGuiCanvas;
    public Market market;
    public string gameName;

    private List<Player> players;
    private int currentPlayerIndex;
    private Map map;
    private States currentState = States.ACQUISITION;
    private HumanGui humanGui;

    public static string StateToPhaseName(States state)
    {
        return GameManager.stateNames[(int)state];
    }

    /// <summary>
    /// Don't use this constructor. Use the CreateNew method of the GameHandler object.
    /// Throws System.ArgumentException if given a player list with no human players.
    /// </summary>
    /// <param name="gameName"></param>
    /// <param name="players"></param>
    public GameManager(string gameName, List<Player> players)
    {
        this.gameName = gameName;
        this.players = players;
        FormatPlayerList(this.players);
		this.market = new Market();
		this.map = new Map();
    }

    public void StartGame()
    {
        SetUpGui();
        SetUpMap();
        RandomEventManager.InitialiseEvents();
        PlayerAct();
    }

    public void CurrentPlayerEndTurn()
    {
        PlayerAct();
    }

    public GameManager.States GetCurrentState()
    {
        return currentState;
    }

    public Player GetCurrentPlayer()
    {
        if (currentPlayerIndex == 0)
        {
            return players[players.Count - 1];
        }
        else
        {
            return players[currentPlayerIndex - 1];
        }
    }

    //Amended by JBT - Added UI Integration
    private void ShowWinner(List<ScoreboardEntry> scoreboard)
    {
        endGameScript.Scoreboard = scoreboard;
        SceneManager.LoadScene(2);
    }

    //Added by JBT to create scoreboard
    public List<ScoreboardEntry> PlayerScoreBoard(List<Player> playerList)
    {
        List<ScoreboardEntry> scoreboard = new List<ScoreboardEntry>();

        foreach(Player p in playerList)
        {
            scoreboard.Add(new ScoreboardEntry(p.GetName(), p.CalculateScore()));
        }

        scoreboard.Sort((a, b) => a.PlayerScore.CompareTo(b.PlayerScore));
        
        //Player with the highest score wins
        return scoreboard;
    }

    //Added by JBT to support a scoreboard being displayed when the game ends, instead of a singular winner
    public bool GameEnded()
    {
        //Game ends if there are no remaining unowned tiles
        return map.GetNumUnownedTilesRemaining() == 0;
    }

    private void SetUpGui()
    {
        humanGui = new HumanGui();
        GameObject guiGameObject = GameObject.Instantiate(HumanGui.humanGuiGameObject);
        guiGameObject.SetActive(true);
        MonoBehaviour.DontDestroyOnLoad(guiGameObject);

        canvasScript canvas = guiGameObject.GetComponent<canvasScript>();
        humanGui.SetCanvasScript(canvas);
        humanGui.SetGameManager(this);
        canvas.SetHumanGui(humanGui);

        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.GetType() == typeof(Human))
            {
                ((Human)players[i]).SetHumanGui(humanGui);  //Set a reference to the humanGui in each human player
            }
        }

        humanGui.DisplayGui((Human)players[0], currentState); //players[0] will always be a human player. (See FormatPlayerList)
    }

    private void SetUpMap()
    {
        map = new Map();
        map.Instantiate();
    }

    /* JBT Changes to this method
     * call the random event manager at the start of the first players production phase
     * call the ApplyPhaseTimeout method if the player is human
     * added checks to whether the current player is a human or AI to display the gui accordingly
     */
    private void PlayerAct()
    {
        //Check that the current player exists, if not then we have iterated through all players and need to move on to the next stage.
        if (currentPlayerIndex >= players.Count)
        {
            //If we've moved on to the production phase, run the function that handles the logic for the production phase.
            if (currentState == States.PRODUCTION)
            {
                RandomEventManager.ManageAndTriggerEvents();
                ProcessProductionPhase();
                currentState = States.ACQUISITION;       //Reset the state counter after the production (final) phase
            }
            else
            {
                currentState++;
            }
            currentPlayerIndex = 0;
        }

        //Call the Act function for the current player, passing the state to it.
        Player currentPlayer = players[currentPlayerIndex];

        humanGui.DisableGui();  //Disable the Gui in between turns. Re-enabled in the human Act function.
        humanGui.SetCurrentPlayerName(currentPlayer.GetName());
        
        if (currentPlayer is Human)
        {
            ApplyPhaseTimeout(currentState);
            currentPlayer.Act(currentState);
        }
        else if(currentPlayer is AI)
        {
            humanGui.DisplayAIInfo((AI)currentPlayer, currentState);
            ApplyAITimeout();
        }

        currentPlayerIndex++;
        map.UpdateMap();
    }

    /// <summary>
    /// if the current phase is timelimited, set the timeout, otherwise set it to null
    /// </summary>
    /// <param name="currentState"></param>
    private void ApplyPhaseTimeout(States currentState)
    {
        // if we are in a timelimited phase create a timeout, otherwise set the timeout to null
        Timeout CurrentPhaseTimeout = null;
        if (currentState == States.PURCHASE)
        {
            CurrentPhaseTimeout = new Timeout(40);
        }
        else if (currentState == States.INSTALLATION)
        {
            CurrentPhaseTimeout = new Timeout(45);
        }
        else if (currentState == States.AUCTION)
        {
            CurrentPhaseTimeout = new Timeout(60);
        }
        humanGui.GetCanvas().SetPhaseTimeout(CurrentPhaseTimeout);
    }
    
    //Created by JBT to simulate an AI taking its turn
    /// <summary>
    /// Set the current AI timeout, so there is a delay whilst they make their turn
    /// </summary>
    private void ApplyAITimeout()
    {
        humanGui.GetCanvas().SetPhaseTimeout(new Timeout(1));
    }

    //Amended by JBT to add GameEnd functionality
    private void ProcessProductionPhase()
    {
        if(GameEnded())
        {
            ShowWinner(PlayerScoreBoard(players));
            return;
        }

        for(int i = 0; i < players.Count; i++)
        {
            players[i].Produce();
        }

        market.UpdatePrices();
    }

    /// <summary>
    /// Sorts the player list so that human players always go first. Mutates players.
    /// </summary>
    /// <param name="players"></param>
    /// <returns></returns>
    private void FormatPlayerList(List<Player> players)
    {
        players.Sort(delegate(Player p1, Player p2)
        {
            if (p1.IsHuman() && p2.IsHuman())
            {
                return 0;
            }
            else if(p1.IsHuman())
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
        
        if (!players[0].IsHuman())
        {
            throw new System.ArgumentException("GameManager was given a player list not containing any Human players.");
        }
    }

    public Map GetMap()
    {
        return map;
    }

    public HumanGui GetHumanGui()
    {
        return humanGui;
    }
}

//Added by JBT to facilitate the construction of the scorboard
public struct ScoreboardEntry
{
    public string PlayerName { get; private set; }
    public int PlayerScore { get; private set; }

    public ScoreboardEntry(string pn, int ps)
    {
        PlayerName = pn;
        PlayerScore = ps;
    }
}
