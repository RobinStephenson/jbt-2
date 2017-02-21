using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class canvasScript : MonoBehaviour
{
    public helpBoxScript helpBox;
    public GameObject optionsMenu;
    public GameObject gambleButton; //JBT used to track Gamble UI button
    public GameObject marketButton; //JBT used to track Market UI button
    public GameObject roboticonButton;  //JBT used to track roboticons UI button
    public roboticonWindowScript roboticonList;
    public GameObject gamblingWindow;
    public marketScript marketScript;
    public GameObject endPhaseButton;
    public tileInfoWindowScript tileWindow;
    public Text currentPlayerText;
    public Text currentPhaseText;
    public Text timeoutText;
    public GameObject aiTurnBox;
    public Text aiTurnText;
    public roboticonUpgradesWindowScript roboticonUpgradesWindow;
    public Text NewEventTitle; //JBT Title of event displayed to user
    public Text NewEventDescription; //JBT Description of event displayed to user
    public GameObject NewEventMessage; //JBT UI element displayed when a new event is started.
    private Timeout CurrentPhaseTimeout; //JBT used to limit phase durations
    private Timeout EventMessageTimeout; //JBT used to display the new event message for a few seconds
    private GameObject eventSystem; //JBT used to create and track the eventsystem in the scene
    public Texture2D tex;

    /// <summary>
    /// Tracks all of the player's resources within the UI
    /// </summary>
    #region Resource Labels
    public Text foodLabel;
    public Text foodChangeLabel;
    public Text energyLabel;
    public Text energyChangeLabel;
    public Text oreLabel;
    public Text oreChangeLabel;
    public Text moneyLabel;
    #endregion

    private HumanGui humanGui;
     
    // JBT created this method
    void Update()
    {
        //If the current phase has a Time restriction then...
        if (CurrentPhaseTimeout != null)
        {
            // We are in a timed phase, update the display timer
            ShowTimeout(CurrentPhaseTimeout);

            /// If the timeout is finished then...
            if (CurrentPhaseTimeout.Finished)
            {
                Debug.Log("Current Timeout Finished");
                //Set the phase time to null
                CurrentPhaseTimeout = null;

                //Move the game onto the next phase
                if (GameHandler.gameManager.GetCurrentPlayer() is Human)
                {
                    EndPhase();
                }
                else if(GameHandler.gameManager.GetCurrentPlayer() is AI)
                {
                    GameHandler.gameManager.GetCurrentPlayer().Act(GameHandler.gameManager.GetCurrentState());
                }
            }
        }
        //Else if the phase is not timed then...
        else
        {
            //Hide the phase timer
            HidePhaseTimeout();
        }

        //If there is an event in effect then...
        if (EventMessageTimeout != null)
        {
            //Check if the event's message should still be displayed
            if (EventMessageTimeout.Finished)
            {
                NewEventMessage.SetActive(false);
                EventMessageTimeout = null;
            }
        }

        //Create an event system if there is not already one in the scene.
        SetEventSystem();
    }

    // JBT Created this method
    /// <summary>
    /// Create an Event system if there is not already one in the scene
    /// </summary>
    public void SetEventSystem()
    {
        //If there is an event system then return
        if (eventSystem != null)
            return;

        //else make a new event system
        eventSystem = new GameObject();
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    // JBT Created this method
    /// <summary>
    /// Dispaly information about a new event that has started for a few seconds
    /// </summary>
    /// <param name="newEvent">The event that has started</param>
    public void DisplayNewEventMessage(RandomEvent newEvent)
    {
        NewEventTitle.text = newEvent.Title;
        NewEventDescription.text = newEvent.Description;
        NewEventMessage.SetActive(true);
        EventMessageTimeout = new Timeout(8);
    }

    // JBT created this method
    /// <summary>
    /// Set a timout for the current phase
    /// </summary>
    public void SetPhaseTimeout(Timeout timeout)
    {
        if (timeout != null && timeout.Finished)
        {
            throw new ArgumentException("Need a fresh timeout");
        }
        CurrentPhaseTimeout = timeout;
        Debug.Log(String.Format("Set a new timeout {0}", timeout));
    }

    // this is called by the end phase button
    public void EndPhase()
    {
        CurrentPhaseTimeout = null;
        humanGui.EndPhase();
    }

    // This is called inbetween turns to prevent any actions being registered while the game swaps players. 
    public void DisableEndPhaseButton()
    {
        endPhaseButton.SetActive(false);
    }

    // This is called when the GUI is started and re-set for each player's turn. 
    public void EnableEndPhaseButton()
    {
        endPhaseButton.SetActive(true);
    }

    // This is called at the start of each player's turn.
    public void SetCurrentPhaseText(string text)
    {
        currentPhaseText.text = text;
    }

    //This is called when the player attempts to buy something from the market. 
    public void BuyFromMarket(ResourceGroup resources, int roboticonsToBuy, int price)
    {
        humanGui.BuyFromMarket(resources, roboticonsToBuy, price);
        RefreshRoboticonList(); //Added by JBT to fix a bug when buying roboticons with the roboticon list open was not creating the GUI elements correctly
    }

    //This is called when the player attempts to sell something to the market.
    public void SellToMarket(ResourceGroup resources, int price)
    {
        humanGui.SellToMarket(resources, price);
    }

    //Added by JBT 
    //Show or hide the gambling window depending on the state the window is in when the button is pressed
    public void GamblingButtonPressed()
    {
        if(gamblingWindow.activeSelf)
        {
            HideGamblingWindow();
        }
        else
        {
            ShowGamblingWindow();
        }
    }

    //Added by JBT 
    //Show or hide the market window depending on the state the window is in when the button is pressed
    public void MarketButtonPressed()
    {
        if (marketScript.gameObject.activeSelf)
        {
            HideMarketWindow();
        }
        else
        {
            ShowMarketWindow();
            marketScript.SetShownMarketPrices();
        }
    }

    //Added by JBT 
    //Show or hide the roboticon window depending on the state the window is in when the button is pressed
    public void RoboticonButtonPressed()
    {
        if(roboticonList.gameObject.activeSelf)
        {
            HideRoboticonWindow();
        }
        else
        {
            ShowRoboticonWindow();
        }
    }

    //Added by JBT 
    //Show or hide the options window depending on the state the window is in when the button is pressed
    public void OptionsButtonPressed()
    {
        if (optionsMenu.activeSelf)
        {
            HideOptionsMenu();
        }
        else
        {
            ShowOptionsMenu();
        }
    }

    // This is called when the market button in the UI is pressed. Used to display the market window.
    public void ShowMarketWindow()
    {
        if (GameHandler.GetGameManager().GetCurrentState() == GameManager.States.PURCHASE)
        {
            marketScript.gameObject.SetActive(true);
        }
    }

    // This is called after a turn, or when the market window is closed. Used to hide the market window.
    public void HideMarketWindow()
    {
        marketScript.gameObject.SetActive(false);
    }

    //Added by JBT
    //This is called whent the Gambling button is pressed in the UI. Used to show the gambling window.
    public void ShowGamblingWindow()
    {
        if (GameHandler.GetGameManager().GetCurrentState() == GameManager.States.PURCHASE)
        {
            gamblingWindow.SetActive(true);
            gamblingWindow.GetComponent<gamblingScript>().OpenGamblingWindow();
        }
    }

    //Added by JBT
    //This is called at the start of a new turn or when the gambling window is closed. Used to hide the gambling window.
    public void HideGamblingWindow()
    {
        gamblingWindow.SetActive(false);
    }

    //
    public void ShowRoboticonWindow()
    {
        roboticonList.gameObject.SetActive(true);
        ShowRoboticonList();
    }

    public void HideRoboticonWindow()
    {
        roboticonList.gameObject.SetActive(false);
    }

    //JBT
    public void ShowMarketButton()
    {
        marketButton.SetActive(true);
    }

    //JBT
    public void HideMarketButton()
    {
        marketButton.SetActive(false);
    }

    //JBT
    public void ShowGambleButton()
    {
        gambleButton.SetActive(true);
    }

    //JBT
    public void HideGambleButton()
    {
        gambleButton.SetActive(false);
    }

    //Added by JBT to show the current human player the amount of seconds left in the current turn, if the current phase is a timed one
    public void ShowTimeout(Timeout t)
    {
        timeoutText.gameObject.SetActive(true);
        timeoutText.text = t.SecondsRemaining.ToString("00");
        if(t.SecondsRemaining < 5)
        {
            timeoutText.color = Color.red;
        }
        else
        {
            timeoutText.color = Color.white;
        }
    }

    //Added by JBT to enable the hiding of the timer text, if the current phase is not a timed one
    public void HidePhaseTimeout()
    {
        timeoutText.gameObject.SetActive(false);
    }

    //JBT
    public void ShowRoboticonButton()
    {
        roboticonButton.SetActive(true);
    }

    //JBT
    public void HideRoboticonButton()
    {
        roboticonButton.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void PurchaseTile(Tile tile)
    {
        humanGui.PurchaseTile(tile);
        tileWindow.Refresh();
    }

    public void ShowTileInfoWindow(Tile tile)
    {
        tileWindow.Show(tile);
    }

    public void RefreshTileInfoWindow()
    {
        tileWindow.Refresh();
    }

    public void HideTileInfoWindow()
    {
        tileWindow.Hide();
    }

    public void RefreshRoboticonList()
    {
        if (roboticonList.isActiveAndEnabled)
        {
            ShowRoboticonList();
        }
    }

    public void ShowRoboticonList()
    {
        List<Roboticon> roboticonsToDisplay = new List<Roboticon>();

        foreach(Roboticon roboticon in humanGui.GetCurrentHumanRoboticonList())
        {
            roboticonsToDisplay.Add(roboticon);
        }

        roboticonList.DisplayRoboticonList(roboticonsToDisplay);
    }

    /// <summary>
    /// Adds a roboticon to the roboticon display list if it is currently
    /// being displayed.
    /// </summary>
    public void AddRoboticonToList(Roboticon roboticon)
    {
        if (roboticonList.isActiveAndEnabled)
        {
            roboticonList.AddRoboticon(roboticon);
        }
    }

    public void HideRoboticonList()
    {
        roboticonList.HideRoboticonList();
    }

    public void ShowRoboticonUpgradesWindow(Roboticon roboticon)
    {
        roboticonUpgradesWindow.Show(roboticon);
    }

    public void HideRoboticonUpgradesWindow()
    {
        roboticonUpgradesWindow.Hide();
    }

    public void UpgradeRoboticon(Roboticon roboticon, ResourceGroup upgrades)
    {
        humanGui.UpgradeRoboticon(roboticon, upgrades);
    }

    public void InstallRoboticon(Roboticon roboticon)
    {
        humanGui.InstallRoboticon(roboticon);
    }

    //Added by JBT so that it is now possible to remove roboticons from their tile
    public void UninstallRoboticon(Roboticon roboticon)
    {
        humanGui.UninstallRoboticon(roboticon);
    }

    public void SetCurrentPlayerName(string name)
    {
        currentPlayerText.text = name;
    }

    public void ShowHelpBox(string helpBoxText)
    {
        helpBox.ShowHelpBox(helpBoxText);
    }

    public void HideHelpBox()
    {
        helpBox.HideHelpBox();
    }

    public void SetResourceLabels(ResourceGroup resources, int money)
    {
        foodLabel.text = resources.food.ToString();
        energyLabel.text = resources.energy.ToString();
        oreLabel.text = resources.ore.ToString();
        moneyLabel.text = money.ToString();
    }

    public void SetResourceChangeLabels(ResourceGroup resources)
    {
        foodChangeLabel.text = FormatResourceChangeLabel(resources.food);
        energyChangeLabel.text = FormatResourceChangeLabel(resources.energy);
        oreChangeLabel.text = FormatResourceChangeLabel(resources.ore);
    }

    //Created by JBT to change the UI when an AI is taking its turn
    public void SetUnknownResourceLabels()
    {
        foodLabel.text = "??";
        energyLabel.text = "??";
        oreLabel.text = "??";
        moneyLabel.text = "??";
    }

    //Created by JBT to change the UI when an AI is taking its turn
    public void SetUnknownChangeLabels()
    {
        foodChangeLabel.text = "+??";
        energyChangeLabel.text = "+??";
        oreChangeLabel.text = "+??";
    }

    //Created by JBT to change the UI when an AI is taking its turn
    public void SetAITurnText(string t)
    {
        aiTurnBox.SetActive(true);
        aiTurnText.text = t;
    }

    //Created by JBT to change the UI when an AI is taking its turn
    public void HideAITurnText()
    {
        aiTurnBox.gameObject.SetActive(false);
    }

    public void SetHumanGui(HumanGui gui)
    {
        humanGui = gui;
    }

    public HumanGui GetHumanGui()
    {
        return humanGui;
    }

    private string FormatResourceChangeLabel(int changeAmount)
    {
        string sign = (changeAmount >= 0) ? "+" : "";
        return "(" + sign + changeAmount.ToString() + ")";
    }

    //Added by JBT 
    public void QuitToMenu()
    {
        //Remove gameobjects that would not get destroyed on load
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Tile Holder"));
        Destroy(GameObject.Find("Map Manager"));

        //Go to the main menu
        SceneManager.LoadScene(0);
    }
}
