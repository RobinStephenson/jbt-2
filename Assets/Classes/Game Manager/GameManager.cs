using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameManager : System.Object
{
    public enum States : int
    {
        ACQUISITION, PURCHASE, INSTALLATION, PRODUCTION, AUCTION
    };

    public string gameName;
    private List<Player> players;
    private int currentPlayerIndex;

    public Market market;
	private RandomEventFactory randomEventFactory;
	private Map map;

    private States currentState = States.ACQUISITION;
    private HumanGui humanGui;

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
		this.randomEventFactory = new RandomEventFactory();
		this.map = new Map();
    }

    public void StartGame()
    {
        humanGui = new HumanGui();
        GameObject guiGameObject = GameObject.Instantiate(HumanGui.humanGuiGameObject);
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
            else
            {
                break;
            }
        }

        humanGui.DisplayGui((Human)players[0], currentState); //players[0] will always be a human player. (See FormatPlayerList)
        PlayerAct();
    }

    public void CurrentPlayerEndTurn()
    {
        PlayerAct();
    }

	private void PlayerAct()
	{
        //Check that the current player exists, if not then we have iterated through all players and need to move on to the next stage.
		if (currentPlayerIndex >= players.Count)
		{
            //If we've moved on to the production phase, run the function that handles the logic for the production phase.
            if (currentState == States.PRODUCTION)
            {
                ProcessProductionPhase();
                currentState = States.ACQUISITION;       //Reset the state counter after the production (final) phase
            }
            else
            {
                currentState++;
            }

            currentPlayerIndex = 0;
		}

        MonoBehaviour.print("Calling act on player:  " + currentPlayerIndex + "Human?   " + (players[currentPlayerIndex].GetType() == typeof(Human)) );

        //Call the Act function for the current player, passing the state to it.
		players[currentPlayerIndex].Act(currentState);
        currentPlayerIndex++;
	}

	private Player GetWinnerIfGameHasEnded()
	{
		//Game ends if there are no remaining unowned tiles (Req 2.3.a)
		if(map.GetNumUnownedTilesRemaining() == 0)
		{
			float highestScore = Mathf.NegativeInfinity;
            Player winner = null;

			for(int i = 0; i < players.Count; i++)
			{
                //Player with the highest score wins (Req 2.3.c)
                int currentScore = players[i].CalculateScore();
				if(currentScore > highestScore)
				{
                    highestScore = currentScore;
                    winner = players[i];
				}
			}

			if(highestScore != Mathf.NegativeInfinity)
			{
				return winner;
			}
		}

        return null;
	}

	private void ShowWinner(Player player)
	{
		//Handle exiting the game, showing a winner screen (leaderboard) and returning to main menu
	}

    private void ProcessProductionPhase()
    {
        Player winner = GetWinnerIfGameHasEnded();
        if(winner != null)
        {
            ShowWinner(winner);
            return;
        }

        for(int i = 0; i < players.Count; i++)
        {
            players[i].Produce();
        }

        //Instantiate a random event (probability handled in the randomEventFactory) (Req 2.5.a, 2.5.b)
        GameObject randomEventGameObject = randomEventFactory.Create(UnityEngine.Random.Range(0, 101));

        if (randomEventGameObject != null)
        {
            GameObject.Instantiate(randomEventGameObject);
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
            bool p1IsHuman = p1.GetType().ToString() == "Human";
            bool p2IsHuman = p2.GetType().ToString() == "Human";

            if (p1IsHuman && p2IsHuman)
            {
                return 0;
            }
            else if(p1IsHuman)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });
        
        if (players[0].GetType().ToString() != "Human")
        {
            throw new System.ArgumentException("GameManager was given a player list not containing any Human players.");
        }
    }
}
