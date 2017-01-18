using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameManager : System.Object
{
  public string gameName;
  private List<Player> players;
	private int currentPlayer;

	private Market market;
	private RandomEventFactory factory;
	private Map map;

  enum States : int
  {
    ACQUISITION, PURCHASE, INSTALLATION, PRODUCTION, AUCTION
  };
	private States currentState = States.ACQUISITION;

  public GameManager(string gameName, List<Player> players)
  {
    this.gameName = gameName;
    this.players = players;
		this.market = new Market(new ResourceGroup(100, 100, 100), new ResourceGroup(100, 100, 100), 200);
		this.randomEventFactory = new RandomEventFactory();
		this.map = new Map();
  }

  public void NextState()
  {
	   currentState++;
	}

	public void PlayerAct()
	{
	   //Check that the current player exists, if not then we have iterated through all players and need to move on to the next stage.
		if (currentPlayer >= players.Count)
		{
			//If we've moved on to the production phase then run the function that handles the logic for the production phase.
			if(currentState == States.PRODUCTION)
			{
				ProcessProductionPhase();
			}

			currentState++;
		}

		//Call the Act function for the current player, passing the state to it.
		players[currentPlayer].Act(currentState);
		currentPlayer++;
	}

	private Player GetWinnerIfGameHasEnded()
	{
		//Game ends if there are no remaining tiles (Req 2.3.a)
		if(map.GetRemainingTiles() == 0)
		{
			highestScore = Mathf.NegativeInfinity;
			winner = Mathf.Infinity;

			for(int i = 0; i < players.Count; i++)
			{
				//Player with the highest score wins (Req 2.3.c)
				if(players[i].GetScore() > highestScore)
				{
					highestScore = players[i].GetScore();
					winner = i;
				}
			}

			if(highestScore != Mathf.NegativeInfinity)
			{
				return players[winner];
			}
			else
			{
				return null;
			}
		}
	}

	private void ShowWinner(Player player)
	{
		//Handle exiting the game, showing a winner screen (leaderboard) and returning to main menu
	}

  public void ProcessProductionPhase()
  {
	  winner = GetWinnerIfGameHasEnded();
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
		randomEventFactory.Create(Random.Range(0, 101)).Instantiate();
		market.UpdatePrices();
  }
}
