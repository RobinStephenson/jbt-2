﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class canvasScript : MonoBehaviour
{
    public helpBoxScript helpBox;
    public GameObject optionsMenu;
    public roboticonWindowScript roboticonList;
    public marketScript marketScript;
    public GameObject endPhaseButton;
    public tileInfoWindowScript tileWindow;
    public Text currentPlayerText;
    public Text currentPhaseText;
    public roboticonUpgradesWindowScript roboticonUpgradesWindow;
    private Timeout CurrentTimout;

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
        if (CurrentTimout != null)
        {
            // we are in a timed phase
            // TODO update the timer display
            if (CurrentTimout.SecondsRemaining < 5)
            {
                // TODO make the text red or something to make it clearer its ending
            }
            if (CurrentTimout.Finished)
            {
                Debug.Log("Current Timeout Finished");
                CurrentTimout = null;
                EndPhase();
            }
        }
    }

    // JBT created this method
    public void SetTimeout(Timeout timeout)
    {
        if (timeout != null && timeout.Finished)
        {
            throw new ArgumentException("Need a fresh timeout");
        }
        CurrentTimout = timeout;
        Debug.Log(String.Format("Set a new timeout {0}", timeout));
    }

    // this is called by the end phase button
    public void EndPhase()
    {
        CurrentTimout = null;
        humanGui.EndPhase();
    }

    public void DisableEndPhaseButton()
    {
        endPhaseButton.SetActive(false);
    }

    public void EnableEndPhaseButton()
    {
        endPhaseButton.SetActive(true);
    }

    public void SetCurrentPhaseText(string text)
    {
        currentPhaseText.text = text;
    }

    public void BuyFromMarket(ResourceGroup resources, int roboticonsToBuy, int price)
    {
        humanGui.BuyFromMarket(resources, roboticonsToBuy, price);
    }

    public void SellToMarket(ResourceGroup resources, int price)
    {
        humanGui.SellToMarket(resources, price);
    }

    public void ShowMarketWindow()
    {
        if (GameHandler.GetGameManager().GetCurrentState() == GameManager.States.PURCHASE)
        {
            marketScript.gameObject.SetActive(true);
        }
    }

    public void HideMarketWindow()
    {
        marketScript.gameObject.SetActive(false);
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
        foodChangeLabel.text = "??";
        energyChangeLabel.text = "??";
        oreChangeLabel.text = "??";
    }

    //Created by JBT to change the UI when an AI is taking its turn
    public void SetUnknownChangeLabels()
    {
        foodChangeLabel.text = "+??";
        energyChangeLabel.text = "+??";
        oreChangeLabel.text = "+??";
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
}
